using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Limbo.Console.Generator.AutoCompletion.Rules
{
    internal interface IAutoCompleteValidationRule
    {
        /// <summary>
        /// Throws an exception if the auto-completes are invalid for the given method.
        /// </summary>
        /// <param name="methodSymbol"></param>
        /// <param name="autoCompletes"></param>
        void Validate(IMethodSymbol methodSymbol, IEnumerable<AutoCompleteDefinition> autoCompletes);
    }
}