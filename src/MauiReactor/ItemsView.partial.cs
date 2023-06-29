using MauiReactor.Internals;
using System.Collections;

namespace MauiReactor
{
    public partial interface IItemsView
    {
        IEnumerable? ItemsSource { get; set; }

        Func<object, VisualNode>? ItemTemplate { get; set; }

        Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>? ItemTemplateWithNativeView { get; set; }

        VisualStateGroupList? ItemVisualStateGroups { get; set; }  
    }


    public partial class ItemsView<T> : ICustomDataTemplateOwner
    {
        IEnumerable? IItemsView.ItemsSource { get; set; }

        Func<object, VisualNode>? IItemsView.ItemTemplate { get; set; }

        Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>? IItemsView.ItemTemplateWithNativeView { get; set; }

        VisualStateGroupList? IItemsView.ItemVisualStateGroups { get; set; }

        Func<object, VisualNode>? ICustomDataTemplateOwner.ItemTemplate => ((IItemsView)this).ItemTemplate;

        private CustomDataTemplate? _customDataTemplate;

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIItemsView = (IItemsView)this;

            if (thisAsIItemsView.ItemsSource != null &&
                NativeControl.ItemsSource == thisAsIItemsView.ItemsSource)
            {
                Validate.EnsureNotNull(_customDataTemplate);

                _customDataTemplate.Owner = this;

                _customDataTemplate.Update();
            }
            else if (thisAsIItemsView.ItemsSource != null)
            {
                _customDataTemplate = new CustomDataTemplate(this, itemTemplatePresenter => 
                    thisAsIItemsView.ItemVisualStateGroups?.SetToVisualElement(itemTemplatePresenter));
                NativeControl.ItemsSource = thisAsIItemsView.ItemsSource;
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }
            else
            {
                NativeControl.ClearValue(ItemsView.ItemsSourceProperty);
                NativeControl.ClearValue(ItemsView.ItemTemplateProperty);
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            var newItemsView = ((ItemsView<T>)newNode);
            newItemsView._customDataTemplate = _customDataTemplate;
            if (newItemsView._customDataTemplate != null)
            {
                newItemsView._customDataTemplate.Owner = newItemsView;
            }
            

            base.OnMigrated(newNode);
        }
    }

    public static partial class ItemsViewExtensions
    {
        public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable<TItem> itemsSource) where T : IItemsView
        {
            itemsView.ItemsSource = itemsSource;
            return itemsView;
        }

        public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable<TItem> itemsSource, Func<TItem, VisualNode> template) where T : IItemsView
        {
            itemsView.ItemsSource = itemsSource;
            itemsView.ItemTemplate = new Func<object, VisualNode>(item => template((TItem)item));
            return itemsView;
        }

        public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable<TItem> itemsSource, Func<TItem, Microsoft.Maui.Controls.ItemsView, VisualNode> template) where T : IItemsView
        {
            itemsView.ItemsSource = itemsSource;
            itemsView.ItemTemplateWithNativeView = new Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>((item, nativeView) => template((TItem)item, nativeView));
            return itemsView;
        }

        public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable itemsSource, Func<TItem, Microsoft.Maui.Controls.ItemsView, VisualNode> template) where T : IItemsView
        {
            itemsView.ItemsSource = itemsSource;
            itemsView.ItemTemplateWithNativeView = new Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>((item, nativeView) => template((TItem)item, nativeView));
            return itemsView;
        }

        public static T ItemVisualState<T>(this T itemsView, string groupName, string stateName, BindableProperty property, object? value, string? targetName = null) where T : IItemsView
        {
            itemsView.ItemVisualStateGroups ??= new();

            itemsView.ItemVisualStateGroups.TryGetValue(groupName, out var group);

            if (group == null)
            {
                itemsView.ItemVisualStateGroups.Add(groupName, group = new VisualStateGroup());
            }

            group.TryGetValue(stateName, out var state);
            if (state == null)
            {
                group.Add(stateName, state = new VisualState());
            }

            state.Add(new VisualStatePropertySetter(property, value, targetName));

            return itemsView;
        }

        public static T ItemVisualState<T>(this T itemsView, VisualStateGroupList visualState) where T : IItemsView
        {
            itemsView.ItemVisualStateGroups = visualState;

            return itemsView;
        }

    }
}
