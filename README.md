# MauiReactor
## Component-based UI Library built on top of .NET MAUI

[![Build status](https://ci.appveyor.com/api/projects/status/trl7dwvicfxn5at5?svg=true)](https://ci.appveyor.com/project/adospace/reactorui-maui) [![Nuget](https://img.shields.io/nuget/v/Reactor.Maui)](https://www.nuget.org/packages/Reactor.Maui) 

MauiReactor is .NET library written on top of .NET MAUI that allows you to write applications in pure C# using an MVU approach.

This is the classic Counter app in MauiReactor:

```csharp
class CounterPageState
{
    public int Counter { get; set; }
}

class CounterPage : Component<CounterPageState>
{
    public override VisualNode Render()
        => ContentPage("Counter Sample",
            VStack(
                Label($"Counter: {State.Counter}"),

                Button("Click To Increment", () =>
                    SetState(s => s.Counter++))
            )
            .Spacing(10)
            .Center()
        );
    
}
```

### Setting up MauiReactor from CLI

1. Install MauiReactor templates
```
dotnet new install Reactor.Maui.TemplatePack
```

2. Install MauiReactor hot reload console command
```
dotnet tool install -g Reactor.Maui.HotReload
```
If you already installed an old version of Reactor.Maui.HotReload you can update it to the latest using this command:
```
dotnet tool update -g Reactor.Maui.HotReload
```

3. Create a sample project
```
dotnet new maui-reactor-startup -o my-new-project
```
and move inside the new project folder
```
cd .\my-new-project\
```

4. Build & run the project (emulator or device must be running and configured)
```
dotnet build -t:Run -f net9.0-android
```
Under Mac, to target an iOS device/emulator, issue a command like this:
```
dotnet build -t:Run /p:_DeviceName=:v2:udid=<device_id> -f net9.0-ios
```
where the device id comes from this list:
```
xcrun simctl list
```

5. Hot-reload console (in a different shell)
```
dotnet-maui-reactor -f [net9.0-android|net9.0-ios|...]
```

6. Edits to code should be hot-reloaded by the application --> Enjoy!

### Documentation ###

[Documentation](https://adospace.gitbook.io/mauireactor/)


### Videos ###

[All Packages](https://www.nuget.org/packages?q=Reactor.Maui)

[Introductionary video from Solution1 conference](https://www.youtube.com/watch?v=TSh9PL-ziY0&t=961s&ab_channel=C%23CommunityDiscord)  ![YouTube Video Views](https://img.shields.io/youtube/views/TSh9PL-ziY0?style=social)

[Interview with James Montemagno](https://www.youtube.com/watch?v=w_Km5AyreT0&ab_channel=dotnet)  ![YouTube Video Views](https://img.shields.io/youtube/views/w_Km5AyreT0?style=social)

[Getting started video from Gerald Versluis](https://www.youtube.com/watch?v=egklcAC9arY&ab_channel=GeraldVersluis)  ![YouTube Video Views](https://img.shields.io/youtube/views/egklcAC9arY?style=social)

### Sample Applications ###

[Main Samples Repository](https://github.com/adospace/mauireactor-samples)

[Rive App](https://github.com/adospace/rive-app)

[KeeMind App](https://github.com/adospace/kee-mind)

[Samples and test application](https://github.com/adospace/reactorui-maui/tree/main/samples)


### How to contribute

- Star the repository!
- File an issue ([Issues](https://github.com/adospace/reactorui-maui/issues))
- Fix bugs, add features, or improve the code with PRs
- Help with the documentation ([Documentation Repo](https://github.com/adospace/reactorui-maui-docs))
