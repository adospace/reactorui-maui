using MauiReactor.Internals;
using System.Collections;

namespace MauiReactor
{
    public partial interface IItemsView
    {
        IEnumerable? ItemsSource { get; set; }

        Func<object, VisualNode>? ItemTemplate { get; set; }

        Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>? ItemTemplateWithNativeView { get; set; }

        VisualStateGroupList ItemVisualStateGroups { get; set; }  
    }


    public partial class ItemsView<T> : ICustomDataTemplateOwner
    {
        IEnumerable? IItemsView.ItemsSource { get; set; }

        Func<object, VisualNode>? IItemsView.ItemTemplate { get; set; }

        Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>? IItemsView.ItemTemplateWithNativeView { get; set; }

        public VisualStateGroupList ItemVisualStateGroups { get; set; } = new VisualStateGroupList();

        Func<object, VisualNode>? ICustomDataTemplateOwner.ItemTemplate => ((IItemsView)this).ItemTemplate;

        private CustomDataTemplate? _customDataTemplate;

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIItemsView = (IItemsView)this;

            if (//NativeControl.ItemsSource is ObservableItemsSource existingCollection &&
                NativeControl.ItemsSource == thisAsIItemsView.ItemsSource)
            {
                Validate.EnsureNotNull(_customDataTemplate);

                _customDataTemplate.Owner = this;

                _customDataTemplate.Update();

                //existingCollection.NotifyCollectionChanged();
            }
            else if (thisAsIItemsView.ItemsSource != null)
            {
                _customDataTemplate = new CustomDataTemplate(this, itemTemplatePresenter => VisualStateManager.SetVisualStateGroups(itemTemplatePresenter, this.ItemVisualStateGroups));
                NativeControl.ItemsSource = thisAsIItemsView.ItemsSource;// ObservableItemsSource.Create(thisAsIItemsView.ItemsSource);
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

        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable<TItem> itemsSource, Func<TItem, Microsoft.Maui.Controls.ItemsView, VisualNode> template) where T : IItemsView
        {
            itemsview.ItemsSource = itemsSource;
            itemsview.ItemTemplateWithNativeView = new Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>((item, nativeView) => template((TItem)item, nativeView));
            return itemsview;
        }

        public static T ItemsSource<T, TItem>(this T itemsview, IEnumerable itemsSource, Func<TItem, Microsoft.Maui.Controls.ItemsView, VisualNode> template) where T : IItemsView
        {
            itemsview.ItemsSource = itemsSource;
            itemsview.ItemTemplateWithNativeView = new Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>((item, nativeView) => template((TItem)item, nativeView));
            return itemsview;
        }

        public static T ItemVisualState<T>(this T itemsview, string groupName, string stateName, BindableProperty property, object value) where T : IItemsView
        {
            var group = itemsview.ItemVisualStateGroups.FirstOrDefault(_ => _.Name == groupName);

            if (group == null)
            {
                itemsview.ItemVisualStateGroups.Add(group = new VisualStateGroup()
                {
                    Name = groupName
                });
            }

            var state = group.States.FirstOrDefault(_ => _.Name == stateName);
            if (state == null)
            {
                group.States.Add(state = new VisualState { Name = stateName });
            }

            state.Setters.Add(new Setter() { Property = property, Value = value });

            return itemsview;
        }

    }
}
