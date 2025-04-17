using Godot;
namespace Limbo.Console.Sharp;
/// <summary>
/// A class that wraps the limbo console for ease of use with c#
/// </summary>
public static class LimboConsole
{
    static CanvasLayer _limboConsole = null!;

    private static CanvasLayer LimboConsoleInstance
    {
        get
        {
            if (_limboConsole == null)
            {                
                var mainLoop = Godot.Engine.GetMainLoop();
                var sceneTree = (mainLoop as SceneTree)!;
                _limboConsole = sceneTree.Root.GetNode<CanvasLayer>("LimboConsole")!;
            }
            return _limboConsole;
        }
    }

    /// <summary>
    /// Opens the console if it is enabled - sets process mode to true for the console
    /// </summary>
    public static void OpenConsole()
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.OpenConsole);
    }
    /// <summary>
    /// Hides the console if it is enabled - sets process mode to false for the console
    /// </summary>
    public static void CloseConsole()
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.CloseConsole);
    }

    /// <summary>
    /// Returns true if the console is visible
    /// </summary>
    /// <returns></returns>
    public static bool IsVisible()
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.IsVisible).As<bool>();
    }

    /// <summary>
    /// Flips the current state of the console
    /// </summary>
    public static void ToggleConsole()
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.ToggleConsole);
    }

    /// <summary>
    /// Clears all messages in the console
    /// </summary>
    public static void ClearConsole()
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.ClearConsole);
    }

    /// <summary>
    /// Prints an info message to the console and the output
    /// </summary>
    /// <param name="pLine"></param>
    public static void Info(string pLine)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.Info, pLine);
    }
    /// <summary>
    /// Prints an error message to the console and the output
    /// </summary>
    /// <param name="pLine"></param>
    public static void Error(string pLine)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.Error, pLine);
    }

    /// <summary>
    /// Prints a warning message to the console and the output
    /// </summary>
    /// <param name="pLine"></param>
    public static void Warn(string pLine)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.Warn, pLine);
    }

    /// <summary>
    /// Prints a debug message to the console and the output
    /// </summary>
    /// <param name="pLine"></param>
    public static void Debug(string pLine)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.Debug, pLine);
    }

    /// <summary>
    /// Prints a line using boxed ASCII art
    /// </summary>
    /// <param name="p_line"></param>
    public static void PrintBoxed(string p_line)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.PrintBoxed, p_line);
    }

    /// <summary>
    /// Prints a line to the console, and optionally to standard output.
    /// </summary>
    /// <param name="pLine"></param>
    /// <param name="pStdOut"></param>
    public static void PrintLine(string pLine, bool pStdOut = false)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.PrintLine, pLine, pStdOut);
    }

    /// <summary>
    /// Registers a new command for the specified callable.
    /// Optionally, you can provide a name and a description.
    /// </summary>
    /// <param name="pFunc"></param>
    /// <param name="pName"></param>
    /// <param name="pDesc"></param>
    public static void RegisterCommand(Callable pFunc, string pName = "", string pDesc = "")
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.RegisterCommand, pFunc, pName, pDesc);
    }

    /// <summary>
    /// Unregisters a command by its callable
    /// </summary>
    /// <param name="pFunc"></param>
    public static void UnregisterCommand(Callable pFunc)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.UnregisterCommand, pFunc);
    }

    /// <summary>
    /// Unregisters a command by its name
    /// </summary>
    /// <param name="pName"></param>
    public static void UnregisterCommand(string pName)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.UnregisterCommand, pName);
    }

    /// <summary>
    /// Is a command or an alias registered by the given name
    /// </summary>
    /// <param name="pName"></param>
    /// <returns></returns>
    public static bool HasCommand(string pName)
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.HasCommand, pName).As<bool>();
    }
    /// <summary>
    /// Gets all command names registered in the console.
    /// If p_include_aliases is true, it will also include aliases.
    /// </summary>
    /// <param name="p_include_aliases"></param>
    /// <returns></returns>
    public static string[] GetCommandNames(bool p_include_aliases = false)
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.GetCommandNames, p_include_aliases).AsStringArray();
    }

    /// <summary>
    /// Gets the description of a command by its name.
    /// If the command is not found, it returns an empty string.
    /// </summary>
    /// <param name="pName"></param>
    /// <returns></returns>
    public static string GetCommandDescription(string pName)
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.GetCommandDescription, pName).AsString();
    }

    /// <summary>
    /// Registers an alias for a command (may include arguments).
    /// </summary>
    /// <param name="alias"></param>
    /// <param name="pCommandToRun"></param>
    public static void AddAlias(string alias, string pCommandToRun)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.AddAlias, alias, pCommandToRun);
    }

    /// <summary>
    /// Removes an alias by name.
    /// </summary>
    /// <param name="pName"></param>
    public static void RemoveAlias(string pName)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.RemoveAlias, pName);
    }
    /// <summary>
    /// Is an alias registered by the given name.
    /// </summary>
    /// <param name="pName"></param>
    /// <returns></returns>
    public static bool HasAlias(string pName)
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.HasAlias, pName).AsBool();
    }

    /// <summary>
    /// Lists all registered aliases
    /// </summary>
    /// <returns></returns>
    public static string[] GetAliases()
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.GetAliases).AsStringArray();
    }
    /// <summary>
    /// Returns the alias's actual command as an argument vector.
    /// </summary>
    /// <param name="p_alias"></param>
    /// <returns></returns>
    public static string[] GetAliasArgv(string p_alias)
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.GetAliasArgv, p_alias).AsStringArray();
    }

    /// <summary>
    /// Registers a callable that should return an array of possible values for the given argument and command.
    /// It will be used for autocompletion.
    /// </summary>
    /// <param name="pCommand"></param>
    /// <param name="pArgument"></param>
    /// <param name="pSource"></param>
    public static void AddArgumentAutocompleteSource(string pCommand, int pArgument, Callable pSource)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.AddArgumentAutocompleteSource, pCommand, pArgument, pSource);
    }

    /// <summary>        
    /// Parses the command line and executes the command if it's valid.
    /// </summary>
    /// <param name="pCommandLine"></param>
    /// <param name="pSilent"></param>
    public static void ExecuteCommand(string pCommandLine, bool pSilent = false)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.ExecuteCommand, pCommandLine, pSilent);
    }

    /// <summary>
    /// Execute commands from file.
    /// </summary>
    /// <param name="pFile"></param>
    /// <param name="pSilent"></param>
    public static void ExecuteScript(string pFile, bool pSilent = true)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.ExecuteScript, pFile, pSilent);
    }

    /// <summary>
    ///  Formats the tip text (hopefully useful ;).
    /// </summary>
    /// <param name="pText"></param>
    /// <returns></returns>
    public static string FormatTip(string pText)
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.FormatTip, pText).AsString();
    }

    /// <summary>
    /// Formats the command name for display.
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public static string FormatName(string p_name)
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.FormatName, p_name).AsString();
    }

    /// <summary>
    /// Prints the help text for the given command.
    /// </summary>
    /// <param name="pCommand"></param>
    /// <returns></returns>
    public static Error Usage(string pCommand)
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.Usage, pCommand).As<Error>();
    }

    /// <summary>
    /// Define an input variable for "eval" command.
    /// (I am uncertain what type pValue should be)
    /// </summary>
    /// <param name="pName"></param>
    /// <param name="pValue"></param>
    public static void AddEvalInput(string pName, Variant pValue)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.AddEvalInput, pName, pValue);
    }

    /// <summary>
    /// Remove specified input variable from "eval" command.
    /// </summary>
    /// <param name="pName"></param>
    public static void RemoveEvalInput(string pName)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.RemoveEvalInput, pName);
    }

    /// <summary>
    /// List the defined input variables used in "eval" command.
    /// </summary>
    /// <returns></returns>
    public static string[] GetEvalInputNames()
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.GetEvalInputNames).AsStringArray();
    }

    /// <summary>
    /// Get input variable values used in "eval" command, listed in the same order as names.
    /// (I am unsure if there is a more specific type I can provide)
    /// </summary>
    /// <returns></returns>
    public static Godot.Collections.Array GetEvalInputs()
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.GetEvalInputs).AsGodotArray();
    }

    /// <summary>
    /// Define the object that will be used as the base instance for "eval" command.
    /// When defined, this object will be the "self" for expressions.
    /// Can be null (the default) to not use any base instance.
    /// (I am unure if there is a more specific type I can provide for the input)
    /// </summary>
    /// <param name="instance"></param>
    public static void SetEvalBaseInstance(Variant instance)
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.SetEvalBaseInstance, instance);
    }

    /// <summary>
    /// 
    /// Get the object that will be used as the base instance for "eval" command.
    /// Null by default.
    /// </summary>
    /// <returns></returns>
    public static Variant GetEvalBaseInstance()
    {
        return LimboConsoleInstance.Call(LimboConsoleStringNames.GetEvalBaseInstance);
    }

    /// <summary>
    /// Erases the history that is persisted to the disk.
    /// </summary>
    public static void EraseHistory()
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.EraseHistory);
    }

    /// <summary>
    /// Toggles the visibility of the history GUI.
    /// </summary>
    public static void ToggleHistory()
    {
        LimboConsoleInstance.Call(LimboConsoleStringNames.ToggleHistory);
    }

    /// <summary>
    /// Connects the given callable to the signal
    /// </summary>
    /// <param name="signalName"></param>
    /// <param name="callable"></param>
    public static void Connect(StringName signalName, Callable callable)
    {
        LimboConsoleInstance.Connect(signalName, callable);
    }
    
    /// <summary>
    /// Disconnects the signal from the given callable. Must pass back the same callable that was used to connect!
    /// </summary>
    /// <param name="signalName"></param>
    /// <param name="callable"></param>
    public static void Disconnect(StringName signalName, Callable callable)
    {
        LimboConsoleInstance.Disconnect(signalName, callable);
    }

    /// <summary>
    /// Connects the ToggledSignal to the given callable
    /// </summary>
    /// <param name="callable"></param>
    public static void ConnectToggled(Callable callable)
    {
        LimboConsoleInstance.Connect(LimboConsoleStringNames.ToggledSignal, callable);
    }

    /// <summary>
    /// Disconnects the ToggledSignal from the given callable. Must pass back the same callable that was used to connect!
    /// </summary>
    /// <param name="callable"></param>
    public static void DisconnectToggled(Callable callable)
    {
        LimboConsoleInstance.Disconnect(LimboConsoleStringNames.ToggledSignal, callable);
    }
}
