using MauiReactor.Internals;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface ICanvasNode : IVisualNode
    {
    }

    public partial class CanvasNode<T> : VisualNode<T>, ICanvasNode where T : Internals.CanvasNode, new()
    {
        public CanvasNode()
        {

        }

        public CanvasNode(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public partial class CanvasNode : CanvasNode<Internals.CanvasNode>
    {
        public CanvasNode()
        {

        }

        public CanvasNode(Action<Internals.CanvasNode?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class CanvasNodeExtensions
    {

    }
}
