using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class EditableCollectionViewState
{
    public ObservableCollection<(Guid Id, string Name)> Items { get; set; } = new();

    public Guid? SelectedItemId { get; set; }
}

class EditableCollectionView : Component<EditableCollectionViewState>
{
    static readonly Random _random = new();

    protected override void OnMounted()
    {
        State.Items = new(Enumerable.Range(1, 10).Select(_ => (Id: Guid.NewGuid(), Name: $"Item {_}")));
        base.OnMounted();
    }

    public override VisualNode Render()
    {
        return new ContentPage("CollectionView")
        {
            new Grid("48, 44, *", "*, *")
            {
                new Button("Add Item")
                    .OnClicked(()=>SetState(s => s.Items.Add((Id: Guid.NewGuid(), $"Item {s.Items.Count + 1}")), invalidateComponent: false)),

                new Button("Remove Item")
                    .OnClicked(()=>SetState(s => s.Items.RemoveAt(_random.Next(0, s.Items.Count - 1)), invalidateComponent: false))
                    .GridColumn(1)
                    .IsEnabled(()=>State.Items.Count > 0),

                new Label()
                    .Text(()=> $"{State.Items.Count} Items (Swipe right to delete the item)")
                    .GridRow(1)
                    .GridColumnSpan(2)
                    .HCenter()
                    .VCenter(),


                new CollectionView()
                    .ItemsSource(State.Items, RenderItem)
                    .GridRow(2)
                    .GridColumnSpan(2)
            }
        };
    }

    private VisualNode RenderItem((Guid Id, string Name) item)
    {
        return new Item()
            .Name(item.Name)
            .ShowActions(State.SelectedItemId == item.Id)
            .OnShowActions(() => SetState(s => s.SelectedItemId = item.Id))
            .OnRemove(() => SetState(s => s.Items.Remove(item)));
    }
}

class ItemState
{
    public bool ShowActions { get; set; }
}

class Item : Component<ItemState>
{
    private string? _name;
    private bool _showActions;
    private Action? _showActionsAction;
    private Action? _removeAction;

    public Item Name(string name)
    {
        _name = name;
        return this;
    }

    public Item ShowActions(bool showActions)
    {
        _showActions = showActions;
        return this;
    }

    public Item OnShowActions(Action action)
    {
        _showActionsAction = action;
        return this;
    }

    public Item OnRemove(Action action)
    {
        _removeAction = action;
        return this;
    }

    protected override void OnPropsChanged()
    {
        if (_showActions != State.ShowActions)
        {
            MauiControls.Application.Current?.Dispatcher.Dispatch(() =>
            {
                SetState(s => s.ShowActions = _showActions);
            });
        }
        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new Grid()
        {
            new Button("Remove")
                .HStart()
                .Margin(5)
                .OnClicked(_removeAction),

            new VStack
            {
                new Label(_name)
                    .VCenter(),
            }
            .TranslationX(State.ShowActions ? 100 : 0)
            .WithAnimation(duration: 100)
            .OnSwiped(OnSwipedRight, direction: SwipeDirection.Right, threshold: 10)
            .Padding(5)
            .BackgroundColor(Colors.WhiteSmoke)
        }
        .BackgroundColor(Colors.Red);
    }

    private void OnSwipedRight()
    {
        SetState(s =>
        {
            s.ShowActions = true;
        });

        MauiControls.Application.Current?.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(300), () =>
                    _showActionsAction?.Invoke());
    }
}