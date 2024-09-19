using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.Controls.VisualStateManager;

namespace MauiReactor.TestApp.Resources.Styles;

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
    public static Color LightBackground { get; } = Color.FromArgb("#FFF1F2F3");
    public static Color LightText { get; } = Color.FromArgb("#FF000000");

    public static class Dark
    {
        public static Color Primary = Colors.Red;
        public static Color OnPrimary = Colors.White;
    }
    public static class Light
    {
        public static Color Primary = Colors.DarkGray;
        public static Color OnPrimary = Colors.White;
    }

    // define Schemes
    public static Color Primary => IsDarkTheme ? Dark.Primary : Light.Primary;
    public static Color OnPrimary => IsDarkTheme ? Dark.OnPrimary : Light.OnPrimary;

    protected override void OnApply()
    {
        ContentPageStyles.Default = _ => _
            .BackgroundColor(IsDarkTheme ? DarkBackground : LightBackground);

        LabelStyles.Default = _ => _
            .TextColor(IsDarkTheme ? DarkText : LightText);

        //Uncomment to test: https://github.com/adospace/reactorui-maui/issues/255
        //ButtonStyles.Default = _ => _
        //    .TextColor(OnPrimary)
        //    .FontSize(14)
        //    .BorderWidth(0)
        //    .CornerRadius(100)
        //    .MinimumHeightRequest(44)
        //    .MinimumWidthRequest(44)
        //    .Padding(24, 11)
        //    .VisualState(nameof(CommonStates), CommonStates.Normal, MauiControls.Button.BackgroundColorProperty, Primary)
        //    .VisualState(nameof(CommonStates), "Pressed", MauiControls.Button.BackgroundColorProperty, Primary.AddLuminosity(-0.1f))
        //    ;

    }
}
