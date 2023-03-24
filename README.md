# MauiReactor
## Component-based UI Library built on top of Maui Controls inspired to ReactJs and Flutter
MauiReactor is the successor of ReactorUI for Xamarin-Forms (https://github.com/adospace/reactorui-xamarin)

[![Build status](https://ci.appveyor.com/api/projects/status/trl7dwvicfxn5at5?svg=true)](https://ci.appveyor.com/project/adospace/reactorui-maui)

MauiReactor Nuget Packages

[![Nuget](https://img.shields.io/nuget/v/Reactor.Maui)](https://www.nuget.org/packages/Reactor.Maui) 

[Documentation (WIP)](https://adospace.gitbook.io/mauireactor/)

[Introductionary video from Solution1 conference](https://www.youtube.com/watch?v=TSh9PL-ziY0&t=961s&ab_channel=C%23CommunityDiscord)

[Interview with James Montemagno](https://www.youtube.com/watch?v=w_Km5AyreT0&ab_channel=dotnet)

[Getting started video from Gerald Versluis](https://www.youtube.com/watch?v=egklcAC9arY&ab_channel=GeraldVersluis)

### Setting up MauiReactor from CLI

1. Install MauiReactor templates
[![Nuget](https://img.shields.io/nuget/v/Reactor.Maui.TemplatePack)](https://www.nuget.org/packages/Reactor.Maui.TemplatePack)
```
dotnet new install Reactor.Maui.TemplatePack
```

2. Install MauiReactor hot reload console command
[![Nuget](https://img.shields.io/nuget/v/Reactor.Maui.HotReload)](https://www.nuget.org/packages/Reactor.Maui.HotReload)
```
dotnet tool install -g Reactor.Maui.HotReload
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
Under Mac, to target an iOS device/emulator, issue a command like this:
```
dotnet build -t:Run /p:_DeviceName=:v2:udid=<device_id> -f net7.0-ios
```
where the device id comes from this list:
```
xcrun simctl list
```

5. Hot-reload console
```
dotnet-maui-reactor -f [net7.0-android|net7.0-ios|...]
```

6. Edits to code should be hotreloaded by the application --> Enjoy!

[All Packages](https://www.nuget.org/packages?q=Reactor.Maui)
