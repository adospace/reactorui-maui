using MauiReactorTemplate.StartupSampleXaml.Framework;
using MauiReactorTemplate.StartupSampleXaml.Models;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;

namespace MauiReactorTemplate.StartupSampleXaml.Components.Main;

partial class ProjectCardView : Component
{
    [Prop]
    Project _project = default!;

    [Prop]
    bool _isBusy;

    public override VisualNode Render()
    {
        return Border(
            new SfShimmer
            {
                RenderContent()
            }
            .IsActive(_isBusy)
            .CustomView(RenderCustomView())
            .BackgroundColor(Colors.Transparent)
            .VFill()
        )
        .Style(ResourceHelper.GetResource<Style>("CardStyle"))
        .WidthRequest(200);
    }

    VStack RenderContent()
    {
        return VStack(spacing: 15,
            Image()
                .HStart()
                .Aspect(Aspect.Center)
                .Source(Icon(_project.Icon)),
            Label(_project.Name)
                .TextColor(ResourceHelper.GetResource<Color>("Gray400"))
                .FontSize(14)
                .TextTransform(TextTransform.Uppercase),
            Label(_project.Description)
                .LineBreakMode(LineBreakMode.WordWrap),
            HStack(spacing: 15,
                RenderTags().ToArray()
            )
        );
    }

    static FontImageSource Icon(string glyph)
        => new()
        {
            Glyph = glyph,
            FontFamily = "FluentUI",
            Color = Theme.IsLightTheme ?
                        ResourceHelper.GetResource<Color>("DarkOnLightBackground") :
                        ResourceHelper.GetResource<Color>("LightOnDarkBackground"),
            Size = ResourceHelper.GetResource<OnIdiom<double>>("IconSize")
        };

    IEnumerable<VisualNode> RenderTags()
    {
        foreach (var tag in _project?.Tags ?? Enumerable.Empty<Tag>())
        {
            yield return Border(
                Label(tag.Title)
                    .TextColor(Theme.IsLightTheme ?
                        ResourceHelper.GetResource<Color>("LightBackground") :
                        ResourceHelper.GetResource<Color>("DarkBackground"))
                    .FontSize(14)
                    .VCenter()
                    .VerticalTextAlignment(TextAlignment.Center)
            )
            .Padding(12, 0, 12, 8)
            .OnAndroid(_ => _.Padding(12, 0, 12, 0))
            .StrokeCornerRadius(16)
            .HeightRequest(32)
            .StrokeThickness(0)
            .Background(tag.DisplayColor);
        }
    }

    static VStack RenderCustomView()
    {
        return VStack(spacing: 15,
            BoxView()
                .CornerRadius(48)
                .WidthRequest(24)
                .HeightRequest(24)
                .Center()
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle")),
            BoxView()
                .HeightRequest(24)
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle")),
            BoxView()
                .HeightRequest(48)
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle")),
            BoxView()
                .HeightRequest(24)
                .Margin(new Thickness(0, 12))
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle"))
        );
    }
}
