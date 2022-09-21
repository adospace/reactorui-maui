using MauiReactor.Animations;
using Microsoft.Maui.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

enum PageType
{
    Home,
    Favorites,
    Map,
    Settings
}

class LandscapePageState : IState
{
    public PageType Type { get; set; }
}

class LandscapePage : Component<LandscapePageState>
{
    private bool Landscape() => DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape;

    protected override void OnMounted()
    {
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        base.OnMounted();
    }

    private void DeviceDisplay_MainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        Invalidate(); 
    }

    protected override void OnWillUnmount()
    {
        DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
        base.OnWillUnmount();
    }

    public override VisualNode Render()
    {
        return new ContentPage
        {
            Landscape() ?
            RenderLandscape()
            :
            RenderPortrait()
        }
        .BackgroundColor(Color.Parse("#2B0B98"))
        .Set(MauiControls.Shell.NavBarIsVisibleProperty, false);
    }

    private VisualNode RenderPortrait()
    {
        return new Grid("64, *", "*")
        {
            new HorizontalTab()
                .PageType(State.Type)
                .OnTabItemChanged(newPageType => SetState(s => s.Type = newPageType)),

            new FadeIn()
            {
                new Label(State.Type)
                    .FontSize(24)
                    .TextColor(Colors.White)
            }
            .GridRow(1)
        };
    }

    private VisualNode RenderLandscape()
    {
        return new Grid("*", "64, *")
        {
            new VerticalTab()
                .PageType(State.Type)
                .OnTabItemChanged(newPageType => SetState(s => s.Type = newPageType)),

            new FadeIn()
            {
                new Label(()=>State.Type.ToString())
                    .FontSize(24)
                    .TextColor(Colors.White)
            }
            .GridColumn(1)    
        };
    }
}

class FadeInState : IState
{
    public double TransY { get; set; }

    public double Opacity { get; set; } = 1.0;
}

class FadeIn : Component<FadeInState>
{
    protected override void OnPropsChanged()
    {
        if (MauiControls.Application.Current == null)
        {
            return;
        }

        SetState(s =>
        {
            s.TransY = 10;
            s.Opacity = 0.0;
        });

        MauiControls.Application.Current.Dispatcher.Dispatch(() => SetState(s =>
        {
            s.TransY = 0.0;
            s.Opacity = 1.0;
        }));
        
        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new Grid("*", "*")
        {
            Children()
        }
        .HCenter()
        .VCenter()
        .TranslationY(State.TransY)
        .Opacity(State.Opacity)
        .WithAnimation();
    }
}

abstract class TabState : IState
{
    public PageType PageType { get; set; }
}

abstract class TabComponent<T> : Component<T> where T : TabState, new()
{
    protected static readonly Color _accentColor = Color.Parse("#F7B548");
    protected PageType _pageType;
    protected Action<PageType>? _tabItemChangedAction;

    public TabComponent<T> PageType(PageType pageType)
    {
        _pageType = pageType;
        return this;
    }

    public TabComponent<T> OnTabItemChanged(Action<PageType> tabItemChangedAction)
    {
        _tabItemChangedAction = tabItemChangedAction;
        return this;
    }

    protected Image RenderTabItem(PageType pageType)
    {
        var imageSource = "tab_home.png";
        switch (pageType)
        {
            case Pages.PageType.Map:
                imageSource = "tab_map.png";
                break;
            case Pages.PageType.Favorites:
                imageSource = "tab_favorites.png";
                break;
            case Pages.PageType.Settings:
                imageSource = "tab_settings.png";
                break;
        }

        return new Image(imageSource)
            .Aspect(Aspect.Center)
            .OnTapped(() => _tabItemChangedAction?.Invoke(pageType));
    }

}

class HorizontalTabState : TabState
{
    public float Left { get; set; }
    public float Right { get; set; }
    public float FromLeft { get; set; }
    public float FromRight { get; set; }
    public float TargetLeft { get; set; }
    public float TargetRight { get; set; }
    public double? Width { get; set; }
}

class HorizontalTab : TabComponent<HorizontalTabState>
{
    private MauiControls.Grid? _grid;

    protected override void OnMounted()
    {
        UpdateLinePositions();

        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        UpdateLinePositions();

        base.OnPropsChanged();
    }

    private void UpdateLinePositions()
    {
        SetState(s =>
        {
            if (s.Width != null)
            {
                var tabWidth = Math.Min(s.Width.Value / 4, 40.0);
                s.FromLeft = s.Left;
                s.FromRight = s.Right;
                s.TargetLeft = (float)(((int)_pageType * s.Width / 4) + (s.Width.Value / 4.0 - tabWidth) / 2.0f);
                s.TargetRight = (float)(((int)_pageType * s.Width / 4) + (s.Width.Value / 4.0 - tabWidth) / 2.0f + tabWidth);
            }
        });
    }

