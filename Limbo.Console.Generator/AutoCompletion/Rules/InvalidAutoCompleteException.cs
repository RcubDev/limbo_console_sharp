using System;

namespace Limbo.Console.Generator.AutoCompletion.Rules
{
    /// <summary>
    /// Exception thrown during auto-completion generation when an autocomplete declaration violates a rule.
    /// </summary>
    internal sealed class InvalidAutoCompleteException : Exception
    {
        public InvalidAutoCompleteException(string message) : base(message) { }
    }
}