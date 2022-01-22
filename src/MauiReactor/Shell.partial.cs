using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial interface IShell
    {
        Func<Microsoft.Maui.Controls.BaseShellItem, VisualNode>? ItemTemplate { get; set; }
    }

    public partial class Shell<T> : IEnumerable
    {
        Func<Microsoft.Maui.Controls.BaseShellItem, VisualNode>? IShell.ItemTemplate { get; set; }

        private readonly Dictionary<BindableObject, Microsoft.Maui.Controls.ShellItem> _elementItemMap = new();
        
        private readonly Dictionary<BindableObject, Microsoft.Maui.Controls.ToolbarItem> _elementToolbarItemMap = new();

        private class ItemTemplateNode : VisualNode, IHostElement
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

            private IHostElement GetPageHost()
            {
                var current = _owner;
                while (current != null && current is not IHostElement)
                    current = current.Parent;

                return Validate.EnsureNotNull(current as IHostElement);
            }

            public VisualNode Root
            {
                get => _root;
                set
                {
                    if (_root != value)
                    {
                        _root = value;
                        Invalidate();
                    }
                }
            }

            public Microsoft.Maui.Controls.Page? ContainerPage => GetPageHost().ContainerPage;

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

            public IHostElement Run()
            {
                var ownerPageHost = GetPageHost();
                if (ownerPageHost == null)
                {
                    throw new NotSupportedException();
                }

                return ownerPageHost.Run();
            }

            public void Stop()
            {
                var ownerPageHost = GetPageHost();
                if (ownerPageHost == null)
                {
                    throw new NotSupportedException();
                }

                ownerPageHost.Stop();
            }
        }

        private class ItemTemplatePresenter : Microsoft.Maui.Controls.ContentView
        {
            private ItemTemplateNode? _itemTemplateNode;
            private readonly CustomDataTemplate _template;

            public ItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                if (BindingContext != null)
                {
                    var item = (Microsoft.Maui.Controls.BaseShellItem)BindingContext;
                    if (item != null)
                    {
                        var layout = (IShell)_template.Owner;
                        if (layout.ItemTemplate != null)
                        {
                            var newRoot = layout.ItemTemplate(item);
                            if (newRoot != null)
                            {
                                _itemTemplateNode = new ItemTemplateNode(newRoot, this, _template.Owner);
                                _itemTemplateNode.Layout();
                            }
                        }
                    }
                }

                base.OnBindingContextChanged();
            }
        }

        private class CustomDataTemplate
        {
            public DataTemplate DataTemplate { get; }
            public Shell<T> Owner { get; set; }

            public CustomDataTemplate(Shell<T> owner)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
            }
        }

        private CustomDataTemplate? _customDataTemplate;

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIShell = (IShell)this;

            if (thisAsIShell.ItemTemplate != null)
            {
                _customDataTemplate = new CustomDataTemplate(this);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }
            else
            {
                NativeControl.ItemTemplate = null;
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.ShellItem shellItem)
            {
                NativeControl.Items.Insert(widget.ChildIndex, shellItem);
                _elementItemMap[childControl] = shellItem;
            }
            else if (childControl is Microsoft.Maui.Controls.Page page)
            {
                var shellContentItem = new Microsoft.Maui.Controls.ShellContent() { Content = page };
                NativeControl.Items.Insert(widget.ChildIndex, shellContentItem);
                _elementItemMap[childControl] = shellContentItem;
            }
            else if (childControl is Microsoft.Maui.Controls.ToolbarItem toolbarItem)
            {
                NativeControl.ToolbarItems.Add(toolbarItem);
                _elementToolbarItemMap[childControl] = toolbarItem;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (_elementItemMap.TryGetValue(childControl, out var item))
                NativeControl.Items.Remove(item);
            else if (_elementToolbarItemMap.TryGetValue(childControl, out var toolbarItem))
                NativeControl.ToolbarItems.Remove(toolbarItem);

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            ((Shell<T>)newNode)._customDataTemplate = _customDataTemplate;

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
    }
}
