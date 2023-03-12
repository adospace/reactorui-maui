using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;

public interface IAutomationItemContainer
{
    IEnumerable<IAutomationItem> Descendants();
}
