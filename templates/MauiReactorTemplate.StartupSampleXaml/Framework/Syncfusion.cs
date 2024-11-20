using MauiReactor.Internals;

namespace MauiReactorTemplate.StartupSampleXaml.Framework;

[Scaffold(typeof(Syncfusion.Maui.Toolkit.SfView))]
public abstract partial class SfView
{
}

[Scaffold(typeof(Syncfusion.Maui.Toolkit.Internals.PullToRefreshBase))]
partial class PullToRefreshBase
{

}

[Scaffold(typeof(Syncfusion.Maui.Toolkit.PullToRefresh.SfPullToRefresh))]
partial class SfPullToRefresh
{
    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is MauiControls.View view)
        {
            NativeControl.PullableContent = view;
        }

        base.OnAddChild(widget, childControl);
    }
}

[Scaffold(typeof(Syncfusion.Maui.Toolkit.SfContentView))]
partial class SfContentView
{
}

partial class SfContentView<T>
{
    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);
        if (childControl is MauiControls.View view)
        {
            NativeControl.Content = view;
        }
        base.OnAddChild(widget, childControl);
    }
}

[Scaffold(typeof(Syncfusion.Maui.Toolkit.TextInputLayout.SfTextInputLayout))]
partial class SfTextInputLayout
{ }

[Scaffold(typeof(Syncfusion.Maui.Toolkit.Shimmer.SfShimmer))]
partial class SfShimmer
{
    
}

partial interface ISfShimmer
{
    public VisualNode? CustomView { get; set; }
}

partial class SfShimmer<T>
{
    VisualNode? ISfShimmer.CustomView { get; set; }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        var thisAsISfShimmer = (ISfShimmer)this;

        var children = base.RenderChildren();

        if (thisAsISfShimmer.CustomView != null)
        {
            children = children.Concat([thisAsISfShimmer.CustomView]);
        }

        return children;
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsISfShimmer = (ISfShimmer)this;

        if (widget == thisAsISfShimmer.CustomView)
        {
            NativeControl.CustomView = (View)childControl;
        }

        base.OnAddChild(widget, childControl);
    }
}

partial class SfShimmerExtensions
{
    public static T CustomView<T>(this T shimmer, VisualNode? customView) where T : ISfShimmer
    {
        shimmer.CustomView = customView;
        return shimmer;
    }
}

[Scaffold(typeof(Syncfusion.Maui.Toolkit.Shimmer.ShimmerView))]
partial class ShimmerView
{

}

[Scaffold(typeof(Syncfusion.Maui.Toolkit.EffectsView.SfEffectsView))]
partial class SfEffectsView
{

}

[Scaffold(typeof(Syncfusion.Maui.Toolkit.SegmentedControl.SfSegmentedControl))]
partial class SfSegmentedControl
{
    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Syncfusion.Maui.Toolkit.SegmentedControl.SfSegmentItem segmentItem)
        {
            if (NativeControl.ItemsSource is IList<Syncfusion.Maui.Toolkit.SegmentedControl.SfSegmentItem> existingList)
            {
                existingList.Add(segmentItem);
            }
            else
            {
                NativeControl.ItemsSource = new List<Syncfusion.Maui.Toolkit.SegmentedControl.SfSegmentItem> { segmentItem };
            }
        }

        base.OnAddChild(widget, childControl);
    }
}

[Scaffold(typeof(Syncfusion.Maui.Toolkit.SegmentedControl.SfSegmentItem))]
partial class SfSegmentItem
{

}
