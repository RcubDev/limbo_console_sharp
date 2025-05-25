namespace Limbo.Console.Sharp.Generator;

using System.Collections.Generic;
using System.Text;

[Generator]
public class ConsoleCommandGenerator : IIncrementalGenerator {
  public void Initialize(IncrementalGeneratorInitializationContext context) {
    var methodsWithAttr = context.SyntaxProvider
      .CreateSyntaxProvider(
        predicate: (s, _) => s is MethodDeclarationSyntax m && m.AttributeLists.Count > 0,
        transform: (ctx, _) => GetCommandMethod(ctx))
      .Where(static m => m is not null);

    var compilationAndMethods = context.CompilationProvider.Combine(methodsWithAttr.Collect());

    context.RegisterSourceOutput(compilationAndMethods, (spc, source) => {
      var (compilation, methods) = source;
      var grouped = methods
        .OfType<CommandMethodInfo>()
        .GroupBy(m => m.ContainingType, SymbolEqualityComparer.Default);

      foreach (var group in grouped) {
        var typeSymbol = group.Key;
        var src = GenerateRegisterFunction(typeSymbol, group);
        spc.AddSource($"{typeSymbol.Name}_ConsoleCommands.g.cs", SourceText.From(src, Encoding.UTF8));
      }
    });
  }

  private static CommandMethodInfo? GetCommandMethod(GeneratorSyntaxContext context) {
    var methodSyntax = (MethodDeclarationSyntax)context.Node;
    var methodSymbol = context.SemanticModel.GetDeclaredSymbol(methodSyntax) as IMethodSymbol;
    if (methodSymbol is null || !methodSymbol.GetAttributes().Any(attr => attr.AttributeClass?.Name == "ConsoleCommandAttribute"))
      return null;

    var attrData = methodSymbol.GetAttributes().First(attr => attr.AttributeClass?.Name == "ConsoleCommandAttribute");
    var args = attrData.ConstructorArguments;

    return new CommandMethodInfo {
      Method = methodSymbol,
      ContainingType = methodSymbol.ContainingType!,
      Name = args.Length > 0 ? args[0].Value?.ToString() ?? methodSymbol.Name : methodSymbol.Name,
      Description = args.Length > 1 ? args[1].Value?.ToString() : null
    };
  }

  private static string GenerateRegisterFunction(INamedTypeSymbol classSymbol, IEnumerable<CommandMethodInfo> methods) {
    var ns = classSymbol.ContainingNamespace.ToDisplayString();
    var sb = new StringBuilder();

    sb.AppendLine($"namespace {ns} {{");
    sb.AppendLine($"partial class {classSymbol.Name} {{");
    sb.AppendLine("  private void RegisterConsoleCommands() {");

    foreach (var method in methods) {
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
    sb.AppendLine("}"); // namespace

    return sb.ToString();
  }

  private record CommandMethodInfo {
    public IMethodSymbol Method { get; init; } = null!;
    public INamedTypeSymbol ContainingType { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
  }
}
