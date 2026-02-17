using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.Internals;

namespace MauiReactor.TestApp.Pages;

[Scaffold(typeof(CommunityToolkit.Maui.Behaviors.IconTintColorBehavior))]
partial class IconTintColorBehavior { }

//public partial interface IIconTintColorBehavior : IBehavior
//{
//    object? TintColor { get; set; }
//}

//public abstract partial class IconTintColorBehavior<T> : Behavior<T, Microsoft.Maui.Controls.View>, IIconTintColorBehavior where T : CommunityToolkit.Maui.Behaviors.IconTintColorBehavior, new()
//{
//    public IconTintColorBehavior()
//    {
//        IconTintColorBehaviorStyles.Default?.Invoke(this);
//    }

//    public IconTintColorBehavior(Action<T?> componentRefAction) : base(componentRefAction)
//    {
//        IconTintColorBehaviorStyles.Default?.Invoke(this);
//    }

//    object? IIconTintColorBehavior.TintColor { get; set; }

//    protected override void OnUpdate()
//    {
//        OnBeginUpdate();
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIIconTintColorBehavior = (IIconTintColorBehavior)this;
//        SetPropertyValue(NativeControl, global::CommunityToolkit.Maui.Behaviors.IconTintColorBehavior.TintColorProperty, thisAsIIconTintColorBehavior.TintColor);
//        base.OnUpdate();
//        OnEndUpdate();
//    }

//    protected override void OnAnimate()
//    {
//        OnBeginAnimate();
//        var thisAsIIconTintColorBehavior = (IIconTintColorBehavior)this;
//        AnimateProperty(global::CommunityToolkit.Maui.Behaviors.IconTintColorBehavior.TintColorProperty, thisAsIIconTintColorBehavior.TintColor);
//        base.OnAnimate();
//        OnEndAnimate();
//    }

//    partial void OnBeginUpdate();
//    partial void OnEndUpdate();
//    partial void OnBeginAnimate();
//    partial void OnEndAnimate();
//    protected override void OnThemeChanged()
//    {
//        if (ThemeKey != null && IconTintColorBehaviorStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
//        {
//            styleAction(this);
//        }

//        base.OnThemeChanged();
//    }
//}

//public partial class IconTintColorBehavior : IconTintColorBehavior<CommunityToolkit.Maui.Behaviors.IconTintColorBehavior>
//{
//    public IconTintColorBehavior()
//    {
//    }

//    public IconTintColorBehavior(Action<CommunityToolkit.Maui.Behaviors.IconTintColorBehavior?> componentRefAction) : base(componentRefAction)
//    {
//    }
//}

//public static partial class IconTintColorBehaviorExtensions
//{
//    public static T TintColor<T>(this T iconTintColorBehavior, Microsoft.Maui.Graphics.Color? tintColor)
//        where T : IIconTintColorBehavior
//    {
//        iconTintColorBehavior.TintColor = tintColor;
//        return iconTintColorBehavior;
//    }

//    public static T TintColor<T>(this T iconTintColorBehavior, Func<Microsoft.Maui.Graphics.Color?> tintColorFunc)
//        where T : IIconTintColorBehavior
//    {
//        iconTintColorBehavior.TintColor = new PropertyValue<Microsoft.Maui.Graphics.Color?>(tintColorFunc);
//        return iconTintColorBehavior;
//    }
//}

//public static partial class IconTintColorBehaviorStyles
//{
//    public static Action<IIconTintColorBehavior>? Default { get; set; }
//    public static Dictionary<string, Action<IIconTintColorBehavior>> Themes { get; } = [];
//}



class BehaviorTestPageState
{
    public Color Color { get; set; } = Colors.Red;
}

class BehaviorTestPage : Component<BehaviorTestPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage()
        {
            new VStack(spacing: 10)
            {
                new Image("shield.png")
                {
                    new IconTintColorBehavior()
                        .TintColor(State.Color)
                },

                new HStack(spacing: 5)
                {
                    new Button(nameof(Colors.Red), () => SetState(s => s.Color = Colors.Red)),
                    new Button(nameof(Colors.Green), () => SetState(s => s.Color = Colors.Green)),
                    new Button(nameof(Colors.Black), () => SetState(s => s.Color = Colors.Black)),
                }
                .HCenter()
            }
            .Center()
        };
    }
}

