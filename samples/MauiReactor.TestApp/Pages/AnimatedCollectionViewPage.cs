using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

record ItemModel(string Name);


class AnimatedCollectionViewPage : Component
{
    static AnimatedCollectionViewPage()
    {
        ItemsSource = Enumerable.Range(1, 200)
            .Select(_ => new ItemModel($"Item{_}"))
            .ToArray();
    }
    
    static ItemModel[] ItemsSource { get; }

    public override VisualNode Render()
    {
        return new ContentPage("Animated 2")
        {
            new Grid("* * *", "*")
            {
                new CollectionView()
                    .ItemsSource(ItemsSource, RenderItem),

                new CollectionView()
                    .ItemsLayout(new HorizontalLinearItemsLayout())
                    .ItemsSource(ItemsSource, RenderItem)
                    .GridRow(1),

                new CollectionView()
                    .ItemsLayout(new VerticalGridItemsLayout().Span(4))
                    .ItemsSource(ItemsSource, RenderItem)
                    .GridRow(2),



                new Internals.FrameRateIndicator()
                    .VStart()
                    .HEnd()
                    .BackgroundColor(Colors.White)
            }
        };
    }

    private VisualNode RenderItem(ItemModel item)
        => new AnimatedItem
        {
            Border()
                .BackgroundColor(Color.Parse("#512BD4"))
                .Margin(4)
                .StrokeCornerRadius(8)
        };
}

class AnimatedItemState
{
    public double ScaleX { get; set; } = 0.8;

    public double ScaleY { get; set; } = 0.5;
}

class AnimatedItem : Component<AnimatedItemState>
{
    protected override void OnMountedOrPropsChanged()
    {
        State.ScaleX = 0.8;
        State.ScaleY = 0.5;
        MauiControls.Application.Current?.Dispatcher.Dispatch(() =>
        SetState(s =>
        {
            s.ScaleX = s.ScaleY = 1.0;
        }));
        base.OnMountedOrPropsChanged();
    }

    public override VisualNode Render() =>
        Grid([.. Children()])
            .ScaleX(State.ScaleX)
            .ScaleY(State.ScaleY)
            .WithAnimation(easing: Easing.CubicOut);

}
