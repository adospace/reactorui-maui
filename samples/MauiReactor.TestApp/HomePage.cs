﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp
{


    internal class HomePage : Component
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
                new FlyoutItem("ListView")
                {
                    new Pages.ListViewPage()
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
                new FlyoutItem("AnimationBasics")
                {
                    new Pages.AnimationBasics()
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
            }
            .ItemTemplate(RenderItemTemplate);
        }

        static VisualNode RenderItemTemplate(Microsoft.Maui.Controls.BaseShellItem item)
            => new Grid("68", "*")
            {
                new Label(item.Title)
                    .VCenter()
                    .Margin(10,0)
            };

    }
}
