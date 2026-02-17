using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;
record Subscription(SubscriptionType Type, double MonthBill, DateOnly StartingDate);


enum SubscriptionType
{
    Spotify,

    [Display(Name = "You Tube Premium")]
    YouTube,

    [Display(Name = "Microsoft One Drive")]
    OneDrive,

    Netflix
}

class TestCollectionViewMemoryIssueState
{
    public bool Toggle { get;set; }

    public List<Subscription> Subscriptions { get; set; } = [
        new Subscription(SubscriptionType.Spotify, 5.99, DateOnly.FromDateTime(DateTime.Now)),
                    new Subscription(SubscriptionType.YouTube, 18.99, DateOnly.FromDateTime(DateTime.Now.AddDays(-2))),
                    new Subscription(SubscriptionType.OneDrive, 29.99, DateOnly.FromDateTime(DateTime.Now.AddDays(-5))),
                    new Subscription(SubscriptionType.Netflix, 9.99, DateOnly.FromDateTime(DateTime.Now.AddDays(-7)))
        ];
}

class TestCollectionViewMemoryIssue : Component<TestCollectionViewMemoryIssueState>
{
    public override VisualNode Render()
        => ContentPage(
            Grid(
                State.Toggle ? null :
                CollectionView()
                    .ItemsSource(State.Subscriptions, item => Label(item.Type))
                ,

                new Timer()
                    .Interval(2000)
                    .IsEnabled(true)
                    .OnTick(()=>
                    {
                        GC.Collect();
                        SetState(s => s.Toggle = !s.Toggle);
                    })
            )
        );

}
