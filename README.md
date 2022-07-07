# MauiReactor
## Component-based UI Library built on top of Maui Controls inspired to ReactJs and Flutter
MauiReactor is the successor of ReactorUI for Xamarin-Forms (https://github.com/adospace/reactorui-xamarin)

[![Build status](https://ci.appveyor.com/api/projects/status/trl7dwvicfxn5at5?svg=true)](https://ci.appveyor.com/project/adospace/reactorui-maui)
![Nuget](https://img.shields.io/nuget/v/Reactor.Maui)


### Setting up MauiReactor from CLI

1. Install MauiReactor templates
```
dotnet new --install Reactor.Maui.TemplatePack
```

2. Install MauiReactor hot reload console command
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
dotnet build -t:Run -f net6.0-android
```

5. Hot-reload console
```
dotnet-maui-reactor
```

6. Edits to code should be hotreloaded by the application --> Enjoy!

