using System.Collections.Generic;
using System.Linq;
using Limbo.Console.Generator.AutoCompletion.Rules;
using Limbo.Console.Sharp;
using Microsoft.CodeAnalysis;

namespace Limbo.Console.Generator.AutoCompletion
{
    /// <summary>
    /// Supporting functions for parsring AutoCompletion when processing [ConsoleCommands] 
    /// </summary>
    public static class AutoCompletes
    {
        /// <summary>
        /// Parses all of the [AutoComplete] attributes on the method, if any.
        /// </summary>
        /// <param name="methodSymbol"></param>
        /// <returns></returns>
        internal static IEnumerable<AutoCompleteDefinition> Parse(IMethodSymbol methodSymbol)
        {
            var autoCompletes = methodSymbol.GetAttributes()
                                            .Where(attr => attr.AttributeClass?.Name == nameof(AutoCompleteAttribute))
                                            .Select(AsAutoCompleteDefinition)
                                            .ToArray();

            ValidateAutoCompletes(methodSymbol, autoCompletes);

            return autoCompletes;
        }
        
        private static readonly IEnumerable<IAutoCompleteValidationRule> ValidationRules = new IAutoCompleteValidationRule[]
        {
            new MustDecorateAConsoleCommand(),
            new NoDuplicateIndices(),
        };

        private static void ValidateAutoCompletes(IMethodSymbol methodSymbol, IEnumerable<AutoCompleteDefinition> autoCompletes)
        {
            var autoCompletesArray = autoCompletes as AutoCompleteDefinition[] ?? autoCompletes.ToArray();
            if (!autoCompletesArray.Any())
            {
                return; // No auto-completes to validate
            }
            foreach (var rule in ValidationRules)
            {
                rule.Validate(methodSymbol, autoCompletesArray);
            }
        }

        private static AutoCompleteDefinition AsAutoCompleteDefinition(AttributeData attr)
        {
            var sourceMethod = attr.ConstructorArguments[0].Value as string ?? string.Empty;
            var argIndex = attr.ConstructorArguments.Length > 1 ? (int)(attr.ConstructorArguments[1].Value ?? 0) : 0;

            return new AutoCompleteDefinition(sourceMethod, argIndex);
        }
    }
}