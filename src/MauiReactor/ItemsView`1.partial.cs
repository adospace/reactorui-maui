using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;
using System.Collections;

namespace MauiReactor
{
    public partial interface IGenericItemsView
    {
        IEnumerable? ItemsSource { get; set; }

        Func<object, VisualNode>? ViewCellItemTemplate { get; set; }

        Func<object, EntryCell>? EntryCellItemTemplate { get; set; }

        Func<object, TextCell>? TextCellItemTemplate { get; set; }

        Func<object, SwitchCell>? SwitchCellItemTemplate { get; set; }

        Func<object, ImageCell>? ImageCellItemTemplate { get; set; }
    }

    public abstract partial class ItemsView<T, TChild>
    {
        IEnumerable? IGenericItemsView.ItemsSource { get; set; }

        Func<object, VisualNode>? IGenericItemsView.ViewCellItemTemplate { get; set; }
        Func<object, EntryCell>? IGenericItemsView.EntryCellItemTemplate { get; set; }
        Func<object, TextCell>? IGenericItemsView.TextCellItemTemplate { get; set; }
        Func<object, SwitchCell>? IGenericItemsView.SwitchCellItemTemplate { get; set; }
        Func<object, ImageCell>? IGenericItemsView.ImageCellItemTemplate { get; set; }

        private class ItemTemplateNode : VisualNode, IVisualNode//, IHostElement
        {
            private readonly Cell? _presenter = null;
            private readonly VisualNode _owner;

            public ItemTemplateNode(VisualNode root, Cell presenter, VisualNode owner)
            {
                _root = root;
                _presenter = presenter;
                _owner = owner;

                Invalidate();
            }

            private VisualNode _root;

            //private IHostElement GetPageHost()
            //{
            //    var current = _owner;
            //    while (current != null && current is not IHostElement)
            //        current = current.Parent;

            //    return Validate.EnsureNotNull(current as IHostElement);
            //}
            Microsoft.Maui.Controls.Page? IVisualNode.GetContainerPage()
            {
                return ((IVisualNode)_owner).GetContainerPage();
            }

            IHostElement? IVisualNode.GetPageHost()
            {
                return ((IVisualNode)_owner).GetPageHost();
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
                    else
                    {
                        _root.Update();
                    }
                }
            }

            //public Microsoft.Maui.Controls.Page? ContainerPage => GetPageHost()?.ContainerPage;

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

            //public IHostElement Run()
            //{
            //    var ownerPageHost = GetPageHost();
            //    if (ownerPageHost == null)
            //    {
            //        throw new NotSupportedException();
            //    }

            //    return ownerPageHost.Run();
            //}

            //public void Stop()
            //{
            //    var ownerPageHost = GetPageHost();
            //    if (ownerPageHost == null)
            //    {
            //        throw new NotSupportedException();
            //    }

            //    ownerPageHost.Stop();
            //}

            //public void RequestAnimationFrame(VisualNode visualNode)
            //{
            //    throw new NotImplementedException();
            //}
        }

        private class ViewCellItemTemplatePresenter : Microsoft.Maui.Controls.ViewCell
        {
            private ItemTemplateNode? _itemTemplateNode;
            private readonly CustomDataTemplate _template;

            public ViewCellItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                while (true)
                {
                    var item = BindingContext;

                    if (item == null)
                        break;

                    VisualNode? newRoot = _template.GetVisualNodeForItem(item);

                    if (newRoot == null)
                        break;

                    ((IVisualNodeWithNativeControl)newRoot).Attach(this);

                    if (_itemTemplateNode != null)
                    {
                        _itemTemplateNode.Root = newRoot;
                    }
                    else
                    {
                        _itemTemplateNode = new ItemTemplateNode(newRoot, this, _template.Owner);
                        _itemTemplateNode.Layout();
                    }

                    break;
                }

                base.OnBindingContextChanged();
            }
        }

        private class EntryCellItemTemplatePresenter : Microsoft.Maui.Controls.EntryCell
        {
            private ItemTemplateNode? _itemTemplateNode;
            private readonly CustomDataTemplate _template;

            public EntryCellItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                while (true)
                {
                    var item = BindingContext;

                    if (item == null)
                        break;

                    VisualNode? newRoot = _template.GetVisualNodeForItem(item);

                    if (newRoot == null)
                        break;

                    ((IVisualNodeWithNativeControl)newRoot).Attach(this);

                    if (_itemTemplateNode != null)
                    {
                        _itemTemplateNode.Root = newRoot;
                    }
                    else
                    {
                        _itemTemplateNode = new ItemTemplateNode(newRoot, this, _template.Owner);
                        _itemTemplateNode.Layout();
                    }

                    break;
                }

                base.OnBindingContextChanged();
            }
        }

        private class TextCellItemTemplatePresenter : Microsoft.Maui.Controls.TextCell
        {
            private ItemTemplateNode? _itemTemplateNode;
            private readonly CustomDataTemplate _template;

            public TextCellItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                while (true)
                {
                    var item = BindingContext;

                    if (item == null)
                        break;

                    VisualNode? newRoot = _template.GetVisualNodeForItem(item);

                    if (newRoot == null)
                        break;

                    ((IVisualNodeWithNativeControl)newRoot).Attach(this);

                    if (_itemTemplateNode != null)
                    {
                        _itemTemplateNode.Root = newRoot;
                    }
                    else
                    {
                        _itemTemplateNode = new ItemTemplateNode(newRoot, this, _template.Owner);
                        _itemTemplateNode.Layout();
                    }

                    break;
                }

                base.OnBindingContextChanged();
            }
        }
        
        private class SwitchCellItemTemplatePresenter : Microsoft.Maui.Controls.SwitchCell
        {
            private ItemTemplateNode? _itemTemplateNode;
            private readonly CustomDataTemplate _template;

            public SwitchCellItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                while (true)
                {
                    var item = BindingContext;

                    if (item == null)
                        break;

                    VisualNode? newRoot = _template.GetVisualNodeForItem(item);

                    if (newRoot == null)
                        break;

                    ((IVisualNodeWithNativeControl)newRoot).Attach(this);

                    if (_itemTemplateNode != null)
                    {
                        _itemTemplateNode.Root = newRoot;
                    }
                    else
                    {
                        _itemTemplateNode = new ItemTemplateNode(newRoot, this, _template.Owner);
                        _itemTemplateNode.Layout();
                    }

                    break;
                }

                base.OnBindingContextChanged();
            }
        }

        private class ImageCellItemTemplatePresenter : Microsoft.Maui.Controls.ImageCell
        {
            private ItemTemplateNode? _itemTemplateNode;
            private readonly CustomDataTemplate _template;

            public ImageCellItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                while (true)
                {
                    var item = BindingContext;

                    if (item == null)
                        break;

                    VisualNode? newRoot = _template.GetVisualNodeForItem(item);

                    if (newRoot == null)
                        break;

                    ((IVisualNodeWithNativeControl)newRoot).Attach(this);

                    if (_itemTemplateNode != null)
                    {
                        _itemTemplateNode.Root = newRoot;
                    }
                    else
                    {
                        _itemTemplateNode = new ItemTemplateNode(newRoot, this, _template.Owner);
                        _itemTemplateNode.Layout();
                    }

                    break;
                }

                base.OnBindingContextChanged();
            }
        }

        private class CustomDataTemplate
        {
            public DataTemplate DataTemplate { get; }
            public ItemsView<T, TChild> Owner { get; set; }

            public Func<object, VisualNode>? ItemTemplateFunc { get; }

            public CustomDataTemplate(ItemsView<T, TChild> owner)
            {
                Owner = owner;
                ItemTemplateFunc = owner.GetItemTemplateFunc();
                DataTemplate = new DataTemplate(() =>
                {
                    IGenericItemsView itemsView = Owner;
                    if (itemsView.ViewCellItemTemplate != null)
                    {
                        return new ViewCellItemTemplatePresenter(this);
                    }
                    else if (itemsView.EntryCellItemTemplate != null)
                    {
                        return new EntryCellItemTemplatePresenter(this);
                    }
                    else if (itemsView.TextCellItemTemplate != null)
                    {
                        return new TextCellItemTemplatePresenter(this);
                    }
                    else if (itemsView.SwitchCellItemTemplate != null)
                    {
                        return new SwitchCellItemTemplatePresenter(this);
                    }
                    else if (itemsView.ImageCellItemTemplate != null)
                    {
                        return new ImageCellItemTemplatePresenter(this);
                    }

                    throw new InvalidOperationException();
                });
            }

            public VisualNode? GetVisualNodeForItem(object item)
            {
                return Owner.GetItemTemplateFunc()?.Invoke(item);
            }
        }

        private CustomDataTemplate? _customDataTemplate;

        private Func<object, VisualNode>? GetItemTemplateFunc()
        {
            IGenericItemsView itemsView = this;

            if (itemsView.ViewCellItemTemplate != null)
            {
                return itemsView.ViewCellItemTemplate;
            }
            else if (itemsView.EntryCellItemTemplate != null)
            {
                return itemsView.EntryCellItemTemplate;
            }
            else if (itemsView.TextCellItemTemplate != null)
            {
                return itemsView.TextCellItemTemplate;
            }
            else if (itemsView.SwitchCellItemTemplate != null)
            {
                return itemsView.SwitchCellItemTemplate;
            }
            else if (itemsView.ImageCellItemTemplate != null)
            {
                return itemsView.ImageCellItemTemplate;
            }

            return null;
        }

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIItemsView = (IGenericItemsView)this;

            var itemTemplateFunc = GetItemTemplateFunc();

            if (NativeControl.ItemsSource is ObservableItemsSource existingCollection &&
                existingCollection.ItemsSource == thisAsIItemsView.ItemsSource &&
                itemTemplateFunc != null &&
                _customDataTemplate?.ItemTemplateFunc?.GetType() == itemTemplateFunc.GetType())
            {
                Validate.EnsureNotNull(_customDataTemplate);

                _customDataTemplate.Owner = this;
                existingCollection.NotifyCollectionChanged();
            }
            else if (thisAsIItemsView.ItemsSource != null && itemTemplateFunc != null)
            {
                _customDataTemplate = new CustomDataTemplate(this);
                NativeControl.ItemsSource = ObservableItemsSource.Create(thisAsIItemsView.ItemsSource, itemTemplateFunc);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }
            else
            {
                NativeControl.ItemsSource = null;
                NativeControl.ItemTemplate = null;
                _customDataTemplate = null;
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            ((ItemsView<T, TChild>)newNode)._customDataTemplate = _customDataTemplate;

            base.OnMigrated(newNode);
        }
    }

    public static partial class GenericItemsViewExtensions
    {
        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable<TItem> itemsSource) where T : IGenericItemsView
        {
            itemsview.ItemsSource = itemsSource;
            return itemsview;
        }

        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable<TItem> itemsSource, Func<TItem, ViewCell> template) where T : IGenericItemsView
        {
            itemsview.ItemsSource = itemsSource;
            
            itemsview.ViewCellItemTemplate = new Func<object, ViewCell>(item => template((TItem)item));
            itemsview.SwitchCellItemTemplate = null;
            itemsview.TextCellItemTemplate = null;
            itemsview.EntryCellItemTemplate = null;
            itemsview.ImageCellItemTemplate = null;

            return itemsview;
        }

        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable<TItem> itemsSource, Func<TItem, EntryCell> template) where T : IGenericItemsView
        {
            itemsview.ItemsSource = itemsSource;

            itemsview.ViewCellItemTemplate = null;
            itemsview.SwitchCellItemTemplate = null;
            itemsview.TextCellItemTemplate = null;
            itemsview.EntryCellItemTemplate = new Func<object, EntryCell>(item => template((TItem)item));
            itemsview.ImageCellItemTemplate = null;

            return itemsview;
        }

        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable<TItem> itemsSource, Func<TItem, TextCell> template) where T : IGenericItemsView
        {
            itemsview.ItemsSource = itemsSource;

            itemsview.ViewCellItemTemplate = null;
            itemsview.SwitchCellItemTemplate = null;
            itemsview.TextCellItemTemplate = new Func<object, TextCell>(item => template((TItem)item));
            itemsview.EntryCellItemTemplate = null;
            itemsview.ImageCellItemTemplate = null;

            return itemsview;
        }

        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable<TItem> itemsSource, Func<TItem, SwitchCell> template) where T : IGenericItemsView
        {
            itemsview.ItemsSource = itemsSource;

            itemsview.ViewCellItemTemplate = null;
            itemsview.SwitchCellItemTemplate = new Func<object, SwitchCell>(item => template((TItem)item));
            itemsview.TextCellItemTemplate = null;
            itemsview.EntryCellItemTemplate = null;
            itemsview.ImageCellItemTemplate = null;

            return itemsview;
        }

        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable<TItem> itemsSource, Func<TItem, ImageCell> template) where T : IGenericItemsView
        {
            itemsview.ItemsSource = itemsSource;

            itemsview.ViewCellItemTemplate = null;
            itemsview.SwitchCellItemTemplate = null;
            itemsview.TextCellItemTemplate = null;
            itemsview.EntryCellItemTemplate = null;
            itemsview.ImageCellItemTemplate = new Func<object, ImageCell>(item => template((TItem)item));

            return itemsview;
        }
    }
}
