using System;

namespace Limbo.Console.Sharp
{
    /// <summary>
    /// Defines how a method should be treated as a console command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ConsoleCommandAttribute : Attribute
    {

        /// <summary>
        /// The name of the command as used in the console.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The user readable description of the command.
        /// </summary>
        public string Description { get; }

        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once ConvertToPrimaryConstructor
        /// <summary>
        /// Defines how a method should be treated as a console command.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        // ReSharper disable once MemberCanBePrivate.Global
        public ConsoleCommandAttribute(string name = "", string description = "")
        {
            Name = name;
            Description = description;
        }
    }
}

