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
    public partial interface ICollectionView : IGroupableItemsView
    {


    }
    public partial class CollectionView<T> : GroupableItemsView<T>, ICollectionView where T : Microsoft.Maui.Controls.CollectionView, new()
    {
        public CollectionView()
        {

        }

        public CollectionView(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }



        protected override void OnUpdate()
        {
            OnBeginUpdate();

            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class CollectionView : CollectionView<Microsoft.Maui.Controls.CollectionView>
    {
        public CollectionView()
        {

        }

        public CollectionView(Action<Microsoft.Maui.Controls.CollectionView?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class CollectionViewExtensions
    {

    }
}
