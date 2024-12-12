using CommunityToolkit.Maui.Core;
using MauiReactor;
using MauiReactor.Shapes;
using MauiReactorTemplate.StartupSampleXaml.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactorTemplate.StartupSampleXaml.Resources.Styles;

class ApplicationTheme : Theme
{
    public static Color Primary { get; } = Color.FromRgba(81, 43, 212, 255); // #512BD4
    public static Color PrimaryDark { get; } = Color.FromRgba(172, 153, 234, 255); // #AC99EA
    public static Color PrimaryDarkText { get; } = Color.FromRgba(36, 36, 36, 255); // #242424
    public static Color Secondary { get; } = Color.FromRgba(223, 216, 247, 255); // #DFD8F7
    public static Color SecondaryDarkText { get; } = Color.FromRgba(152, 128, 229, 255); // #9880E5
    public static Color Tertiary { get; } = Color.FromRgba(43, 11, 152, 255); // #2B0B98

    public static Color White { get; } = Colors.White; // #FFFFFF
    public static Color Black { get; } = Colors.Black; // #000000
    public static Color Magenta { get; } = Color.FromRgba(214, 0, 170, 255); // #D600AA
    public static Color MidnightBlue { get; } = Color.FromRgba(25, 6, 73, 255); // #190649
    public static Color OffBlack { get; } = Color.FromRgba(31, 31, 31, 255); // #1F1F1F
    public static Color OffWhite { get; } = Color.FromRgba(241, 241, 241, 255); // #F1F1F1

    public static Color Gray100 { get; } = Color.FromRgba(225, 225, 225, 255); // #E1E1E1
    public static Color Gray200 { get; } = Color.FromRgba(200, 200, 200, 255); // #C8C8C8
    public static Color Gray300 { get; } = Color.FromRgba(172, 172, 172, 255); // #ACACAC
    public static Color Gray400 { get; } = Color.FromRgba(145, 145, 145, 255); // #919191
    public static Color Gray500 { get; } = Color.FromRgba(110, 110, 110, 255); // #6E6E6E
    public static Color Gray600 { get; } = Color.FromRgba(64, 64, 64, 255); // #404040
    public static Color Gray900 { get; } = Color.FromRgba(33, 33, 33, 255); // #212121
    public static Color Gray950 { get; } = Color.FromRgba(20, 20, 20, 255); // #141414

    public static Brush PrimaryBrush { get; } = new SolidColorBrush(Primary);
    public static Brush SecondaryBrush { get; } = new SolidColorBrush(Secondary);
    public static Brush TertiaryBrush { get; } = new SolidColorBrush(Tertiary);
    public static Brush WhiteBrush { get; } = new SolidColorBrush(White);
    public static Brush BlackBrush { get; } = new SolidColorBrush(Black);
    public static Brush Gray100Brush { get; } = new SolidColorBrush(Gray100);
    public static Brush Gray200Brush { get; } = new SolidColorBrush(Gray200);
    public static Brush Gray300Brush { get; } = new SolidColorBrush(Gray300);
    public static Brush Gray400Brush { get; } = new SolidColorBrush(Gray400);
    public static Brush Gray500Brush { get; } = new SolidColorBrush(Gray500);
    public static Brush Gray600Brush { get; } = new SolidColorBrush(Gray600);
    public static Brush Gray900Brush { get; } = new SolidColorBrush(Gray900);
    public static Brush Gray950Brush { get; } = new SolidColorBrush(Gray950);



    protected override void OnApply()
    {
        //Define additional styles here
        ButtonStyles.Themes["AddButton"] = _ => _
            .ImageSource(ResourceHelper.GetResource<ImageSource>("IconAdd"))
            .BackgroundColor(ResourceHelper.GetResource<Color>("Primary"))
            .CornerRadius(30)
            .HeightRequest(60)
            .WidthRequest(60)
            .VEnd()
            .HEnd()
            .Margin(3);

        ContentPageStyles.Default = _ => _
            .AddChildren(
                new StatusBarBehavior()
                    .StatusBarColor(IsLightTheme ?
                            ResourceHelper.GetResource<Color>("LightBackground") :
                            ResourceHelper.GetResource<Color>("DarkBackground"))
                    .StatusBarStyle(IsLightTheme ?
                        StatusBarStyle.DarkContent :
                        StatusBarStyle.LightContent)
            );
    }
}
