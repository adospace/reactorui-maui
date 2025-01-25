using MauiReactor.TestApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp;

public class HomePage : Component
{
    public override VisualNode Render()
    {
        return new Shell
        {
            new FlyoutItem("Counter")
            {
                new CounterPage()
            },
            new FlyoutItem("Counter with service")
            {
                new CounterWithServicePage()
            },
            new FlyoutItem("Parameters")
            {
                new ParametersPage()
            },
            new FlyoutItem("CollectionView")
            {
                new CollectionViewPage()
            },
            new FlyoutItem("EditableCollectionView")
            {
                new EditableCollectionView()
            },
            new FlyoutItem("ListView")
            {
                new ListViewPage()
            },
            new FlyoutItem("ListView Extended Test")
            {
                new ListViewExtendedTestPage()
            },
            new FlyoutItem("AnimationBasics")
            {
                new AnimationBasics()
            },
            new FlyoutItem("CardsAnimation")
            {
                new CardsAnimationPage()
            },
            new FlyoutItem("CanvasCardsAnimationPage")
            {
                new CanvasCardsAnimationPage()
            },
            new FlyoutItem("Image")
            {
                new ImagePage()
            },
            new FlyoutItem("AnimatedCollectionView")
            {
                new AnimatedCollectionViewPage()
            },
            new FlyoutItem("AnimationShowcase")
            {
                new AnimationShowcasePage()
            },
            new FlyoutItem("AnimationLoop")
            {
                new AnimationLoopPage()
            },
            new FlyoutItem("Navigation")
            {
                new NavigationMainPage()
            },
            new FlyoutItem("ElementRef")
            {
                new ElementRefPage()
            },
            new FlyoutItem("Canvas")
            {
                new CanvasPage()
            },
            new FlyoutItem("Landscape")
            {
                new LandscapePage()
            },
            new FlyoutItem("GraphicsView")
            {
                new GraphicsViewPage()
            },
            new FlyoutItem("RemainingItemsThreshold Test")
            {
                new RemainingItemsThresholdTestPage()
            },
            new FlyoutItem("AnimatedButtonPage")
            {
                new AnimatedButtonPage()
            },
            new FlyoutItem("Border Corner Radius Test")
            {
                new BorderCornerRadiusPage()
            },
            new FlyoutItem("Show Popup Test")
            {
                new ShowPopupTestPage()
            },
            new FlyoutItem("Carousel Test")
            {
                new CarouselTestPage()
            },
            new FlyoutItem("Carousel with Images Test")
            {
                new CarouselTestWithImagesPage()
            },
            new FlyoutItem("Canvas AutoV/HStack Test")
            {
                new CanvasAutoStackPage()
            },
            new FlyoutItem("Inline components")
            {
                new InlineComponentsPage()
            },
            new FlyoutItem("Collection View Grouped")
            {
                new CollectionViewExtendedTestPage()
            },
            new FlyoutItem("Drag & Drop")
            {
                new DragDropTestPage()
            },

            new FlyoutItem("Gradient Test")
            {
                new GradientPage()
            },

            new FlyoutItem("Behavior Test")
            {
                new BehaviorTestPage()
            },

            new FlyoutItem("Pickers Page")
            {
                new PickerPage()
            },
            new FlyoutItem("Visual State Test Page")
            {
                new VisualStateTestPage()
            },
            new FlyoutItem("FormattedText Test Page")
            {
                new FormattedTextTestPage()
            },
            new FlyoutItem("Theming Test Page")
            {
                new ThemeTestPage()
            },
            new FlyoutItem("CollectionView Multiple Selection Test Page")
            {
                new CollectionViewSelectionPage()
            },
            new FlyoutItem("Animating Label Test Page")
            {
                new AnimatingLabelTestPage()
            },
        }
        .ItemTemplate(RenderItemTemplate);
    }

    static VisualNode RenderItemTemplate(MauiControls.BaseShellItem item)
        => new Grid("68", "*")
        {
            new Label(item.Title)
                .VCenter()
                .Margin(10,0)
        };
}
