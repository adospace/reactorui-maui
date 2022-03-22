using MauiReactor.Shapes;
using MauiReactor.WeatherTwentyOne.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages
{
    enum Units
    { 
        Imperial,

        Metric,

        Hybrid
    }

    class SettingsPageState : IState
    {
        public Units Units { get; set; } = Units.Imperial;

        public string Temperature => IsImperial ? "70˚F" : "21˚C";

        public bool IsImperial => Units == Units.Imperial;

        public bool IsMetric => Units == Units.Metric;

        public bool IsHybrid => Units == Units.Hybrid;
    }

    class SettingsPage : Component<SettingsPageState>
    {
        public override VisualNode Render()
        {
            return new ContentPage(title: "Settings")
            {
                new Grid("100,*", "*")
                { 
                    RenderHeader(),

                    RenderBody()
                        .GridRow(1)
                }
            };
        }

        Grid RenderHeader() 
            => new("*", "16,75,16,*,100,16")
            {
                new Image(new Uri("https://devblogs.microsoft.com/xamarin/wp-content/uploads/sites/44/2019/03/Screen-Shot-2017-01-03-at-3.35.53-PM-150x150.png"))
                    .GridColumn(1)
                    .WidthRequest(75)
                    .HeightRequest(75)
                    .Aspect(Aspect.AspectFill)
                    .VCenter()
                    .HCenter()
                    .Clip(new EllipseGeometry()
                        .Center(98,98)
                        .RadiusX(98)
                        .RadiusY(98)),

                new Label("David Ortinau")
                    .GridColumn(3)
                    .LineBreakMode(LineBreakMode.WordWrap)
                    .VCenter(),

                new Button("Sign Out", OnSignOut)
                    .GridColumn(4)
                    .HEnd()
                    .VCenter(),

                new BoxView()
                    .GridColumnSpan(6)
                    .Color(ThemeColors.NeutralDarker)
                    .HeightRequest(1)
                    .VEnd()
            };

        ScrollView RenderBody() 
            => new ScrollView
            {
                new VerticalStackLayout(spacing: 8)
                {
                    new VerticalStackLayout(spacing: 8)
                    {
                        new Image("fluent_weather_moon_16_filled.png")
                            .HeightRequest(115)
                            .Aspect(Aspect.AspectFit),
                        new Label(() => State.Temperature)
                            .HCenter()
                            .Class("Title1"),
                        new Border()
                            .HeightRequest(30)
                            .VStart()
                            .HCenter()
                            .Padding(15,0)
                            .Stroke(Brush.Transparent)
                            .BackgroundColor(ThemeColors.Primary)
                            .StrokeShape(new RoundRectangle().CornerRadius(60.0)),
                        new Label("Clear")
                            .HCenter()
                            .VCenter()
                            .Class("HeadLine")
                    },

                    new Label("Units")
                        .Class("SectionTitle"),

                    RenderUnits(Units.Imperial, "˚F / mph / miles / inches"),

                    RenderUnits(Units.Metric, "˚C / kmh / km / millimeters / milibars"),

                    RenderUnits(Units.Hybrid, "˚C / mph / miles / millimeters / millibars"),

                    new Label("More")
                        .Class("SectionTitle"),

                    new Label("Support")
                        .Class("Subhead")
                        .OnTapped(OnSupportTapped),

                    new BoxView()
                        .Class("HRule"),

                    new FlexLayout()
                    {
                        new Label("Dark Mode")
                            .Class("Subhead"),

                        new Switch()
                            .IsToggled(Application.Current?.UserAppTheme == AppTheme.Dark)
                            .OnToggled(OnSwitchTheme)
                    }
                    .HeightRequest(30)
                    .JustifyContent(Microsoft.Maui.Layouts.FlexJustify.SpaceBetween)
                }
            }
            .Margin(Device.Idiom == TargetIdiom.Phone ? 15.0 : 25.0);

        void OnSwitchTheme(object? sender, ToggledEventArgs args)
        {
            if (Application.Current != null)
            {
                Application.Current.UserAppTheme = args.Value ? AppTheme.Dark : AppTheme.Light;
            }
        }

        IEnumerable<VisualNode> RenderUnits(Units units, string label)
        {
            yield return new Grid("*,*", "*")
            {
                new TapGestureRecognizer(()=>SetState(s => s.Units = units)),

                new Label(units.ToString())
                    .Class("Subhead"),

                new Label(label)
                    .Class("SubContent")
                    .GridRow(1),

                new Image("checkmark_icon.png")
                    .GridRowSpan(2)
                    .Aspect(Aspect.Center)
                    .IsVisible(() => State.Units == units)
                    .HEnd()
                    .VCenter(),
            };
            
            yield return new BoxView()
                .Class("HRule");
        }

        async void OnSupportTapped()
        {
            if (ContainerPage == null)
            {
                return;
            }
            string action = await ContainerPage.DisplayActionSheet("Get Help", "Cancel", null, "Email", "Chat", "Phone");
            await ContainerPage.DisplayAlert("You Chose", action, "Okay");
        }
        async void OnSignOut()
        {
            if (ContainerPage == null)
            {
                return;
            }
            await ContainerPage.DisplayAlert("Sign Out", "Are you sure?", "Yes", "No");
        }
    }
}
