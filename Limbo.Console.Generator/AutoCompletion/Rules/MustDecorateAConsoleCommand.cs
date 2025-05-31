using System.Collections.Generic;
using System.Linq;
using Limbo.Console.Sharp;
using Microsoft.CodeAnalysis;

namespace Limbo.Console.Generator.AutoCompletion.Rules
{
    internal class MustDecorateAConsoleCommand : IAutoCompleteValidationRule
    {
        public void Validate(IMethodSymbol methodSymbol, IEnumerable<AutoCompleteDefinition> autoCompletes)
        {
            if(methodSymbol.GetAttributes().Any(a => a?.AttributeClass?.Name == nameof(ConsoleCommandAttribute)))
                return;

            var methodName = $"{methodSymbol.ContainingType.Name}.{methodSymbol.Name}";
            throw new InvalidAutoCompleteException($"AutoComplete method {methodName} must be decorated with [ConsoleCommand] attribute.");
        }
    }
}