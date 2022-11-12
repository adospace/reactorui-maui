# MauiReactor
## Component-based UI Library built on top of Maui Controls inspired to ReactJs and Flutter
MauiReactor is the successor of ReactorUI for Xamarin-Forms (https://github.com/adospace/reactorui-xamarin)

[![Build status](https://ci.appveyor.com/api/projects/status/trl7dwvicfxn5at5?svg=true)](https://ci.appveyor.com/project/adospace/reactorui-maui)

MauiReactor Nuget Packages

[![Nuget](https://img.shields.io/nuget/v/Reactor.Maui)](https://www.nuget.org/packages/Reactor.Maui) 

[All Packages](https://www.nuget.org/packages?q=Reactor.Maui)

[Documentation (WIP)](https://adospace.gitbook.io/mauireactor/)

### Setting up MauiReactor from CLI

1. Install MauiReactor templates
[![Nuget](https://img.shields.io/nuget/v/Reactor.Maui.TemplatePack)](https://www.nuget.org/packages/Reactor.Maui.TemplatePack)
```
dotnet new --install Reactor.Maui.TemplatePack
```

2. Install MauiReactor hot reload console command
[![Nuget](https://img.shields.io/nuget/v/Reactor.Maui.HotReload)](https://www.nuget.org/packages/Reactor.Maui.HotReload)
```
dotnet tool install -g Reactor.Maui.HotReload --prerelease
```

3. Create a sample project
```
dotnet new maui-reactor-startup -o my-new-project
cd .\my-new-project\
```

4. Build & run the project (emulator or device must be running and configured)
```
dotnet build -t:Run -f net7.0-android
```

5. Hot-reload console
```
dotnet-maui-reactor -f net7.0-android
```

6. Edits to code should be hotreloaded by the application --> Enjoy!

