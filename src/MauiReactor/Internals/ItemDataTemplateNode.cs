using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;


internal class ItemDataTemplateNode<T> : Element<T>, IVisualNode where T : Microsoft.Maui.Controls.Element, Microsoft.Maui.IContentView, new()
{
    private VisualNode? _root;

    // The item this cell's subtree was built for, and whether the next layout must tear
    // that subtree down instead of merging into it.
    private object? _boundItem;
    private bool _remountRoot;
    private readonly WeakReference<CustomDataTemplate<T>> _dataTemplateRef;

    public ItemDataTemplateNode(CustomDataTemplate<T> dataTemplate)
    {
        _dataTemplateRef = new WeakReference<CustomDataTemplate<T>>(dataTemplate);
    }

    public VisualNode? Root
    {
        get => _root;
        set
        {
            if (_root != value)
            {
                _root = value;

                try
                {
                    //we want the animations to restart instead of migrating from an old visual node
                    _skipAnimationMigration = true;
                    Invalidate();
                }
                finally
                {
                    _skipAnimationMigration = false;
                }
            }
        }
    }

    internal T? ItemContainer => NativeControl;

    protected override void OnMount()
    {
        base.OnMount();

        Validate.EnsureNotNull(NativeControl);
        NativeControl.BindingContextChanged += NativeControl_BindingContextChanged;
    }

    private void NativeControl_BindingContextChanged(object? sender, EventArgs e) => BindRoot();

    /// <summary>
    /// Rebuilds the cell's subtree for whatever item it is currently bound to. When that
    /// item is a different one - which is what a recycled list cell looks like - the
    /// existing subtree is marked for teardown, because merging would hand the previous
    /// item's component state, and its hook cells, to the new item's tree.
    /// Identity is reference-based on purpose: two equal-but-distinct items are still two
    /// items, and sharing state between them is the bug this prevents.
    /// </summary>
    private void BindRoot()
    {
        Validate.EnsureNotNull(NativeControl);

        var item = NativeControl.BindingContext;

        if (item == null)
        {
            _boundItem = null;
            Root = null;
            return;
        }

        if (!ReferenceEquals(item, _boundItem))
        {
            _boundItem = item;
            _remountRoot = true;
        }

        if (_dataTemplateRef.TryGetTarget(out var dataTemplate))
        {
            Root = dataTemplate.GetVisualNodeForItem(item);
        }
    }

    internal override void MergeChildrenFrom(IReadOnlyList<VisualNode> oldChildren, bool oldHasKeyedChildren = false)
    {
        if (_remountRoot)
        {
            _remountRoot = false;

            foreach (var oldChild in oldChildren)
            {
                oldChild.Unmount();
                oldChild.Parent = null;
            }

            return;
        }

        base.MergeChildrenFrom(oldChildren, oldHasKeyedChildren);
    }

    protected override void OnUnmount()
    {
        Validate.EnsureNotNull(NativeControl);
        NativeControl.BindingContextChanged -= NativeControl_BindingContextChanged;

        base.OnUnmount();
    }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        if (_root != null)
        {
            yield return _root;
        }
    }

    internal override VisualNode? Parent
    {
        get
        {
            if (_dataTemplateRef.TryGetTarget(out var dataTemplate))
            {
                return dataTemplate.Owner as VisualNode;
            }
            return null;
        }
        set => throw new InvalidOperationException();
    }

    protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (nativeControl is View view)
        {
            if (NativeControl is Microsoft.Maui.Controls.ContentView contentView)
            {
                contentView.Content = view;
            }
            else if (NativeControl is Microsoft.Maui.Controls.ContentPage contentPage)
            {
                contentPage.Content = view;
            }
        }            
        else
        {
            throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
        }
    }

    protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
    {
    }

    protected internal override void OnLayoutCycleRequested()
    {
        Layout();
        base.OnLayoutCycleRequested();
    }

    public new void Update()
    {
        if (NativeControl?.BindingContext != null)
        {
            BindRoot();
        }
    }
}

internal class ItemControlTemplateNode : ContentView<Microsoft.Maui.Controls.ContentView>, IVisualNode
{
    private VisualNode? _root;

    // The item this cell's subtree was built for, and whether the next layout must tear
    // that subtree down instead of merging into it.
    private object? _boundItem;
    private bool _remountRoot;
    private readonly WeakReference<CustomControlTemplate> _controlTemplateRef;

    public ItemControlTemplateNode(CustomControlTemplate dataTemplate)
    {
        _controlTemplateRef = new WeakReference<CustomControlTemplate>(dataTemplate);
    }

    public VisualNode? Root
    {
        get => _root;
        set
        {
            if (_root != value)
            {
                _root = value;

                try
                {
                    //we want the animations to restart instead of migrating from an old visual node
                    _skipAnimationMigration = true;
                    Invalidate();
                }
                finally
                {
                    _skipAnimationMigration = false;
                }
            }
        }
    }

    internal Microsoft.Maui.Controls.ContentView? ItemContainer => NativeControl;

    protected override void OnMount()
    {
        base.OnMount();

        Validate.EnsureNotNull(NativeControl);
        NativeControl.BindingContextChanged += NativeControl_BindingContextChanged;
    }

    private void NativeControl_BindingContextChanged(object? sender, EventArgs e) => BindRoot();

    /// <summary>See the note on <see cref="ItemDataTemplateNode{T}"/>: a rebind to a
    /// different item tears the subtree down rather than merging into it.</summary>
    private void BindRoot()
    {
        Validate.EnsureNotNull(NativeControl);

        var item = NativeControl.BindingContext;

        if (item == null)
        {
            _boundItem = null;
            Root = null;
            return;
        }

        if (!ReferenceEquals(item, _boundItem))
        {
            _boundItem = item;
            _remountRoot = true;
        }

        if (_controlTemplateRef.TryGetTarget(out var dataTemplate))
        {
            Root = dataTemplate.GetVisualNodeForItem(item);
        }
    }

    internal override void MergeChildrenFrom(IReadOnlyList<VisualNode> oldChildren, bool oldHasKeyedChildren = false)
    {
        if (_remountRoot)
        {
            _remountRoot = false;

            foreach (var oldChild in oldChildren)
            {
                oldChild.Unmount();
                oldChild.Parent = null;
            }

            return;
        }

        base.MergeChildrenFrom(oldChildren, oldHasKeyedChildren);
    }

    protected override void OnUnmount()
    {
        Validate.EnsureNotNull(NativeControl);
        NativeControl.BindingContextChanged -= NativeControl_BindingContextChanged;

        base.OnUnmount();
    }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        if (_root != null)
        {
            yield return _root;
        }
    }

    internal override VisualNode? Parent
    {
        get
        {
            if (_controlTemplateRef.TryGetTarget(out var dataTemplate))
            {
                return dataTemplate.Owner as VisualNode;
            }
            return null;
        }
        set => throw new InvalidOperationException();
    }

    protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (nativeControl is View view)
            NativeControl.Content = view;
        else
        {
            throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
        }
    }

    protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
    {
    }

    protected internal override void OnLayoutCycleRequested()
    {
        Layout();
        base.OnLayoutCycleRequested();
    }

    public new void Update()
    {
        if (NativeControl?.BindingContext != null)
        {
            BindRoot();
        }
    }
}

