﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages.Components
{
    class WindLiveWidgetState
    {
        public double NeedleRotation { get; set; } = 98;

        public bool TimerEnabled { get; set; }
    }

    class WindLiveWidget : Component<WindLiveWidgetState>
    {
        readonly static Random _rand = new();
        readonly double[] WindValues = { 98, 84, 140, 92, 55 };

        private double GetNeedleRotation() => WindValues[_rand.Next(0, WindValues.Length - 1)];

        public override VisualNode Render()
        {
            return new VerticalStackLayout
            {
                new Grid
                {
                    new Image("compass_background.png")
                        .HCenter()
                        .VCenter()
                        .WidthRequest(200)
                        .HeightRequest(200),

                    new Image("compass_needle.png")
                        .HCenter()
                        .VCenter()
                        .WidthRequest(200)
                        .HeightRequest(200)
                        .WithOutAnimation()
                        .Rotation(State.NeedleRotation)
                        .WithAnimation(Easing.SpringOut, duration: 200)
                        ,

                    new Timer(interval: 300, () => SetState(s => s.NeedleRotation = GetNeedleRotation(), true))
                        .IsEnabled(() => State.TimerEnabled)
                }
                .OnTapped(()=> SetState(s => s.TimerEnabled = !s.TimerEnabled)),

                new Label("Winds")
                    .HCenter()
                    .Class("SubContent"),

                new Label("14|25")
                    .HCenter()
                    .Class("Title")
            }
            .WidthRequest(200);
        }

    }
}
