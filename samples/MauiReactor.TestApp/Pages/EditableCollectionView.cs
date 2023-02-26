using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class EditableCollectionViewState
{
    public ObservableCollection<string> Items { get; set; } = new();
}

class EditableCollectionView : Component<EditableCollectionViewState>
{
    static readonly Random _random = new();
    public override VisualNode Render()
    {
        return new ContentPage("CollectionView")
        {
            new Grid("48, 44, *", "*, *")
            {
                new Button("Add Item")
                    .OnClicked(()=>SetState(s => s.Items.Insert(_random.Next(0, s.Items.Count), $"Item {DateTime.Now}"), invalidateComponent: false)),

                new Button("Remove Item")
                    .OnClicked(()=>SetState(s => s.Items.RemoveAt(_random.Next(0, s.Items.Count - 1)), invalidateComponent: false))
                    .GridColumn(1)
                    .IsEnabled(()=>State.Items.Count > 0),

                new Label()
                    .Text(()=> $"{State.Items.Count} Items")
                    .GridRow(1)
                    .GridColumnSpan(2)
                    .HCenter()
                    .VCenter(),


                new CollectionView()
                    .ItemsSource(State.Items, _ => new Label(_))
                    .GridRow(2)
            }
        };
    }
}
