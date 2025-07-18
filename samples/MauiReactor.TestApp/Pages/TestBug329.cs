using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

partial class TestBug329 : Component
{
    private MauiControls.Shell? _shellRef;

    protected override async void OnMounted()
    {
        base.OnMounted();
        var savedTheme = await SecureStorage.GetAsync("Theme");
        if (savedTheme != null)
        {
            Theme.UserTheme = Enum.Parse<Microsoft.Maui.ApplicationModel.AppTheme>(savedTheme);
        }
    }

    private void OnSizeChanged(object? sender, EventArgs args)
    {
        var window = ((Microsoft.Maui.Controls.Window)sender!);
        System.Diagnostics.Debug.WriteLine($"Window size changed to {window.Width}x{window.Height}");
        System.Diagnostics.Debug.WriteLine($"Window position changed to {window.X},{window.Y}");
    }

    public override VisualNode Render() =>
        Window(
            Shell(shellRef => _shellRef = shellRef,
                FlyoutItem("Number Generator", new ThemeSwitcherPage().ThemeChanged(Invalidate)),
                FlyoutItem("Decision Maker", new ContentPage().Title("Decision Maker")),
                FlyoutItem("Email Generator", new ContentPage().Title("Email Generator"))
            )
            .FlyoutFooter(
                Label()
                    .Text($"Version: {Microsoft.Maui.ApplicationModel.AppInfo.Current.VersionString}")
                    .HorizontalTextAlignment(TextAlignment.Center)
            )
            .ItemTemplate(RenderItemTemplate),

            TitleBar()
                .Title("Random Generator")
                .TrailingContent(
                    Button("Settings", async () => await _shellRef!.DisplayAlert("Test App", "Open Settings page!", "OK"))
                        .BackgroundColor(Colors.Transparent)
                        .BorderWidth(0)
                        .VerticalOptions(LayoutOptions.Center)
                )
        )
        .OnSizeChanged(OnSizeChanged);

    VisualNode RenderItemTemplate(MauiControls.BaseShellItem item)
        => Grid("68", "*",
            HStack(
                Label(item.Title)
                    .TextColor(Theme.IsLightTheme ? Colors.Black : Colors.White)
                    .VCenter()
                    .Margin(10, 0)
            )

        );
}

partial class ThemeSwitcherPage : Component
{
    [Prop]
    Action? _themeChanged;

    public override VisualNode Render()
    {
        return ContentPage("Number Generator",
            Button("Toggle Theme")
                .OnClicked(ToggleTheme)
                .Center()
        );
    }

    async Task ToggleTheme()
    {
        Theme.UserTheme = Theme.UserTheme == Microsoft.Maui.ApplicationModel.AppTheme.Light
            ? Microsoft.Maui.ApplicationModel.AppTheme.Dark
            : Microsoft.Maui.ApplicationModel.AppTheme.Light;

        await SecureStorage.SetAsync("Theme", Theme.UserTheme.ToString());

        _themeChanged?.Invoke();
    }
}