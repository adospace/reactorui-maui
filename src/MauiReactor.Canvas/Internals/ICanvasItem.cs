using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals;

public interface ICanvasNodeParent
{
    void RequestInvalidate();
}
