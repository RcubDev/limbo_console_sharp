using Godot;
using Limbo.Console.Sharp;
using System;

public partial class Demo : Node2D
{
    private LimboConsole _console;
    public override void _Ready()
    {
        var limboConsole = GetTree().Root.GetNode<CanvasLayer>("LimboConsole");
        _console = new LimboConsole(limboConsole);
        
        _console.RegisterCommand(Callable.From(() => _console.PrintLine("Hello World!")), "hello_callable", "Prints Hello World from a callable");
        _console.RegisterCommand(new Callable(this, MethodName.HelloWorld), "hello_world", "Prints Hello World with a value after it");
        _console.RegisterCommand(Callable.From((string val) => _console.PrintLine("Hello World! Val: " + val)), "hello_world_string", "Prints Hello World with a value after it");        
        _console.Toggled += OnConsoleToggled;
    }

    private void OnConsoleToggled(bool isShown)
    {
        _console.PrintLine("Console toggled: " + isShown);
    }

    /// <summary>
    /// A method that takes an argument
    /// </summary>
    /// <param name="val"></param>
    public void HelloWorld(int val)
    {
        _console.PrintLine("Hello World! Val: " + val);
    }
}
