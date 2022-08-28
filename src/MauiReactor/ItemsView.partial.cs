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


    public partial class ItemsView<T>
    {
        IEnumerable? IItemsView.ItemsSource { get; set; }

        Func<object, VisualNode>? IItemsView.ItemTemplate { get; set; }

        Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>? IItemsView.ItemTemplateWithNativeView { get; set; }

        public VisualStateGroupList ItemVisualStateGroups { get; set; } = new VisualStateGroupList();

        private class ItemTemplateNode : VisualNode, IVisualNode//, IHostElement
        {
            private readonly ItemTemplatePresenter _presenter;
            //private readonly VisualNode _owner;

            public ItemTemplateNode(VisualNode root, ItemTemplatePresenter presenter)
            {
                _root = root;
                _presenter = presenter;
                //_owner = owner;
                //Parent = owner;

                //Invalidate();
            }

            private VisualNode _root;

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

            internal override VisualNode? Parent
            {
                get => _presenter.Template.Owner;
                set => throw new InvalidOperationException();
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

            public ItemTemplatePresenter(CustomDataTemplate template)
            {
                Template = template;
                VisualStateManager.SetVisualStateGroups(this, template.Owner.ItemVisualStateGroups);
            }

            public CustomDataTemplate Template { get; }

            protected override void OnBindingContextChanged()
            {
                while (true)
                {
                    var item = BindingContext;

                    if (item == null)
                        break;

                    var itemsView = (Microsoft.Maui.Controls.ItemsView)Validate.EnsureNotNull(Template.Owner.NativeControl);

                    VisualNode? newRoot = Template.GetVisualNodeForItem(item, itemsView);
                    
                    if (newRoot == null)
                        break;

                    //if (_itemTemplateNode != null)
                    //{
                    //    _itemTemplateNode.Root = newRoot;
                    //}
                    //else
                    {
                        _itemTemplateNode = new ItemTemplateNode(newRoot, this);
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
            public ItemsView<T> Owner { get; set; }

            //private readonly Dictionary<object, VisualNode> _recycledVisualNode = new();

            public CustomDataTemplate(ItemsView<T> owner)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
            }

            public VisualNode? GetVisualNodeForItem(object item, Microsoft.Maui.Controls.ItemsView nativeItemsView)
            {
                IItemsView itemsView = Owner;

                if (itemsView.ItemTemplate != null)
                {
                    return itemsView.ItemTemplate(item);
                }

                return null;
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
