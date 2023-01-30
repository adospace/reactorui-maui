using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class RemainingItemsThresholdTestPageState
{
    public bool IsBusy { get; set; }
    public List<int> Ints { get; set; } = Enumerable.Range(0, 50).ToList();
}
class RemainingItemsThresholdTestPage : Component<RemainingItemsThresholdTestPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage()
        {
            new Grid("*", "*")
            {
                new CollectionView()
                    .ItemsSource(State.Ints, i => new Label(i))
                    .RemainingItemsThreshold(5)
                    .OnRemainingItemsThresholdReached(moreInts),

                new ActivityIndicator()
                    .IsRunning(State.IsBusy)
                    .HCenter()
                    .VCenter()
            }
        };
    }

    async void moreInts()
    {
        if (State.IsBusy)
        {
            return;
        }
        SetState(s => s.IsBusy = true);
        await Task.Delay(1000);
        SetState(s =>
        {
            s.Ints.AddRange(Enumerable.Range(s.Ints.Count, s.Ints.Count + 50));
            s.IsBusy = false;
        });
    }
}
