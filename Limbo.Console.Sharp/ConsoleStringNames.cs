using Godot;
namespace Limbo.Console.Sharp;

public class LimboConsoleStringNames
{
    /// <summary>
    /// The string name relating to the "toggled" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName ToggledSignal = new StringName("toggled");

    /// <summary>
    /// The string name relating to the "open_console" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName OpenConsole = new StringName("open_console");

    /// <summary>
    /// The string name relating to the "close_console" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName CloseConsole = new StringName("close_console");

    /// <summary>
    /// The string name relating to the "is_visible" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName IsVisible = new StringName("is_visible");

    /// <summary>
    /// The string name relating to the "toggle_console" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName ToggleConsole = new StringName("toggle_console");

    /// <summary>
    /// The string name relating to the "clear_console" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName ClearConsole = new StringName("clear_console");

    /// <summary>
    /// The string name relating to the "info" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName Info = new StringName("info");

    /// <summary>
    /// The string name relating to the "error" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName Error = new StringName("error");

    /// <summary>
    /// The string name relating to the "warn" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName Warn = new StringName("warn");

    /// <summary>
    /// The string name relating to the "debug" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName Debug = new StringName("debug");

    /// <summary>
    /// The string name relating to the "print_boxed" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName PrintBoxed = new StringName("print_boxed");

    /// <summary>
    /// The string name relating to the "print_line" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName PrintLine = new StringName("print_line");

    /// <summary>
    /// The string name relating to the "register_command" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName RegisterCommand = new StringName("register_command");

    /// <summary>
    /// The string name relating to the "unregister_command" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName UnregisterCommand = new StringName("unregister_command");

    /// <summary>
    /// The string name relating to the "has_command" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName HasCommand = new StringName("has_command");

    /// <summary>
    /// The string name relating to the "get_command_names" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName GetCommandNames = new StringName("get_command_names");

    /// <summary>
    /// The string name relating to the "get_command_description" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName GetCommandDescription = new StringName("get_command_description");

    /// <summary>
    /// The string name relating to the "add_alias" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName AddAlias = new StringName("add_alias");

    /// <summary>
    /// The string name relating to the "remove_alias" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName RemoveAlias = new StringName("remove_alias");

    /// <summary>
    /// The string name relating to the "has_alias" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName HasAlias = new StringName("has_alias");

    /// <summary>
    /// The string name relating to the "get_aliases" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName GetAliases = new StringName("get_aliases");

    /// <summary>
    /// The string name relating to the "get_alias_argv" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName GetAliasArgv = new StringName("get_alias_argv");

    /// <summary>
    /// The string name relating to the "add_argument_autocomplete_source" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName AddArgumentAutocompleteSource = new StringName("add_argument_autocomplete_source");

    /// <summary>
    /// The string name relating to the "execute_command" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName ExecuteCommand = new StringName("execute_command");

    /// <summary>
    /// The string name relating to the "execute_script" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName ExecuteScript = new StringName("execute_script");

    /// <summary>
    /// The string name relating to the "format_tip" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName FormatTip = new StringName("format_tip");

    /// <summary>
    /// The string name relating to the "format_name" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName FormatName = new StringName("format_name");

    /// <summary>
    /// The string name relating to the "usage" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName Usage = new StringName("usage");

    /// <summary>
    /// The string name relating to the "add_eval_input" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName AddEvalInput = new StringName("add_eval_input");

    /// <summary>
    /// The string name relating to the "remove_eval_input" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName RemoveEvalInput = new StringName("remove_eval_input");

    /// <summary>
    /// The string name relating to the "get_eval_input_names" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName GetEvalInputNames = new StringName("get_eval_input_names");

    /// <summary>
    /// The string name relating to the "get_eval_inputs" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName GetEvalInputs = new StringName("get_eval_inputs");

    /// <summary>
    /// The string name relating to the "set_eval_base_instance" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName SetEvalBaseInstance = new StringName("set_eval_base_instance");

    /// <summary>
    /// The string name relating to the "get_eval_base_instance" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName GetEvalBaseInstance = new StringName("get_eval_base_instance");

    /// <summary>
    /// The string name relating to the "erase_history" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName EraseHistory = new StringName("erase_history");

    /// <summary>
    /// The string name relating to the "toggle_history" signal in limbo_console.gd
    /// </summary>
    public readonly static StringName ToggleHistory = new StringName("toggle_history");
}