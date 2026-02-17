using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.Controls.VisualStateManager;

namespace MauiReactor.TestApp.Pages
{
    class CollectionViewPage : Component
    {
        static CollectionViewPage()
        {
            ItemsSource = Enumerable.Range(1, 200)
                .Select(_ => new Tuple<string, string>($"Item{_}", $"Details{_}"))
                .ToArray();
        }
        
        static Tuple<string, string>[] ItemsSource { get; }

        public override VisualNode Render()
        {
            return new ContentPage("CollectionView")
            {
                new CollectionView()
                    .AutomationId("list") //AutomationId used for test
                    .ItemSizingStrategy(MauiControls.ItemSizingStrategy.MeasureFirstItem)
                    .SelectionMode(MauiControls.SelectionMode.Single)
                    .ItemsSource(ItemsSource, RenderItem)
                    .ItemVisualState(nameof(CommonStates), CommonStates.Normal, MauiControls.VisualElement.BackgroundColorProperty, Colors.Transparent)
                    .ItemVisualState(nameof(CommonStates), CommonStates.Selected, MauiControls.VisualElement.BackgroundColorProperty, Colors.LightCoral)
            };
        }

        private VisualNode RenderItem(Tuple<string, string> item)
            => SwipeView(
                VStack(spacing: 5,
                    Label(item.Item1).AutomationId(item.Item1), //AutomationId used for test
                    Label(item.Item2)

                ).AutomationId($"Container_{item.Item1}")
               )
               .RightItems([
                 SwipeItemView(
                    HStack(
                        Label("Custom Swipte Item")
                        )
                    )
               ]);
    }
}
