using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.SkiaCanvas.Internals;

public interface INodeParent
{
    void RequestInvalidate();
}
