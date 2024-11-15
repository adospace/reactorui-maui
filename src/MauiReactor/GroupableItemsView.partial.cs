using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial interface IGroupableItemsView
{
    Func<object, VisualNode>? GroupHeaderTemplate { get; set; }
    Func<object, VisualNode>? GroupFooterTemplate { get; set; }

}

public partial class GroupableItemsView<T> : SelectableItemsView<T>, IGroupableItemsView where T : Microsoft.Maui.Controls.GroupableItemsView, new()
{
    Func<object, VisualNode>? IGroupableItemsView.GroupHeaderTemplate { get; set; }
    Func<object, VisualNode>? IGroupableItemsView.GroupFooterTemplate { get; set; }


    private class HeaderDataTemplate : CustomDataTemplate
    {
        private readonly GroupableItemsView<T> _owner;

        public HeaderDataTemplate(GroupableItemsView<T> owner)
            : base(owner)
        {
            _owner = owner;
        }

        public override VisualNode? GetVisualNodeForItem(object item)
        {
            return ((IGroupableItemsView)_owner).GroupHeaderTemplate?.Invoke(item);
        }
    }

    private class FooterDataTemplate : CustomDataTemplate
    {
        private readonly GroupableItemsView<T> _owner;

        public FooterDataTemplate(GroupableItemsView<T> owner)
            : base (owner)
        {
            _owner = owner;
        }

        public override VisualNode? GetVisualNodeForItem(object item)
        {
            return ((IGroupableItemsView)_owner).GroupFooterTemplate?.Invoke(item);
        }
    }

    private HeaderDataTemplate? _headerDataTemplate;

    private FooterDataTemplate? _footerDataTemplate;

    partial void OnBeginUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIItemsView = (IGroupableItemsView)this;

        if (NativeControl.ItemsSource == thisAsIItemsView.ItemsSource)
        {
            if (_headerDataTemplate != null)
            {
                _headerDataTemplate.Owner = this;

                _headerDataTemplate.Update();
            }

            if (_footerDataTemplate != null)
            {
                _footerDataTemplate.Owner = this;
                _footerDataTemplate.Update();
            }
        }
        else if (thisAsIItemsView.ItemsSource != null)
        {
            if (thisAsIItemsView.GroupHeaderTemplate != null)
            {
                _headerDataTemplate = new HeaderDataTemplate(this);
                NativeControl.GroupHeaderTemplate = _headerDataTemplate.DataTemplate;
            }

            if (thisAsIItemsView.GroupFooterTemplate != null)
            {
                _footerDataTemplate = new FooterDataTemplate(this);
                NativeControl.GroupFooterTemplate = _footerDataTemplate.DataTemplate;
            }
        }
        else
        {
            NativeControl.ItemsSource = null;
            NativeControl.ItemTemplate = null;
            _headerDataTemplate = null;
            _footerDataTemplate = null;
        }
    }

    protected override void OnMigrated(VisualNode newNode)
    {
        var newItemsView = ((GroupableItemsView<T>)newNode);
        newItemsView._headerDataTemplate = _headerDataTemplate;
        if (newItemsView._headerDataTemplate != null)
        {
            newItemsView._headerDataTemplate.Owner = ((GroupableItemsView<T>)newNode);
        }

        newItemsView._footerDataTemplate = _footerDataTemplate;
        if (newItemsView._footerDataTemplate != null)
        {
            newItemsView._footerDataTemplate.Owner = ((GroupableItemsView<T>)newNode);
        }

        base.OnMigrated(newNode);
    }

    protected override void OnUnmount()
    {
        Validate.EnsureNotNull(NativeControl);
        NativeControl.GroupHeaderTemplate = null;
        NativeControl.GroupFooterTemplate = null;

        base.OnUnmount();
    }
}

public static partial class GroupableItemsViewExtensions
{
    public static T ItemsSource<T, TGroupItem, TItem>(this T itemsView, 
        IEnumerable<TGroupItem> itemsSource,
        Func<TItem, VisualNode> template,
        Func<TGroupItem, VisualNode>? groupHeaderTemplate = null, 
        Func<TGroupItem, VisualNode>? groupFooterTemplate = null
        ) where T : IGroupableItemsView
    {
        itemsView.ItemsSource = itemsSource;

        itemsView.ItemTemplate = new Func<object, VisualNode>(item => template((TItem)item));
        itemsView.GroupHeaderTemplate = groupHeaderTemplate == null ? null : new Func<object, VisualNode>(item => groupHeaderTemplate((TGroupItem)item));
        itemsView.GroupFooterTemplate = groupFooterTemplate == null ? null : new Func<object, VisualNode>(item => groupFooterTemplate((TGroupItem)item));

        return itemsView;
    }
}
