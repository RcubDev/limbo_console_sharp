using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;


namespace Limbo.Console.Sharp.Generator
{
    /// <summary>
    /// Generates a single function to register all functions in the class labeled with <see cref="ConsoleCommandAttribute"/> 
    /// </summary>
    [Generator]
    public sealed class ConsoleCommandGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var methodsWithAttr = context.SyntaxProvider
              .CreateSyntaxProvider(
                predicate: (s, _) => s is MethodDeclarationSyntax m && m.AttributeLists.Count > 0,
                transform: (ctx, _) => GetCommandMethod(ctx))
              .Where(m => m != null);

            var compilationAndMethods = context.CompilationProvider.Combine(methodsWithAttr.Collect());

            context.RegisterSourceOutput(compilationAndMethods, (spc, source) => {
                var (compilation, methods) = source;
                var grouped = methods
                  .OfType<CommandMethodInfo>()
                  .GroupBy(m => m.ContainingType, SymbolEqualityComparer.Default);

                foreach (var group in grouped)
                {
                    var typeSymbol = group.Key;
                    var src = GenerateRegisterFunction(typeSymbol, group);
                    spc.AddSource($"{typeSymbol.Name}_ConsoleCommands.g.cs", SourceText.From(src, Encoding.UTF8));
                }
            });
        }

        private static CommandMethodInfo GetCommandMethod(GeneratorSyntaxContext context)
        {
            var methodSyntax = (MethodDeclarationSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetDeclaredSymbol(methodSyntax) as IMethodSymbol;
            if (methodSymbol is null || !methodSymbol.GetAttributes().Any(attr => attr.AttributeClass?.Name == nameof(ConsoleCommandAttribute)))
                return null;

            var attrData = methodSymbol.GetAttributes().First(attr => attr.AttributeClass?.Name == nameof(ConsoleCommandAttribute));
            ImmutableArray<TypedConstant> args = attrData.ConstructorArguments;

            return new CommandMethodInfo(methodSymbol, args);
        }

        private static string GenerateRegisterFunction(ISymbol classSymbol, IEnumerable<CommandMethodInfo> methods)
        {
            var ns = classSymbol.ContainingNamespace.ToDisplayString();
            var sb = new StringBuilder();

            sb.AppendLine("using Godot;");
            sb.AppendLine("using Limbo.Console.Sharp;");
            sb.AppendLine();

            if (!string.IsNullOrEmpty(ns) && !classSymbol.ContainingNamespace.IsGlobalNamespace)
            {
                sb.AppendLine($"namespace {ns} {{");
            }

            string accessibility;
            switch (classSymbol.DeclaredAccessibility)
            {
                case Accessibility.Public:
                    accessibility = "public";
                    break;
                case Accessibility.Internal:
                    accessibility = "internal";
                    break;
                case Accessibility.Private:
                    accessibility = "private";
                    break;
                case Accessibility.Protected:
                    accessibility = "protected";
                    break;
                case Accessibility.ProtectedAndInternal:
                    accessibility = "protected internal";
                    break;
                case Accessibility.ProtectedOrInternal:
                    accessibility = "internal protected";
                    break;
                default:
                    accessibility = "internal";
                    break;
            }

            sb.AppendLine($"{accessibility} partial class {classSymbol.Name} {{"); sb.AppendLine(" private void RegisterConsoleCommands() {");

            foreach (var method in methods)
            {
                var callable = method.Method.Parameters.Length == 0
                  ? $"new Callable(this, nameof({method.Method.Name}))"
                  : $"new Callable(this, \"{method.Method.Name}\")"; // TODO: consider arg-aware logic

                var registerCall = method.Description != null
                  ? $"LimboConsole.RegisterCommand({callable}, \"{method.Name}\", \"{method.Description}\");"
                  : $"LimboConsole.RegisterCommand({callable}, \"{method.Name}\");";

                sb.AppendLine("    " + registerCall);
            }

            sb.AppendLine("  }");
            sb.AppendLine("}"); // class

            if (!string.IsNullOrEmpty(ns) && !classSymbol.ContainingNamespace.IsGlobalNamespace)
            {
                sb.AppendLine("}"); // namespace
            }

            return sb.ToString();
        }

        private sealed class CommandMethodInfo
        {
            public CommandMethodInfo(IMethodSymbol method, ImmutableArray<TypedConstant> args)
            {
                Method = method;
                ContainingType = method.ContainingType;
                Name = args.Length > 0 ? args[0].Value?.ToString() ?? method.Name : method.Name;
                Description = args.Length > 1 ? args[1].Value?.ToString() : null;
            }

            public IMethodSymbol Method { get; }
            public INamedTypeSymbol ContainingType { get; }
            public string Name { get; }
            public string Description { get; }
        }
    }
}
