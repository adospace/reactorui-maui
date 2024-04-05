using MauiReactor;
using Rearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.Resources.Styles;

static class AppTheme
{
    public static (bool IsDarkTheme, Action ToggleCurrentAppTheme) ThemeCapsule(ICapsuleHandle use)
    {
        var (isDarkTheme, setIsDarkTheme) = use.State(false);

        use.Effect(() =>
        {
            if (MauiControls.Application.Current != null)
            {
                MauiControls.Application.Current.UserAppTheme = isDarkTheme ?
                    Microsoft.Maui.ApplicationModel.AppTheme.Dark :
                    Microsoft.Maui.ApplicationModel.AppTheme.Light;
            }

            return () => { };
        }, [MauiControls.Application.Current, isDarkTheme]);

        return (isDarkTheme, () => setIsDarkTheme(!isDarkTheme));
    }

    public static bool IsDarkTheme(ICapsuleHandle use)
    {
        var (isDarkTheme, _) = use.Invoke(ThemeCapsule);
        return isDarkTheme;
    }

    public static Color DarkBackground { get; } = Color.FromArgb("#FF17171C");
    public static Color DarkText { get; } = Color.FromArgb("#FFFFFFFF");
    public static Color DarkButtonHighEmphasis { get; } = Color.FromArgb("#FF4B5EFC");
    public static Color DarkButtonMediumEmphasis { get; } = Color.FromArgb("#FF4E505F");
    public static Color DarkButtonLowEmphasis { get; } = Color.FromArgb("#FF2E2F38");
    public static Color LightBackground { get; } = Color.FromArgb("#FFF1F2F3");
    public static Color LightText { get; } = Color.FromArgb("#FF000000");
    public static Color LightButtonHighEmphasis { get; } = Color.FromArgb("#FF4B5EFC");
    public static Color LightButtonMediumEmphasis { get; } = Color.FromArgb("#FFD2D3DA");
    public static Color LightButtonLowEmphasis { get; } = Color.FromArgb("#FFFFFFFF");
    public static Color GeneralWhite { get; } = Color.FromArgb("#FFFFFFFF");


    public static Color Background(ICapsuleHandle use) =>
        IsDarkTheme(use) ? DarkBackground : LightBackground;

    public static Color Text(ICapsuleHandle use) =>
        IsDarkTheme(use) ? DarkText : LightText;

    public static Color ButtonHighEmphasisBackground(ICapsuleHandle use) =>
        IsDarkTheme(use) ? DarkButtonHighEmphasis : LightButtonHighEmphasis;

    public static Color ButtonMediumEmphasisBackground(ICapsuleHandle use) =>
        IsDarkTheme(use) ? DarkButtonMediumEmphasis : LightButtonMediumEmphasis;

    public static Color ButtonLowEmphasisBackground(ICapsuleHandle use) =>
        IsDarkTheme(use) ? DarkButtonLowEmphasis : LightButtonLowEmphasis;


    public static Label Label(ICapsuleHandle use, string text)
        => new Label(text)
            .TextColor(Text(use))
            .FontFamily("WorkSansLight");

    public static Label Label(ICapsuleHandle use, Func<string> textStateAction)
    => new Label(textStateAction)
            .TextColor(Text(use))
            .FontFamily("WorkSansLight");

    public static Button Button(ICapsuleHandle use, string text)
        => new Button(text)
            .FontFamily("WorkSansRegular")
            .TextColor(Text(use))
            .CornerRadius(24)
            .FontSize(32);

    public static Grid ImageButton(string imageSource, Color backgroundColor, Action? clickAction)
        => new()
        {
            new Button()
                .CornerRadius(24)
                .BackgroundColor(backgroundColor)
                .OnClicked(clickAction),

            new Image(imageSource)
                .BackgroundColor(Colors.Transparent)
                .HCenter()
                .VCenter()
                .Aspect(Aspect.Center) 
                .OnTapped(clickAction)
        };

    public static Button ButtonHighEmphasis(ICapsuleHandle use, string text)
        => Button(use, text)
            .TextColor(GeneralWhite)
            .BackgroundColor(ButtonHighEmphasisBackground(use));

    public static Button ButtonMediumEmphasis(ICapsuleHandle use, string text)
        => Button(use, text)
            .BackgroundColor(ButtonMediumEmphasisBackground(use));

    public static Button ButtonLowEmphasis(ICapsuleHandle use, string text)
        => Button(use, text)
            .BackgroundColor(ButtonLowEmphasisBackground(use));

    public static Grid ImageButtonHighEmphasis(
        ICapsuleHandle use, string imageSource, Action? clickAction)
        => ImageButton(imageSource, ButtonHighEmphasisBackground(use), clickAction);

    public static Grid ImageButtonMediumEmphasis(
        ICapsuleHandle use, string imageSource, Action? clickAction)
        => ImageButton(imageSource, ButtonMediumEmphasisBackground(use), clickAction);

    public static Grid ImageButtonLowEmphasis(
        ICapsuleHandle use, string imageSource, Action? clickAction)
        => ImageButton(imageSource, ButtonLowEmphasisBackground(use), clickAction);

}
