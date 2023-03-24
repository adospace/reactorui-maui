using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.SkiaCanvas.Internals
{
    public interface INodeContainer
    {
        IReadOnlyList<SkNode> Children { get; }

        void InsertChild(int index, SkNode child);

        void RemoveChild(SkNode child);
    }
}
