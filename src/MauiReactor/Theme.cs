using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public abstract class Theme
{
    internal void Apply()
    {
        OnApply();
    }

    protected abstract void OnApply();
}
