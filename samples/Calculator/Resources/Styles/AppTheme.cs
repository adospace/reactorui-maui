using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Resources.Styles;

class AppTheme : Theme
{
    public static void ToggleCurrentAppTheme()
    {
        if (MauiControls.Application.Current != null)
        {
            MauiControls.Application.Current.UserAppTheme = IsDarkTheme ? Microsoft.Maui.ApplicationModel.AppTheme.Light : Microsoft.Maui.ApplicationModel.AppTheme.Dark;
        }
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


    public static Color Background => IsDarkTheme ? DarkBackground : LightBackground;
    public static Color Text => IsDarkTheme ? DarkText : LightText;
    public static Color ButtonHighEmphasisBackground => IsDarkTheme ? DarkButtonHighEmphasis : LightButtonHighEmphasis;
    public static Color ButtonMediumEmphasisBackground => IsDarkTheme ? DarkButtonMediumEmphasis : LightButtonMediumEmphasis;
    public static Color ButtonLowEmphasisBackground => IsDarkTheme ? DarkButtonLowEmphasis : LightButtonLowEmphasis;

    public static class Selector
    {
        public const string HighEmphasis = nameof(HighEmphasis);
        public const string MediumEmphasis = nameof(MediumEmphasis);
        public const string LowEmphasis = nameof(LowEmphasis);
    }

    public static Button Button(string text)
        => new Button(text)
            .FontFamily("WorkSansRegular")
            .TextColor(Text)
            .CornerRadius(24)
            .FontSize(32);

    public static Grid ImageButton(string imageSource, Color backgroundColor, Action? clickAction)
        =>
        [
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
        ];

    public static Grid ImageButtonHighEmphasis(string imageSource, Action? clickAction)
        => ImageButton(imageSource, ButtonHighEmphasisBackground, clickAction);

    public static Grid ImageButtonMediumEmphasis(string imageSource, Action? clickAction)
        => ImageButton(imageSource, ButtonMediumEmphasisBackground, clickAction);

    public static Grid ImageButtonLowEmphasis(string imageSource, Action? clickAction)
        => ImageButton(imageSource, ButtonLowEmphasisBackground, clickAction);

    protected override void OnApply()
    {
        LabelStyles.Default = _ => _
            .FontFamily("WorkSansLight")
            .TextColor(Text);

        ButtonStyles.Default = _ => _
            .FontFamily("WorkSansRegular")
            .TextColor(Text)
            .CornerRadius(24)
            .FontSize(32);

        ButtonStyles.Themes[Selector.HighEmphasis] = _ => _
            .TextColor(GeneralWhite)
            .BackgroundColor(ButtonHighEmphasisBackground);

        ButtonStyles.Themes[Selector.MediumEmphasis] = _ => _
            .BackgroundColor(ButtonMediumEmphasisBackground);

        ButtonStyles.Themes[Selector.LowEmphasis] = _ => _
            .BackgroundColor(ButtonLowEmphasisBackground);
    }
}
