using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial interface IItemsView
    {
        IEnumerable? ItemsSource { get; set; }
        Func<object, VisualNode>? ItemTemplate { get; set; }
    }

    public partial class ItemsView<T>
    {
        IEnumerable? IItemsView.ItemsSource { get; set; }

        Func<object, VisualNode>? IItemsView.ItemTemplate { get; set; }

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
                    var item = BindingContext;
                    if (item != null)
                    {
                        var layout = (IItemsView)_template.Owner;
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
            public ItemsView<T> Owner { get; set; }

            public CustomDataTemplate(ItemsView<T> owner)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
            }
        }

        private CustomDataTemplate? _customDataTemplate;

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIItemsView = (IItemsView)this;

            if (NativeControl.ItemsSource is ObservableItemsSource existingCollection &&
                existingCollection.ItemsSource == thisAsIItemsView.ItemsSource)
            {
                Validate.EnsureNotNull(_customDataTemplate);

                _customDataTemplate.Owner = this;
                existingCollection.NotifyCollectionChanged();
            }
            else if (thisAsIItemsView.ItemsSource != null)
            {
                _customDataTemplate = new CustomDataTemplate(this);
                NativeControl.ItemsSource = ObservableItemsSource.Create(thisAsIItemsView.ItemsSource);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }
            else
            {
                NativeControl.ItemsSource = null;
                NativeControl.ItemTemplate = null;
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            ((ItemsView<T>)newNode)._customDataTemplate = _customDataTemplate;

            base.OnMigrated(newNode);
        }


    }

    public static partial class ItemsViewExtensions
    {
        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable<TItem> itemsSource) where T : IItemsView
        {
            itemsview.ItemsSource = itemsSource;
            return itemsview;
        }

        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable<TItem> itemsSource, Func<TItem, VisualNode> template) where T : IItemsView
        {
            itemsview.ItemsSource = itemsSource;
            itemsview.ItemTemplate = new Func<object, VisualNode>(item => template((TItem)item));
            return itemsview;
        }
    }
}
