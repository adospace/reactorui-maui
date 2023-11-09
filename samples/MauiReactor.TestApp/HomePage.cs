using MauiReactor.TestApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp;

class HomePage : Component
{
    public override VisualNode Render()
    {
        return new Shell
        {
            new FlyoutItem("Counter")
            {
                new Pages.CounterPage()
            },
            new FlyoutItem("Parameters")
            {
                new Pages.ParametersPage()
            },
            new FlyoutItem("CollectionView")
            {
                new Pages.CollectionViewPage()
            },
            new FlyoutItem("EditableCollectionView")
            {
                new Pages.EditableCollectionView()
            },
            new FlyoutItem("ListView")
            {
                new Pages.ListViewPage()
            },
            new FlyoutItem("ListView Extended Test")
            {
                new Pages.ListViewExtendedTestPage()
            },
            new FlyoutItem("AnimationBasics")
            {
                new Pages.AnimationBasics()
            },
            new FlyoutItem("CardsAnimation")
            {
                new Pages.CardsAnimationPage()
            },
            new FlyoutItem("CanvasCardsAnimationPage")
            {
                new Pages.CanvasCardsAnimationPage()
            },
            new FlyoutItem("Image")
            {
                new Pages.ImagePage()
            },
            new FlyoutItem("AnimatedCollectionView")
            {
                new Pages.AnimatedCollectionViewPage()
            },
            new FlyoutItem("AnimationShowcase")
            {
                new Pages.AnimationShowcasePage()
            },
            new FlyoutItem("AnimationLoop")
            {
                new Pages.AnimationLoopPage()
            },
            new FlyoutItem("Navigation")
            {
                new Pages.NavigationMainPage()
            },
            new FlyoutItem("ElementRef")
            {
                new Pages.ElementRefPage()
            },
            new FlyoutItem("Canvas")
            {
                new Pages.CanvasPage()
            },
            new FlyoutItem("Landscape")
            {
                new Pages.LandscapePage()
            },
            new FlyoutItem("GraphicsView")
            {
                new Pages.GraphicsViewPage()
            },
            new FlyoutItem("RemainingItemsThreshold Test")
            {
                new Pages.RemainingItemsThresholdTestPage()
            },
            new FlyoutItem("AnimatedButtonPage")
            {
                new Pages.AnimatedButtonPage()
            },
            new FlyoutItem("Border Corner Radius Test")
            {
                new Pages.BorderCornerRadiusPage()
            },
            new FlyoutItem("Show Popup Test")
            {
                new Pages.ShowPopupTestPage()
            },
            new FlyoutItem("Carousel Test")
            {
                new Pages.CarouselTestPage()
            },
            new FlyoutItem("Carousel with Images Test")
            {
                new Pages.CarouselTestWithImagesPage()
            },
            new FlyoutItem("Canvas AutoV/HStack Test")
            {
                new Pages.CanvasAutoStackPage()
            },
            new FlyoutItem("Inline components")
            {
                new Pages.InlineComponentsPage()
            },
            new FlyoutItem("Collection View Grouped")
            {
                new Pages.CollectionViewExtendedTestPage()
            },
            new FlyoutItem("Drag & Drop")
            {
                new Pages.DragDropTestPage()
            },
            
            new FlyoutItem("Gradient Test")
            {
                new Pages.GradientPage()
            },

            new FlyoutItem("Behavior Test")
            {
                new Pages.BehaviorTestPage()
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
