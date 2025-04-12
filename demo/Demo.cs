using Godot;
using Limbo.Console.Sharp;
using System;
using System.Linq;

public partial class Demo : Node2D
{
    private LimboConsole _console;
    public override void _Ready()
    {
        var limboConsole = GetTree().Root.GetNode<CanvasLayer>("LimboConsole");
        _console = new LimboConsole(limboConsole);

        // Registering a command with no arguments
        _console.RegisterCommand(new Callable(this, MethodName.StartGameCommand), "game start", "Starts the game");
        // Registering a command with a arguments
        _console.RegisterCommand(new Callable(this, MethodName.StopGameCommand), "game stop", "Stops the game (add a string for options!)");

        // Register commands to start a demo of subscribing to the toggle signal of the console
        _console.RegisterCommand(new Callable(this, MethodName.StartDemoToggledSignal), "toggle_signal start", "Starts the demoing toggled signal");
        _console.RegisterCommand(new Callable(this, MethodName.StopDemoToggledSignal), "toggle_signal stop", "Stops the demoing toggled signal");

        // Registering a command with an argument that has an auto-complete source
        _console.RegisterCommand(new Callable(this, MethodName.ABC), "abc", "Prints A, B, or C based on the argument (autocomplete source)");
        // Arguments with auto-complete sources index starts at 1 (max is 5 currently)
        _console.AddArgumentAutocompleteSource("abc", 1, Callable.From(() => new string[] { "a", "b", "c" }));

        // Register commands via callables 
        // NOTE: currently there is a bug in godot that will throw an error but they still work and register
        //       see (https://github.com/limbonaut/limbo_console/issues/60)

        // Register in-line lamda
        _console.RegisterCommand(Callable.From(() => _console.PrintLine("Hello World!")),
                                                         "hello_callable",
                                                         "Prints Hello World from a command registered by a callable");
        // Register an in-line lambda with a callable argument
        _console.RegisterCommand(Callable.From((string val) => _console.PrintLine("Hello World! Val: " + val)),
                                                               "hello_callable_arg",
                                                               "Prints Hello World with a value after it");

        // Register a callable with a callable argument and an auto-complete source
        var validOptions = new[] { "a", "b", "c" };
        _console.RegisterCommand(Callable.From((string val) =>
                                    {
                                        _console.PrintLine("Hello World! Val: " + (validOptions.Contains(val) ? val : "Invalid"));
                                    }),
                                    "hello_callable_abc",
                                    "Prints Hello World and the value if it is a, b, or c");
        _console.AddArgumentAutocompleteSource("hell_callable_arg", 1, Callable.From(() => validOptions));

        // NOTE: You don't really have a need for bound arguments in c# since you can use lambdas
        // see https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_differences.html#callable
    }

    /// <summary>
    /// Start game command
    /// </summary>
    private void StartGameCommand() => _console.PrintLine("Game started!");

    /// <summary>
    /// Stop game command
    /// </summary>
    /// <param name="options"></param>
    private void StopGameCommand(string options)
    {
        _console.PrintLine("Game stopped! Option: " + options);
    }

    /// <summary>
    /// Prints A, B, or C based on the argument
    /// </summary>
    /// <param name="aOrBOrC"></param>

    private void ABC(string aOrBOrC)
    {
        switch (aOrBOrC)
        {
            case "a":
                _console.PrintLine("A");
                break;
            case "b":
                _console.PrintLine("B");
                break;
            case "c":
                _console.PrintLine("C");
                break;
            default:
                _console.PrintLine("Invalid option");
                break;
        }
    }

    private bool _isToggleConnected = false;
    /// <summary>
    /// Starts listening to the toggled signal if not already listening
    /// </summary>
    private void StartDemoToggledSignal()
    {
        if (_isToggleConnected)
            return;
        _isToggleConnected = true;
        _console.Toggled += OnConsoleToggled;
    }
    /// <summary>
    /// Stops listening to the toggled signal if already listening
    /// </summary>
    private void StopDemoToggledSignal()
    {
        if (!_isToggleConnected)
            return;
        _isToggleConnected = false;
        _console.Toggled -= OnConsoleToggled;
    }

    /// <summary>
    /// Prints a line to the console when the toggled signal is emitted
    /// </summary>
    /// <param name="isShown"></param>
    private void OnConsoleToggled(bool isShown)
    {
        _console.PrintLine("Console toggled: " + isShown);
    }
}
