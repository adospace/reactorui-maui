using System.Linq;
using MauiReactor;
using Shouldly;

namespace UnitTests;

public class TestShellMenuItemTemplate
{
    [Test]
    public void ShellMenuItemTemplate_PresenterRendersTheMenuItemTemplate()
    {
        var mainPage = TemplateHost.Create(new ShellWithMenuItem());
        var shell = mainPage.Find<MauiControls.Shell>("MainShell");
        // The flyout binds each menu item presenter to the ShellItem that wraps the MenuItem
        var menuShellItem = shell.Items.Single(item => item.GetType().GetProperty("MenuItem") != null);

        var presenter = (MauiControls.ContentView)shell.MenuItemTemplate.CreateContent();
        presenter.BindingContext = menuShellItem;

        ((MauiControls.Label)presenter.Content).Text.ShouldBe("Menu: Logout");
    }

    private class ShellWithMenuItem : Component
    {
        public override VisualNode Render()
            => new Shell
                {
                    new ShellContent("Home")
                        .RenderContent(() => new ContentPage()),
                    MenuItem()
                        .Text("Logout")
                }
                .ItemTemplate(item => new Label($"Item: {item.Title}"))
                .MenuItemTemplate(menuItem => new Label($"Menu: {menuItem.Text}"))
                .AutomationId("MainShell");
    }
}
