namespace Limbo.Console.Sharp;

using System;

[AttributeUsage(AttributeTargets.Method)]
public class ConsoleCommandAttribute(string name, string description) : Attribute {
  public string Name { get; } = name;
  public string Description { get; } = description;
  public ConsoleCommandAttribute() : this(string.Empty, string.Empty) {}
  public ConsoleCommandAttribute(string name) : this(name, string.Empty) {}
}
