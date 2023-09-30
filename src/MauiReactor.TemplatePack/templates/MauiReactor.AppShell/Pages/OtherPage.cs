using MauiReactor;

namespace MauiReactor.AppShell.Pages;

class OtherPage : Component
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new ScrollView
            {
                new VerticalStackLayout
                {
                    new Label("Other Page")
                        .FontSize(32)
                        .HCenter(),
                }
                .VCenter()
                .Spacing(25)
                .Padding(30, 0)
            }
        };
    }
}
