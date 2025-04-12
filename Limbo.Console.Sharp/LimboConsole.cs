using Godot;
namespace Limbo.Console.Sharp;
/// <summary>
/// A class that wraps the limbo console for ease of use with c#
/// </summary>
public partial class LimboConsole : Godot.RefCounted
{
    /// <summary>
    /// Delegate type that is emitted when the console is toggled
    /// </summary>
    /// <param name="isShown"></param>
    [Signal]
    public delegate void ToggledEventHandler(bool isShown);
    /// <summary>
    /// Static instance of the console
    /// </summary>
    public static LimboConsole Instance { get; private set; } = null!;
    CanvasLayer _limboConsole = null!;
    /// <summary>
    /// Passes the limbo console to the wrapper
    /// </summary>
    /// <param name="limboConsole"></param>
    public LimboConsole(CanvasLayer limboConsole)
    {
        _limboConsole = limboConsole;
        _limboConsole.Connect(LimboConsoleStringNames.ToggledSignal, new Callable(this, nameof(OnToggledSignal)));
        Instance = this;
    }

    /// <summary>
    /// Opens the console if it is enabled - sets process mode to true for the console
    /// </summary>
    public void OpenConsole()
    {
        _limboConsole.Call(LimboConsoleStringNames.OpenConsole);
    }
    /// <summary>
    /// Hides the console if it is enabled - sets process mode to false for the console
    /// </summary>
    public void CloseConsole()
    {
        _limboConsole.Call(LimboConsoleStringNames.CloseConsole);
    }

    /// <summary>
    /// Returns true if the console is visible
    /// </summary>
    /// <returns></returns>
    public bool IsVisible()
    {
        return _limboConsole.Call(LimboConsoleStringNames.IsVisible).As<bool>();
    }

    /// <summary>
    /// Flips the current state of the console
    /// </summary>
    public void ToggleConsole()
    {
        _limboConsole.Call(LimboConsoleStringNames.ToggleConsole);
    }

    /// <summary>
    /// Clears all messages in the console
    /// </summary>
    public void ClearConsole()
    {
        _limboConsole.Call(LimboConsoleStringNames.ClearConsole);
    }

    /// <summary>
    /// Prints an info message to the console and the output
    /// </summary>
    /// <param name="pLine"></param>
    public void Info(string pLine)
    {
        _limboConsole.Call(LimboConsoleStringNames.Info, pLine);
    }
    /// <summary>
    /// Prints an error message to the console and the output
    /// </summary>
    /// <param name="pLine"></param>
    public void Error(string pLine)
    {
        _limboConsole.Call(LimboConsoleStringNames.Error, pLine);
    }

    /// <summary>
    /// Prints a warning message to the console and the output
    /// </summary>
    /// <param name="pLine"></param>
    public void Warn(string pLine)
    {
        _limboConsole.Call(LimboConsoleStringNames.Warn, pLine);
    }

    /// <summary>
    /// Prints a debug message to the console and the output
    /// </summary>
    /// <param name="pLine"></param>
    public void Debug(string pLine)
    {
        _limboConsole.Call(LimboConsoleStringNames.Debug, pLine);
    }

    /// <summary>
    /// Prints a line using boxed ASCII art
    /// </summary>
    /// <param name="p_line"></param>
    public void PrintBoxed(string p_line)
    {
        _limboConsole.Call(LimboConsoleStringNames.PrintBoxed, p_line);
    }

    /// <summary>
    /// Prints a line to the console, and optionally to standard output.
    /// </summary>
    /// <param name="pLine"></param>
    /// <param name="pStdOut"></param>
    public void PrintLine(string pLine, bool pStdOut = false)
    {
        _limboConsole.Call(LimboConsoleStringNames.PrintLine, pLine, pStdOut);
    }

    /// <summary>
    /// Registers a new command for the specified callable.
    /// Optionally, you can provide a name and a description.
    /// </summary>
    /// <param name="pFunc"></param>
    /// <param name="pName"></param>
    /// <param name="pDesc"></param>
    public void RegisterCommand(Callable pFunc, string pName = "", string pDesc = "")
    {
        _limboConsole.Call(LimboConsoleStringNames.RegisterCommand, pFunc, pName, pDesc);
    }

