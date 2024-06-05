using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public abstract class Theme
{
    public static AppTheme CurrentAppTheme => UserTheme == AppTheme.Unspecified ? RequestedTheme : UserTheme;

    public static AppTheme RequestedTheme => Application.Current?.RequestedTheme ?? AppTheme.Unspecified;

    public static AppTheme UserTheme => Application.Current?.UserAppTheme ?? AppTheme.Unspecified;

    public static bool IsDarkTheme => CurrentAppTheme == AppTheme.Dark;
    public static bool IsLightTheme => CurrentAppTheme == AppTheme.Light;

    internal void Apply()
    {
        OnApply();
    }

    protected abstract void OnApply();
}
