using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals
{
    public interface INodeContainer
    {
        IReadOnlyList<CanvasNode> Children { get; }

        void InsertChild(int index, CanvasNode child);

        void RemoveChild(CanvasNode child);
    }
}
