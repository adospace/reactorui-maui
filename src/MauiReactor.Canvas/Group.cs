using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.Group))]
public partial class Group { }


//public partial interface IGroup : ICanvasNode
//{
//}

//public partial class Group<T> : CanvasNode<T>, IGroup where T : Internals.Group, new()
//{
//    public Group()
//    {

//    }

//    public Group(Action<T?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }


//    protected override void OnUpdate()
//    {
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIGroup = (IGroup)this;

//        base.OnUpdate();
//    }
//}

//public partial class Group : Group<Internals.Group>
//{
//    public Group()
//    {

//    }

//    public Group(Action<Internals.Group?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }
//}

//public static partial class GroupExtensions
//{
//}