    public override VisualNode Render()
    {
        return new Grid(grid => _grid = grid)
        {
            RenderTabItem(Pages.PageType.Home),
            RenderTabItem(Pages.PageType.Favorites).GridColumn(1),
            RenderTabItem(Pages.PageType.Map).GridColumn(2),
            RenderTabItem(Pages.PageType.Settings).GridColumn(3),

            new GraphicsView()
                .HeightRequest(4)
                .GridColumnSpan(4)
                .VEnd()
                .OnDraw(OnDrawSelectionLine),

            new AnimationController
            {
                new ParallelAnimation
                {
                    new DoubleAnimation()
                        .StartValue(State.FromLeft)
                        .TargetValue(State.TargetLeft)
                        .OnTick(v=>SetState(a=> a.Left = (float)v))
                        .Duration(300)
                        .Easing(State.FromLeft > State.TargetLeft ? Easing.CubicOut : Easing.Linear),

                    new DoubleAnimation()
                        .StartValue(State.FromRight)
                        .TargetValue(State.TargetRight)
                        .OnTick(v=>SetState(a=> a.Right = (float)v))
                        .Duration(300)
                        .Easing(State.FromRight < State.TargetRight ? Easing.CubicOut : Easing.Linear),
                }
            }
            .IsEnabled(State.Left != State.TargetLeft || State.Right != State.TargetRight)
        }
        .OnSizeChanged(()=>
        {
            State.Width = _grid?.Width;
            UpdateLinePositions();
        })
        .Rows("*")
        .Columns("* * * *");
    }

    private void OnDrawSelectionLine(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = _accentColor;
        canvas.FillRectangle(new RectF(State.Left, 0.0f, State.Right - State.Left, dirtyRect.Height));
    }
}

class VarticalTabState : TabState
{
    public float Top { get; set; }
    public float Bottom { get; set; }
    public float FromTop { get; set; }
    public float FromBottom { get; set; }
    public float TargetTop { get; set; }
    public float TargetBottom { get; set; }
    public double? Height { get; set; }
}

class VerticalTab : TabComponent<VarticalTabState>
{
    private MauiControls.Grid? _grid;

    protected override void OnMounted()
    {
        UpdateLinePositions();

        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        UpdateLinePositions();

        base.OnPropsChanged();
    }

    private void UpdateLinePositions()
    {
        SetState(s =>
        {
            if (s.Height != null)
            {
                var tabHeight = Math.Min(s.Height.Value / 4, 40.0);
                s.FromTop = s.Top;
                s.FromBottom = s.Bottom;
                s.TargetTop = (float)(((int)_pageType * s.Height / 4) + (s.Height.Value / 4.0 - tabHeight) / 2.0f);
                s.TargetBottom = (float)(((int)_pageType * s.Height / 4) + (s.Height.Value / 4.0 - tabHeight) / 2.0f + tabHeight);
            }
        });
    }

    public override VisualNode Render()
    {
        return new Grid(grid => _grid = grid)
        {
            RenderTabItem(Pages.PageType.Home),
            RenderTabItem(Pages.PageType.Favorites).GridRow(1),
            RenderTabItem(Pages.PageType.Map).GridRow(2),
            RenderTabItem(Pages.PageType.Settings).GridRow(3),

            new GraphicsView()
                .WidthRequest(4)
                .GridRowSpan(4)
                .HEnd()
                .OnDraw(OnDrawSelectionLine),

            new AnimationController
            {
                new ParallelAnimation
                {
                    new DoubleAnimation()
                        .StartValue(State.FromTop)
                        .TargetValue(State.TargetTop)
                        .OnTick(v=>SetState(a=> a.Top = (float)v))
                        .Duration(300)
                        .Easing(State.FromTop > State.TargetTop ? Easing.CubicOut : Easing.Linear),

                    new DoubleAnimation()
                        .StartValue(State.FromBottom)
                        .TargetValue(State.TargetBottom)
                        .OnTick(v=>SetState(a=> a.Bottom = (float)v))
                        .Duration(300)
                        .Easing(State.FromBottom < State.TargetBottom ? Easing.CubicOut : Easing.Linear),
                }
            }
            .IsEnabled(State.Top != State.TargetTop || State.Bottom != State.TargetBottom)
        }
        .OnSizeChanged(() =>
        {
            State.Height = _grid?.Height;
            UpdateLinePositions();
        })
        .Columns("*")
        .Rows("* * * *");
    }

    private void OnDrawSelectionLine(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = _accentColor;
        canvas.FillRectangle(new RectF(0.0f, State.Top, dirtyRect.Width, State.Bottom - State.Top));
    }
}