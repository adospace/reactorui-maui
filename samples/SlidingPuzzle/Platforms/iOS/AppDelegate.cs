﻿using Foundation;
using Microsoft.Maui.Hosting;

namespace SlidingPuzzle
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}