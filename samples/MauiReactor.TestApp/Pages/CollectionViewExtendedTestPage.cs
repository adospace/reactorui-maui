using MauiReactor.TestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class CollectionViewExtendedTestPageState
{
    public Person? SelectedPerson { get; set; }
}

class CollectionViewExtendedTestPage : Component<CollectionViewExtendedTestPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage("Collection Extended Test (BETA)")
        {
            new CollectionView()
                .IsGrouped(true)
                .ItemsSource<CollectionView, GroupOfPerson, Person>(GroupOfPerson.All, RenderItem, groupHeaderTemplate: RenderHeader, groupFooterTemplate: RenderFooter)
                .OnSelected<CollectionView, Person>(OnSelectedItem)
        };
    }

    private void OnSelectedItem(Person? selectedPerson)
    {
        SetState(s => s.SelectedPerson = selectedPerson);
    }

    private VisualNode RenderHeader(GroupOfPerson group)
    {
        return new Label(group.Initial)
            .FontSize(14.0)
            .FontAttributes(MauiControls.FontAttributes.Bold)
            .Margin(5)
            .BackgroundColor(Colors.LightGray);
    }

    private VisualNode RenderFooter(GroupOfPerson group)
    {
        return new Label($"Count: {group.Count}")
            .FontSize(14.0)
            .FontAttributes(MauiControls.FontAttributes.Bold)
            .Margin(5)
            .BackgroundColor(Colors.LightGray);
    }

    private VisualNode RenderItem(Person person)
    {
        return new Label($"{person.FirstName} {person.LastName}")
            .FontSize(14.0)
            .FontAttributes(MauiControls.FontAttributes.Bold)
            .Padding(5)
            .VerticalTextAlignment(TextAlignment.Center);
    }

}
