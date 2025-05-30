namespace Limbo.Console.Generator.AutoCompletion
{
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