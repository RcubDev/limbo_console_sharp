using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Limbo.Console.Sharp.Generator
{
    /// <summary>
    /// Supporting functions for parsring AutoCompletes when processing [ConsoleCommands] 
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

            ValidateAutoCompletes(methodSymbol.Name, autoCompletes);

            return autoCompletes;
        }

        private static void ValidateAutoCompletes(string methodName, IEnumerable<AutoCompleteDefinition> autoCompletes)
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

            throw new InvalidOperationException(
                $"Method '{methodName}' has multiple AutoComplete attributes for the same argument index: {argList}."
            );
        }

        private static AutoCompleteDefinition AsAutoCompleteDefinition(AttributeData attr)
        {
            var sourceMethod = attr.ConstructorArguments[0].Value as string ?? string.Empty;
            var argIndex = attr.ConstructorArguments.Length > 1 ? (int)(attr.ConstructorArguments[1].Value ?? 0) : 0;

            return new AutoCompleteDefinition(sourceMethod, argIndex);
        }
    }

    internal sealed class AutoCompleteDefinition
    {
        public AutoCompleteDefinition(string sourceMethod, int argIndex)
        {
            SourceMethod = sourceMethod;
            ArgIndex = argIndex;
        }

        public readonly string SourceMethod;
        public readonly int ArgIndex;
    }
}