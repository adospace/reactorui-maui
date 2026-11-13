# MauiReactor
## Component-based UI Library built on top of .NET MAUI

[![MauiReactor 5 (beta)](https://img.shields.io/nuget/vpre/Reactor.Maui?label=MauiReactor%205&color=orange)](https://www.nuget.org/packages/Reactor.Maui/absoluteLatest)
[![MauiReactor 4](https://img.shields.io/nuget/v/Reactor.Maui)](https://www.nuget.org/packages/Reactor.Maui) 

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

### Versions

| Version | .NET / MAUI | Status | Branch |
|---|---|---|---|
| **MauiReactor 5** | .NET 11 / MAUI 11 | Beta | [`main`](https://github.com/adospace/reactorui-maui/tree/main) |
| **MauiReactor 4** | .NET 10 / MAUI 10 | Stable, maintained | [`version-4`](https://github.com/adospace/reactorui-maui/tree/version-4) |

#### What's new in MauiReactor 5 (beta)

- **.NET 11 / MAUI 11**: apps run on CoreCLR on Android, iOS and Mac Catalyst. The minimum Android version is now API 24 (Android 7.0), as required by .NET 11.
- **Hooks**: a new, lighter way to write components. There's no state class and no `SetState` round trip:
  ```csharp
  class CounterPage : HookComponent
  {
      public override VisualNode Render()
      {
          var (counter, setCounter) = UseState(0);

          return ContentPage("Counter Sample",
              VStack(
                  Label($"Counter: {counter}"),
                  Button("Click To Increment", () => setCounter(counter + 1))
              )
              .Spacing(10)
              .Center()
          );
      }
  }
  ```
  Also available: `UseReducer`, `UseRef`, `UseEffect` and `UseMemo`. Hook state is preserved across hot reload, and a built-in analyzer reports hooks that are called conditionally, in loops or outside `Render()`. `Component<TState>` still works as before.
- **New MAUI 11 features**: `LongPressGestureRecognizer`, badges on Shell items and toolbar items, `BoxView.Fill`, `Window.StatusBarTheme`, `SwipeItem` text/icon colors, and the new Maps clustering, styling and click events.
- **Keyed children**: `WithKey(...)` is now honoured when a list is reordered, so component state and native controls move with their item.

> ⚠️ **iOS / Mac Catalyst navigation needs testing.** MAUI 11 moved `NavigationPage`, `TabbedPage` and `FlyoutPage` to a new handler architecture on iOS and Mac Catalyst, and `Appearing` now fires *before* a page is pushed. MauiReactor's navigation relies on these events, so please test push/pop, modal navigation and Shell navigation on iOS and [open an issue](https://github.com/adospace/reactorui-maui/issues) if anything behaves differently from MauiReactor 4.

### Setting up MauiReactor from CLI

The steps below use MauiReactor 5 (beta). For MauiReactor 4, omit the version/`--prerelease` options and use `net10.0-*` target frameworks.

1. Install MauiReactor templates
```
dotnet new install Reactor.Maui.TemplatePack::5.0.0-beta.1
```

2. Install the MauiReactor hot reload console command (its version should match the Reactor.Maui package used by your app)
```
dotnet tool install -g Reactor.Maui.HotReloadConsole --prerelease
```
If you already have it installed, update it with:
```
dotnet tool update -g Reactor.Maui.HotReloadConsole --prerelease
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
dotnet build -t:Run -f net11.0-android
```
Under Mac, to target an iOS device/emulator, issue a command like this:
```
dotnet build -t:Run /p:_DeviceName=:v2:udid=<device_id> -f net11.0-ios
```
where the device id comes from this list:
```
xcrun simctl list
```

5. Hot-reload console (in a different shell)
```
dotnet-maui-reactor -f [net11.0-android|net11.0-ios|...]
```

6. Edits to code should be hot-reloaded by the application --> Enjoy!

### Documentation ###

[Documentation](https://adospace.gitbook.io/mauireactor/)


### Videos ###

[Introductionary video from Solution1 conference](https://www.youtube.com/watch?v=TSh9PL-ziY0&t=961s&ab_channel=C%23CommunityDiscord)  ![YouTube Video Views](https://img.shields.io/youtube/views/TSh9PL-ziY0?style=social)

[Interview with James Montemagno](https://www.youtube.com/watch?v=w_Km5AyreT0&ab_channel=dotnet)  ![YouTube Video Views](https://img.shields.io/youtube/views/w_Km5AyreT0?style=social)

[Getting started video from Gerald Versluis](https://www.youtube.com/watch?v=egklcAC9arY&ab_channel=GeraldVersluis)  ![YouTube Video Views](https://img.shields.io/youtube/views/egklcAC9arY?style=social)

[MauiReactor .NET MAUI UI July - 2024 Contribution - Task Manager App](https://www.youtube.com/watch?v=TSh9PL-ziY0&t=961s&ab_channel=C%23CommunityDiscord)  ![YouTube Video Views](https://img.shields.io/youtube/views/TSh9PL-ziY0?style=social)

### Sample Applications ###

[Main Samples Repository](https://github.com/adospace/mauireactor-samples)

[Rive App](https://github.com/adospace/rive-app)

[KeeMind App](https://github.com/adospace/kee-mind)

[Samples and test application](https://github.com/adospace/reactorui-maui/tree/main/samples)

### Star history

[![Star History Chart](https://api.star-history.com/svg?repos=adospace/reactorui-maui&type=Date)](https://star-history.com/#adospace/reactorui-maui&Date)

### How to contribute

- Star the repository!
- File an issue ([Issues](https://github.com/adospace/reactorui-maui/issues))
- Fix bugs, add features, or improve the code with PRs
- Help with the documentation ([Documentation Repo](https://github.com/adospace/reactorui-maui-docs))
