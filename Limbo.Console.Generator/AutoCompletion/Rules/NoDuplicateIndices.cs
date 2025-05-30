using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Limbo.Console.Generator.AutoCompletion.Rules
{
    internal class NoDuplicateIndices : IAutoCompleteValidationRule
    {
        public void Validate(IMethodSymbol methodSymbol, IEnumerable<AutoCompleteDefinition> autoCompletes)
        {
            var duplicateArgIndexes = autoCompletes
                                      .GroupBy(ac => ac.ArgIndex)
                                      .Where(g => g.Count() > 1)
                                      .Select(g => g.Key)
                                      .ToList();

            if (!duplicateArgIndexes.Any())
            {
                return;
            }

            var argList = string.Join(", ", duplicateArgIndexes);

            throw new InvalidAutoCompleteException(
                $"Method '{methodSymbol.Name}' has multiple AutoComplete attributes for the same argument index: {argList}."
            );
        }
    }
}