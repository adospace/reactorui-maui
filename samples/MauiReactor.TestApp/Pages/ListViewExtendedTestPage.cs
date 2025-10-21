using MauiReactor.TestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace MauiReactor.TestApp.Pages;

//class ListViewExtendedTestPageState
//{
//    public Person? SelectedPerson { get; set; }
//}

//class ListViewExtendedTestPage : Component<ListViewExtendedTestPageState>
//{
//    public override VisualNode Render()
//    {
//        return new ContentPage("ListView Extended")
//        {
//            new ListView(MauiControls.ListViewCachingStrategy.RecycleElement)
//                .IsGroupingEnabled(true)
//                .ItemsSource<ListView, GroupOfPerson, Person>(GroupOfPerson.All, RenderGroup, RenderItem)
//                .SeparatorVisibility(MauiControls.SeparatorVisibility.None)
//                .OnItemSelected(OnSelectedItem)
//                //NOTE: Header/Footer not working under .net 7
//                //https://github.com/dotnet/maui/issues/13560
//                //https://github.com/dotnet/maui/issues/12312
//                .Header(Label("Header"))
//                .Footer(Label("Footer"))
//        };
//    }

//    private void OnSelectedItem(object? sender, MauiControls.SelectedItemChangedEventArgs args)
//    {
//        SetState(s => s.SelectedPerson = args.SelectedItem as Person);
//    }

//    private ViewCell RenderGroup(GroupOfPerson person)
//    {
//        return ViewCell(
//            Label(person.Initial,
//                MenuFlyout(
//                    MenuFlyoutItem("MenuItem1")
//                        .OnClicked(()=>OnClickMenuItem("MenuItem1")),
//                    MenuFlyoutItem("MenuItem2")
//                        .OnClicked(()=>OnClickMenuItem("MenuItem2")),
//                    MenuFlyoutItem("MenuItem3")
//                        .OnClicked(()=>OnClickMenuItem("MenuItem3"))
//                )
//            )
//            .FontSize(14.0)
//            .FontAttributes(MauiControls.FontAttributes.Bold)
//            .Margin(5)
//            .BackgroundColor(Colors.LightGray)
//        );
//    }

//    private ViewCell RenderItem(Person person)
//    {
//        return ViewCell(
//        [
//            Label($"{person.FirstName} {person.LastName}")
//                .FontSize(14.0)
//                .FontAttributes(MauiControls.FontAttributes.Bold)
//                .Padding(5)
//                .VerticalTextAlignment(TextAlignment.Center)
//        ]);
//    }

//    private void OnClickMenuItem(string title)
//    {
//        ContainerPage?.DisplayAlertAsync("MauiReactor", $"Clicked menu {title}", "OK");
//    }

//}
