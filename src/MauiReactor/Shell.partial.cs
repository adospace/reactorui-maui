using MauiReactor.Internals;
using System.Collections;

namespace MauiReactor
{
    public partial interface IShell
    {
        Func<Microsoft.Maui.Controls.BaseShellItem, VisualNode>? ItemTemplate { get; set; }

        Func<Microsoft.Maui.Controls.MenuItem, VisualNode>? MenuItemTemplate { get; set; }

        VisualNode? FlyoutHeader { get; set; }

        VisualNode? FlyoutFooter { get; set; }

        VisualNode? FlyoutContent { get; set; }
    }

    public partial class Shell<T> : IEnumerable
    {
        VisualNode? IShell.FlyoutHeader { get; set; }
        VisualNode? IShell.FlyoutFooter { get; set; }
        VisualNode? IShell.FlyoutContent { get; set; }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            var thisAsIShell = (IShell)this;

            var children = base.RenderChildren();

            if (thisAsIShell.FlyoutHeader != null)
            {
                children = children.Concat(new[] { thisAsIShell.FlyoutHeader });
            }
            if (thisAsIShell.FlyoutFooter != null)
            {
                children = children.Concat(new[] { thisAsIShell.FlyoutFooter });
            }
            if (thisAsIShell.FlyoutContent != null)
            {
                children = children.Concat(new[] { thisAsIShell.FlyoutContent });
            }

            return children;
        }

        Func<Microsoft.Maui.Controls.BaseShellItem, VisualNode>? IShell.ItemTemplate { get; set; }
        Func<Microsoft.Maui.Controls.MenuItem, VisualNode>? IShell.MenuItemTemplate { get; set; }

        private class ItemTemplateNode : VisualNode, IVisualNode
        {
            private readonly ItemTemplatePresenter? _presenter = null;
            private readonly VisualNode _owner;

            public ItemTemplateNode(VisualNode root, ItemTemplatePresenter presenter, VisualNode owner)
            {
                _root = root;
                _presenter = presenter;
                _owner = owner;

                Invalidate();
            }

            private VisualNode _root;

            private VisualNode Root
            {
                get => _root;
                set
                {
                    if (_root == value) return;
                    _root = value;
                    Invalidate();
                }
            }

            Microsoft.Maui.Controls.Page? IVisualNode.GetContainerPage()
            {
                return ((IVisualNode)_owner).GetContainerPage();
            }

            IHostElement? IVisualNode.GetPageHost()
            {
                return ((IVisualNode)_owner).GetPageHost();
            }
            protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
            {
                Validate.EnsureNotNull(_presenter);

                if (nativeControl is View view)
                    _presenter.Content = view;
                else
                {
                    throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
                }
            }

            protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
            {
            }

            protected override IEnumerable<VisualNode> RenderChildren()
            {
                yield return Root;
            }

            protected internal override void OnLayoutCycleRequested()
            {
                Layout();
                base.OnLayoutCycleRequested();
            }
        }

        private class ItemTemplatePresenter : Microsoft.Maui.Controls.ContentView
        {
            private ItemTemplateNode? _itemTemplateNode;
            private readonly CustomDataTemplate _template;
            private readonly bool _useMenuItemTemplate;

            public ItemTemplatePresenter(CustomDataTemplate template, bool useMenuItemTemplate)
            {
                _template = template;
                _useMenuItemTemplate = useMenuItemTemplate;
            }

            protected override void OnBindingContextChanged()
            {
                if (BindingContext != null)
                {
                    var item = BindingContext;

                    var owner = _template.Owner;
                    var ownerAsIShell = (IShell)owner;
                    var newRoot = _useMenuItemTemplate ? 
                        //unfortunately seems there is not other way to get the underlying MenuItem, anyway shouldn't be a problem for performance as menu items are generally not so many
                        ownerAsIShell.MenuItemTemplate?.Invoke((Microsoft.Maui.Controls.MenuItem)(item.GetType().GetProperty("MenuItem").EnsureNotNull().GetValue(item).EnsureNotNull()))
                        :
                        ownerAsIShell.ItemTemplate?.Invoke((Microsoft.Maui.Controls.BaseShellItem)item);

                    if (newRoot != null)
                    {
                        _itemTemplateNode = new ItemTemplateNode(newRoot, this, _template.Owner);
                        _itemTemplateNode.Layout();
                    }                
                }

                base.OnBindingContextChanged();
            }
        }

        private class CustomDataTemplate
        {
            public DataTemplate DataTemplate { get; }
            public Shell<T> Owner { get; set; }