    /// <summary>
    /// Unregisters a command by its callable
    /// </summary>
    /// <param name="pFunc"></param>
    public void UnregisterCommand(Callable pFunc)
    {
        _limboConsole.Call(LimboConsoleStringNames.UnregisterCommand, pFunc);
    }

    /// <summary>
    /// Unregisters a command by its name
    /// </summary>
    /// <param name="pName"></param>
    public void UnregisterCommand(string pName)
    {
        _limboConsole.Call(LimboConsoleStringNames.UnregisterCommand, pName);
    }

    /// <summary>
    /// Is a command or an alias registered by the given name
    /// </summary>
    /// <param name="pName"></param>
    /// <returns></returns>
    public bool HasCommand(string pName)
    {
        return _limboConsole.Call(LimboConsoleStringNames.HasCommand, pName).As<bool>();
    }
    /// <summary>
    /// Gets all command names registered in the console.
    /// If p_include_aliases is true, it will also include aliases.
    /// </summary>
    /// <param name="p_include_aliases"></param>
    /// <returns></returns>
    public string[] GetCommandNames(bool p_include_aliases = false)
    {
        return _limboConsole.Call(LimboConsoleStringNames.GetCommandNames, p_include_aliases).AsStringArray();
    }

    /// <summary>
    /// Gets the description of a command by its name.
    /// If the command is not found, it returns an empty string.
    /// </summary>
    /// <param name="pName"></param>
    /// <returns></returns>
    public string GetCommandDescription(string pName)
    {
        return _limboConsole.Call(LimboConsoleStringNames.GetCommandDescription, pName).AsString();
    }

    /// <summary>
    /// Registers an alias for a command (may include arguments).
    /// </summary>
    /// <param name="alias"></param>
    /// <param name="pCommandToRun"></param>
    public void AddAlias(string alias, string pCommandToRun)
    {
        _limboConsole.Call(LimboConsoleStringNames.AddAlias, alias, pCommandToRun);
    }

    /// <summary>
    /// Removes an alias by name.
    /// </summary>
    /// <param name="pName"></param>
    public void RemoveAlias(string pName)
    {
        _limboConsole.Call(LimboConsoleStringNames.RemoveAlias, pName);
    }
    /// <summary>
    /// Is an alias registered by the given name.
    /// </summary>
    /// <param name="pName"></param>
    /// <returns></returns>
    public bool HasAlias(string pName)
    {
        return _limboConsole.Call(LimboConsoleStringNames.HasAlias, pName).AsBool();
    }

    /// <summary>
    /// Lists all registered aliases
    /// </summary>
    /// <returns></returns>
    public string[] GetAliases()
    {
        return _limboConsole.Call(LimboConsoleStringNames.GetAliases).AsStringArray();
    }
    /// <summary>
    /// Returns the alias's actual command as an argument vector.
    /// </summary>
    /// <param name="p_alias"></param>
    /// <returns></returns>
    public string[] GetAliasArgv(string p_alias)
    {
        return _limboConsole.Call(LimboConsoleStringNames.GetAliasArgv, p_alias).AsStringArray();
    }

    /// <summary>
    /// Registers a callable that should return an array of possible values for the given argument and command.
    /// It will be used for autocompletion.
    /// </summary>
    /// <param name="pCommand"></param>
    /// <param name="pArgument"></param>
    /// <param name="pSource"></param>
    public void AddArgumentAutocompleteSource(string pCommand, int pArgument, Callable pSource)
    {
        _limboConsole.Call(LimboConsoleStringNames.AddArgumentAutocompleteSource, pCommand, pArgument, pSource);
    }

    /// <summary>        
    /// Parses the command line and executes the command if it's valid.
    /// </summary>
    /// <param name="pCommandLine"></param>
    /// <param name="pSilent"></param>
    public void ExecuteCommand(string pCommandLine, bool pSilent = false)
    {
        _limboConsole.Call(LimboConsoleStringNames.ExecuteCommand, pCommandLine, pSilent);
    }

