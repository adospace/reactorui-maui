using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    //public partial interface ILayout
    //{
    //    IEnumerable?ItemsSource { get; set; }
    //    Func<object, VisualNode>? ItemTemplate { get; set; }
    //}

    public abstract partial class Layout<T>
    {
        //IEnumerable? ILayout.ItemsSource { get; set; }
        //Func<object, VisualNode>? ILayout.ItemTemplate { get; set; }

        //private class ItemTemplateNode : VisualNode, IHostElement
        //{
        //    private readonly ItemTemplatePresenter? _presenter = null;
        //    private readonly VisualNode _owner;

        //    public ItemTemplateNode(VisualNode root, ItemTemplatePresenter presenter, VisualNode owner)
        //    {
        //        _root = root;
        //        _presenter = presenter;
        //        _owner = owner;
                
        //        Invalidate();
        //    }

        //    private VisualNode _root;

        //    private IHostElement GetPageHost()
        //    {
        //        var current = _owner;
        //        while (current != null && current is not IHostElement)
        //            current = current.Parent;

        //        return Validate.EnsureNotNull(current as IHostElement);
        //    }

        //    public VisualNode Root
        //    {
        //        get => _root;
        //        set
        //        {
        //            if (_root != value)
        //            {
        //                _root = value;
        //                Invalidate();
        //            }
        //        }
        //    }

        //    public Microsoft.Maui.Controls.Page? ContainerPage => GetPageHost().ContainerPage;

        //    protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        //    {
        //        Validate.EnsureNotNull(_presenter);

        //        if (nativeControl is View view)
        //            _presenter.Content = view;
        //        else
        //        {
        //            throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
        //        }
        //    }

        //    protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        //    {
        //    }

        //    protected override IEnumerable<VisualNode> RenderChildren()
        //    {
        //        yield return Root;
        //    }

        //    protected internal override void OnLayoutCycleRequested()
        //    {
        //        Layout();
        //        base.OnLayoutCycleRequested();
        //    }

        //    public IHostElement Run()
        //    {
        //        var ownerPageHost = GetPageHost();
        //        if (ownerPageHost == null)
        //        {
        //            throw new NotSupportedException();
        //        }

        //        return ownerPageHost.Run();
        //    }

        //    public void Stop()
        //    {
        //        var ownerPageHost = GetPageHost();
        //        if (ownerPageHost == null)
        //        {
        //            throw new NotSupportedException();
        //        }

        //        ownerPageHost.Stop();
        //    }
        //}

        //private class ItemTemplatePresenter : Microsoft.Maui.Controls.ContentView
        //{
        //    private ItemTemplateNode _itemTemplateNode;
        //    private readonly CustomDataTemplate _template;

        //    public ItemTemplatePresenter(CustomDataTemplate template)
        //    {
        //        _template = template;
        //    }

        //    protected override void OnBindingContextChanged()
        //    {
        //        if (BindingContext != null)
        //        {
        //            var item = BindingContext;
        //            if (item != null)
        //            {
        //                var layout = (ILayout)_template.Owner;
        //                if (layout.ItemTemplate != null)
        //                {
        //                    var newRoot = layout.ItemTemplate(item);
        //                    if (newRoot != null)
        //                    {
        //                        _itemTemplateNode = new ItemTemplateNode(newRoot, this, _template.Owner);
        //                        _itemTemplateNode.Layout();
        //                    }
        //                }
        //            }
        //        }

        //        base.OnBindingContextChanged();
        //    }
        //}

        //private class CustomDataTemplate
        //{
        //    public DataTemplate DataTemplate { get; }
        //    public Layout<T> Owner { get; set; }

        //    public CustomDataTemplate(Layout<T> owner)
        //    {
        //        Owner = owner;
        //        DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
        //    }
        //}

        //private CustomDataTemplate? _customDataTemplate;

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.IView control)
            {
                NativeControl.Children.Insert(widget.ChildIndex, control);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Children.Remove((Microsoft.Maui.IView)childControl);

            base.OnRemoveChild(widget, childControl);
        }

        //partial void OnBeginUpdate()
        //{
        //    Validate.EnsureNotNull(NativeControl);
        //    var thisAsILayout = (ILayout)this;
        //    var nativeItemsSource = NativeControl.GetValue(BindableLayout.ItemsSourceProperty) as IEnumerable;

        //    if (nativeItemsSource is ObservableItemsSource<I> existingCollection &&
        //        existingCollection.ItemsSource == Collection)
        //    {
        //        _customDataTemplate.Owner = this;
        //        existingCollection.NotifyCollectionChanged();
        //    }
        //    else if (Collection != null)
        //    {
        //        _customDataTemplate = new CustomDataTemplate(this);
        //        NativeControl.ItemsSource = ObservableItemsSource<I>.Create(Collection);
        //        NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
        //    }
        //    else
        //    {
        //        NativeControl.ItemsSource = null;
        //        NativeControl.ItemTemplate = null;
        //    }

        //}
    }

    //public static partial class LayoutExtensions
    //{
    //    public static T ItemsSource<T>(this T itemsview, IEnumerable collection) where T : ILayout
    //    {
    //        itemsview.ItemsSource = collection;
    //        return itemsview;
    //    }

    //    public static T ItemTemplate<T, TItem>(this T itemsview, Func<TItem, VisualNode> itemTemplate) where T : ILayout
    //    {
    //        itemsview.ItemTemplate = new Func<object, VisualNode>(item => itemTemplate((TItem)item));
    //        return itemsview;
    //    }

    //}
}
