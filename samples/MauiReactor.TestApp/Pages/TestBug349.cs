using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MauiReactor.TestApp.Pages;

public class TestBug349HomePage : Component
{
    public override VisualNode Render() => NavigationPage(
        ContentPage("HomePage",
            ScrollView(
                VStack(
                        Label("Home Page")
                            .FontSize(26)
                            .HCenter()
                            .VCenter(),
                        Button("Go to Page 1")
                            .OnClicked(() => Navigation!.PushAsync<TestBug349Page1>())
                            .HCenter()
                    )
                    .VCenter()
                    .Spacing(25)
                    .Padding(30, 0)
            )
        )
    );
}

internal class TestBug349Page1 : Component
{
    protected override void OnWillUnmount()
    {
        Debug.WriteLine("Page 1 is unmounting");
    }

    public override VisualNode Render() => ContentPage("Page1",
        ScrollView(
            VStack(
                    Label("Page 1")
                        .FontSize(26)
                        .HCenter()
                        .VCenter(),
                    Button("Go to Page 2")
                        .OnClicked(() => Navigation!.PushAsync<TestBug349Page2>())
                        // .OnClicked(() => Navigation.PopAsync())
                        .HCenter()
                )
                .VCenter()
                .Spacing(25)
                .Padding(30, 0)
        )
    );
}

internal class TestBug349Page2 : Component
{
    protected override void OnWillUnmount()
    {
        Debug.WriteLine("Page 2 is unmounting");
    }

    public override VisualNode Render() => ContentPage("Page2",
        ScrollView(
            VStack(
                    Label("Page 2")
                        .FontSize(26)
                        .HCenter()
                        .VCenter(),
                    Button("Go to back")
                        .OnClicked(async () =>
                        {
                            // Variant 1
                            await Navigation.PopAsync();

                            // // Variant 2
                            //var page1 = Navigation.NavigationStack[^2];
                            //Navigation.RemovePage(page1);
                            //await Navigation.PopAsync();

                            // // Variant 3
                            //await Navigation.PopToRootAsync();
                        })
                        .HCenter()
                )
                .VCenter()
                .Spacing(25)
                .Padding(30, 0)
        )
    );
}

