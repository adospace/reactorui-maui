using System;
using Shouldly;

namespace UnitTests;

// Reactor.Maui is marked trim/AOT-compatible. Where it still has to reach MAUI internals by name, it keeps
// them alive with [DynamicDependency("Member", "Type", "Assembly")] strings that nothing else checks: if
// MAUI renames the type or member, the dependency silently keeps nothing and only trimmed apps break.
// These tests pin those names against the MAUI version actually referenced.
public class TestTrimmingContracts
{
    [Test]
    public void ShellMenuItemTemplate_MenuShellItemStillExposesMenuItem()
    {
        // Matches [DynamicDependency(..., "Microsoft.Maui.Controls.MenuShellItem", "Microsoft.Maui.Controls")]
        // on Shell's menu item template presenter (Shell.partial.cs).
        var menuShellItemType = typeof(Microsoft.Maui.Controls.Shell).Assembly.GetType("Microsoft.Maui.Controls.MenuShellItem");

        menuShellItemType.ShouldNotBeNull();
        typeof(Microsoft.Maui.Controls.Shell).Assembly.GetName().Name.ShouldBe("Microsoft.Maui.Controls");

        var menuItemProperty = menuShellItemType.GetProperty("MenuItem");
        menuItemProperty.ShouldNotBeNull();
        typeof(Microsoft.Maui.Controls.MenuItem).IsAssignableFrom(menuItemProperty.PropertyType).ShouldBeTrue();
    }
}
