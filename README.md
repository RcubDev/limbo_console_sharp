# LimboConsole.Sharp

[![NuGet](https://img.shields.io/nuget/v/LimboConsole.Sharp.svg)](https://www.nuget.org/packages/LimboConsole.Sharp)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Build Status](https://github.com/RcubDev/limbo_console_sharp/actions/workflows/build-and-publish.yaml/badge.svg)](https://github.com/RcubDev/limbo_console_sharp/actions)
![Platform](https://img.shields.io/badge/platform-.NET-purple)
![Godot Version](https://img.shields.io/badge/Godot-4.4.X-yellow)
[![NuGet Downloads](https://img.shields.io/nuget/dt/LimboConsole.Sharp.svg)](https://www.nuget.org/packages/LimboConsole.Sharp)

This nuget package is a c# wrapper that helps facilitate using the [limbo_console](https://github.com/limbonaut/limbo_console) Godot plugin in c#.

## Table of Contents

- [Requirements](#️-requirements)
- [Installation](#️-installation)
- [Getting Started](#-getting-started)
- [Documentation](#-documentation)
- [Contributing](#-contributing)
- [Features](#-features)
- [License](#-license)

## ✨ Features

- **Simplified Integration**: Easily interact with the [💻 limbo_console](https://github.com/limbonaut/limbo_console) Godot plugin from a c# godot game.
- **Command Registration**: [Register commands](#register-a-command) with descriptions and auto-complete sources.
- **Auto-Complete Support**: [Add auto-complete sources](#register-an-auto-complete-source) for command arguments.
- **Demo Project**: Includes examples for initialization, commands, and advanced features.

## 🛠️ Requirements

- **Godot Engine**: Version 4.4.X or higher.
- **.NET SDK**: Version 8.0
- **limbo_console plugin**: Installed and enabled in your Godot project. See [💻 limbo console](https://github.com/limbonaut/limbo_console).

## 🖥️ Installation

1. Ensure the [💻 limbo console](https://github.com/limbonaut/limbo_console) plugin is installed and enabled in your Godot project:
   - Go to **Project > Project Settings > Plugins** and enable the plugin.

2. Install the NuGet package for your Godot C# project (compatible with Godot 4.4.X).

### Using `dotnet` CLI

Run the following command:

```bash
dotnet add package LimboConsole.Sharp
```

### Using csproj

Add the following line to your godot game's csproj

```cs
<PackageReference Include="LimboConsole.Sharp" Version="<specify.your.version>" />
````

## 📝 Getting started

### Initialize Wrapper

Next on any node in your game you can initialize the console with the following:

```csharp
var limboConsole = GetTree().Root.GetNode<CanvasLayer>("LimboConsole");
LimboConsole console = new LimboConsole(limboConsole);
```

 > **Note:** If LimboConsole is not registered when trying to create the wrapper it will error.

### Register a Command

Once the wrapper is initialized, you can register commands:

```csharp
console.RegisterCommand(new Callable(this, MethodName.StartGame), "start_game", "Starts the game");

private void StartGame() {
    // Do something
}
```

The wrapper is initialized as a singleton for easy access from any node. So after it is initialized you can use `LimboConsole.Instance` anywhere. For example:

```csharp
LimboConsole.Instance.RegisterCommand(new Callable(this, MethodName.StartGame), "start_game", "Starts the game");
```

### Register an Auto-Complete Source

You can also add auto-complete sources for command arguments:

```csharp
console.AddArgumentAutocompleteSource("abc", 1, Callable.From(() => new string[] { "a", "b", "c" }));
```

This example adds an auto-complete source for the first argument of the `abc` command, suggesting the values `a`, `b`, or `c`. 

> **Note**:  when registering an argument index starts at 1 not 0 and currently has a max of 5 arguments

### More examples

See run the demo project and see [`Demo.cs`](demo/Demo.cs) for more examples of how to use the package

## 📚 Documentation

See summary comments in nuget package or refer to [💻 limbo_console](https://github.com/limbonaut/limbo_console)

## 🤝 Contributing

Thanks for your interest in contributing. Please follow these guidelines to keep the project consistent and maintainable in the long-term. Keep in mind this is just a wrapper. Feature or bug fixes for the requests for the console itself should go here [💻 limbo_console](https://github.com/limbonaut/limbo_console).

After a feature is added and/or the public api changes this repo will need to be updated so help keeping it up to date is appreciated!

See something you want or could improve upon? Make a PR! ✨

### Project structure

- **Limbo.Console.Sharp**: The root of the NuGet package.
  - **Pack**: Use `dotnet pack` to create the package. Alternatively, run the provided `.bat` script on Windows to automate packaging to the demo project.

  > The `.bat` file is a helpful reference for understanding how the demo project and the NuGet package interact. It automates the packaging process for Windows users to test changes in the demo project.

- **Demo Project**: A sample Godot project included to demonstrate:
  - Initializing the C# console wrapper.
  - Registering commands and auto-complete sources.
  - Using advanced features like signals and callable commands.

> **Note**: Currently, there is no quick script for packaging on non-Windows platforms. Contributions to add this functionality are welcome!

## Code Style & Recommendations

- Follow the official [Microsoft coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) and [name identifiers](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names)

## 📜 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.