            public CustomDataTemplate(Shell<T> owner, bool useMenuItemTemplate)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this, useMenuItemTemplate));
            }
        }        

        private CustomDataTemplate? _customDataTemplate;
        private CustomDataTemplate? _customMenuItemDataTemplate;

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIShell = (IShell)this;

            if (thisAsIShell.ItemTemplate != null)
            {
                _customDataTemplate = new CustomDataTemplate(this, false);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }

            if (thisAsIShell.MenuItemTemplate != null)
            {
                _customMenuItemDataTemplate = new CustomDataTemplate(this, true);
                NativeControl.MenuItemTemplate = _customMenuItemDataTemplate.DataTemplate;
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIShell = (IShell)this;

            if (childControl is Microsoft.Maui.Controls.ShellItem shellItem)
            {
                NativeControl.Items.Insert(widget.ChildIndex, shellItem);
            }
            else if (childControl is Microsoft.Maui.Controls.ShellContent shellContent)
            {
                NativeControl.Items.Insert(widget.ChildIndex, shellContent);
            }
            else if (childControl is Microsoft.Maui.Controls.Page page)
            {
                var shellContentItem = new Microsoft.Maui.Controls.ShellContent() { Content = page };
                NativeControl.Items.Insert(widget.ChildIndex, shellContentItem);
            }
            else if (childControl is Microsoft.Maui.Controls.MenuItem menuItem)
            {
                NativeControl.Items.Insert(widget.ChildIndex, menuItem);
            }
            else if (childControl is Microsoft.Maui.Controls.ToolbarItem toolbarItem)
            {
                NativeControl.ToolbarItems.Add(toolbarItem);
            }
            else if (widget == thisAsIShell.FlyoutHeader)
            {
                NativeControl.FlyoutHeader = childControl;
            }
            else if (widget == thisAsIShell.FlyoutFooter)
            {
                NativeControl.FlyoutFooter = childControl;
            }
            else if (widget == thisAsIShell.FlyoutContent)
            {
                NativeControl.FlyoutContent = childControl;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIShell = (IShell)this;

            if (childControl is Microsoft.Maui.Controls.ShellItem shellItem)
            {
                NativeControl.Items.Remove(shellItem);
            }
            else if (childControl is Microsoft.Maui.Controls.ShellContent shellContent)
            {
                NativeControl.Items.Remove(shellContent);
            }
            else if (childControl is Microsoft.Maui.Controls.MenuItem menuItem)
            {
                NativeControl.Items.Remove(menuItem);
            }
            else if (childControl is Microsoft.Maui.Controls.Page page &&
                page.Parent is Microsoft.Maui.Controls.ShellContent parentShellContent)
            {
                NativeControl.Items.Remove(parentShellContent);
            }
            else if (childControl is Microsoft.Maui.Controls.ToolbarItem toolbarItem)
            {
                NativeControl.ToolbarItems.Remove(toolbarItem);
            }
            else if (widget == thisAsIShell.FlyoutHeader)
            {
                NativeControl.FlyoutHeader = null;
            }
            else if (widget == thisAsIShell.FlyoutFooter)
            {
                NativeControl.FlyoutFooter = null;
            }
            else if (widget == thisAsIShell.FlyoutContent)
            {
                NativeControl.FlyoutContent = null;
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            var newNodeAsShell = ((Shell<T>)newNode);

            newNodeAsShell._customDataTemplate = _customDataTemplate;
            if (newNodeAsShell._customDataTemplate != null)
            {
                newNodeAsShell._customDataTemplate.Owner = this;
            }

            newNodeAsShell._customMenuItemDataTemplate = _customMenuItemDataTemplate;
            if (newNodeAsShell._customMenuItemDataTemplate != null)
            {
                newNodeAsShell._customMenuItemDataTemplate.Owner = this;
            }

            base.OnMigrated(newNode);
        }

    }

    public partial class ShellExtensions
    {
        public static T ItemTemplate<T>(this T shell, Func<Microsoft.Maui.Controls.BaseShellItem, VisualNode> itemTemplate) where T : IShell
        {
            shell.ItemTemplate = itemTemplate;
            return shell;
        }

        public static T MenuItemTemplate<T>(this T shell, Func<Microsoft.Maui.Controls.MenuItem, VisualNode> menuItemTemplate) where T : IShell
        {
            shell.MenuItemTemplate = menuItemTemplate;
            return shell;
        }

        public static T FlyoutHeader<T>(this T shell, VisualNode fluoutHeader) where T : IShell
        {
            shell.FlyoutHeader = fluoutHeader;
            return shell;
        }

        public static T FlyoutFooter<T>(this T shell, VisualNode flyoutFooter) where T : IShell
        {
            shell.FlyoutFooter = flyoutFooter;
            return shell;
        }

        public static T FlyoutContent<T>(this T shell, VisualNode flyoutContent) where T : IShell
        {
            shell.FlyoutContent = flyoutContent;
            return shell;
        }
    }

    class ComponentShellRouteFactory<T> : RouteFactory where T : Component, new()
    {
        public override Element GetOrCreate()
        {
            if (MauiControlsShellExtensions._propsStack.Count > 0)
            {
                (Type PropsType, Action<object> PropsInitializer) = MauiControlsShellExtensions._propsStack.Peek();
                return PageHost<T>.CreatePage(Microsoft.Maui.Controls.Shell.Current.Navigation, PropsInitializer);
            }
            else
            {
                return PageHost<T>.CreatePage(Microsoft.Maui.Controls.Shell.Current.Navigation);
            }
        }

        public override Element GetOrCreate(IServiceProvider services) => GetOrCreate();
    }

    public static class Routing
    {
        public static void RegisterRoute<T>(string route) where T : Component, new()
        {
            Microsoft.Maui.Controls.Routing.UnRegisterRoute(route);
            Microsoft.Maui.Controls.Routing.RegisterRoute(route, new ComponentShellRouteFactory<T>());
        }
    }

    public static class MauiControlsShellExtensions
    {
        internal static Stack<(Type PropsType, Action<object> PropsInitialiazer)> _propsStack = new();

        public static async Task GoToAsync<P>(this Microsoft.Maui.Controls.Shell shell, string route, Action<P> propsInitializer) where P : new()
        {
            try
            {
                _propsStack.Push((typeof(P), new Action<object>(props =>
                {
                    if (props is P castedProps)
                    {
                        propsInitializer(castedProps);
                    }
                    else
                    {
                        var convertedProps = new P();
                        CopyObjectExtensions.CopyProperties(props, convertedProps);
                        propsInitializer(convertedProps);
                    }
                })));
                await shell.GoToAsync(route);
            }
            finally
            {
                _propsStack.Pop();
            }
        }
    }
}
