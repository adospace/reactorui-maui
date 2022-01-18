using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IGroupableItemsView : ISelectableItemsView
    {
        PropertyValue<bool>? IsGrouped { get; set; }


    }
    public partial class GroupableItemsView<T> : SelectableItemsView<T>, IGroupableItemsView where T : Microsoft.Maui.Controls.GroupableItemsView, new()
    {
        public GroupableItemsView()
        {

        }

        public GroupableItemsView(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IGroupableItemsView.IsGrouped { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIGroupableItemsView = (IGroupableItemsView)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.GroupableItemsView.IsGroupedProperty, thisAsIGroupableItemsView.IsGrouped);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class GroupableItemsView : GroupableItemsView<Microsoft.Maui.Controls.GroupableItemsView>
    {
        public GroupableItemsView()
        {

        }

        public GroupableItemsView(Action<Microsoft.Maui.Controls.GroupableItemsView?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class GroupableItemsViewExtensions
    {
        public static T IsGrouped<T>(this T groupableitemsview, bool isGrouped) where T : IGroupableItemsView
        {
            groupableitemsview.IsGrouped = new PropertyValue<bool>(isGrouped);
            return groupableitemsview;
        }
        public static T IsGrouped<T>(this T groupableitemsview, Func<bool> isGroupedFunc) where T : IGroupableItemsView
        {
            groupableitemsview.IsGrouped = new PropertyValue<bool>(isGroupedFunc);
            return groupableitemsview;
        }




    }
}