    /// <summary>
    /// Execute commands from file.
    /// </summary>
    /// <param name="pFile"></param>
    /// <param name="pSilent"></param>
    public void ExecuteScript(string pFile, bool pSilent = true)
    {
        _limboConsole.Call(LimboConsoleStringNames.ExecuteScript, pFile, pSilent);
    }

    /// <summary>
    ///  Formats the tip text (hopefully useful ;).
    /// </summary>
    /// <param name="pText"></param>
    /// <returns></returns>
    public string FormatTip(string pText)
    {
        return _limboConsole.Call(LimboConsoleStringNames.FormatTip, pText).AsString();
    }

    /// <summary>
    /// Formats the command name for display.
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public string FormatName(string p_name)
    {
        return _limboConsole.Call(LimboConsoleStringNames.FormatName, p_name).AsString();
    }

    /// <summary>
    /// Prints the help text for the given command.
    /// </summary>
    /// <param name="pCommand"></param>
    /// <returns></returns>
    public Error Usage(string pCommand)
    {
        return _limboConsole.Call(LimboConsoleStringNames.Usage, pCommand).As<Error>();
    }

    /// <summary>
    /// Define an input variable for "eval" command.
    /// (I am uncertain what type pValue should be)
    /// </summary>
    /// <param name="pName"></param>
    /// <param name="pValue"></param>
    public void AddEvalInput(string pName, Variant pValue)
    {
        _limboConsole.Call(LimboConsoleStringNames.AddEvalInput, pName, pValue);
    }

    /// <summary>
    /// Remove specified input variable from "eval" command.
    /// </summary>
    /// <param name="pName"></param>
    public void RemoveEvalInput(string pName)
    {
        _limboConsole.Call(LimboConsoleStringNames.RemoveEvalInput, pName);
    }

    /// <summary>
    /// List the defined input variables used in "eval" command.
    /// </summary>
    /// <returns></returns>
    public string[] GetEvalInputNames()
    {
        return _limboConsole.Call(LimboConsoleStringNames.GetEvalInputNames).AsStringArray();
    }

    /// <summary>
    /// Get input variable values used in "eval" command, listed in the same order as names.
    /// (I am unsure if there is a more specific type I can provide)
    /// </summary>
    /// <returns></returns>
    public Godot.Collections.Array GetEvalInputs()
    {
        return _limboConsole.Call(LimboConsoleStringNames.GetEvalInputs).AsGodotArray();
    }

    /// <summary>
    /// Define the object that will be used as the base instance for "eval" command.
    /// When defined, this object will be the "self" for expressions.
    /// Can be null (the default) to not use any base instance.
    /// (I am unure if there is a more specific type I can provide for the input)
    /// </summary>
    /// <param name="instance"></param>
    public void SetEvalBaseInstance(Variant instance)
    {
        _limboConsole.Call(LimboConsoleStringNames.SetEvalBaseInstance, instance);
    }

    /// <summary>
    /// 
    /// Get the object that will be used as the base instance for "eval" command.
    /// Null by default.
    /// </summary>
    /// <returns></returns>
    public object GetEvalBaseInstance()
    {
        return _limboConsole.Call(LimboConsoleStringNames.GetEvalBaseInstance);
    }

    /// <summary>
    /// Erases the history that is persisted to the disk.
    /// </summary>
    public void EraseHistory()
    {
        _limboConsole.Call(LimboConsoleStringNames.EraseHistory);
    }

    /// <summary>
    /// Toggles the visibility of the history GUI.
    /// </summary>
    public void ToggleHistory()
    {
        _limboConsole.Call(LimboConsoleStringNames.ToggleHistory);
    }


    /// <summary>
    /// Handler for the ToggledSignal.
    /// Invokes the OnToggledEvent delegate.
    /// </summary>
    /// <param name="isShown">Whether the console is shown or not.</param>
    private void OnToggledSignal(bool isShown)
    {
        EmitSignal(SignalName.Toggled, isShown);
    }
}
