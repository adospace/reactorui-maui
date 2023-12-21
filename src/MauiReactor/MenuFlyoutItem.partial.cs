using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class MenuFlyoutItem
{
    public MenuFlyoutItem(string text)
    {
        this.Text(text);
    }
}

public partial class Component
{
    public static MenuFlyoutItem MenuFlyoutItem(string text) =>
        GetNodeFromPool<MenuFlyoutItem>().Text(text);

}