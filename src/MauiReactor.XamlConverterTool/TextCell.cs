// <auto-generated />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MauiReactor.XamlConverterTool;
public partial class MauiCodeTextCell : MauiCodeCell
{
    public MauiCodeTextCell(XElement xamlElement) : base(xamlElement)
    {
        RegisterProperty("Text", "string", false);
        RegisterProperty("Detail", "string", false);
        RegisterProperty("TextColor", "Microsoft.Maui.Graphics.Color", false);
        RegisterProperty("DetailColor", "Microsoft.Maui.Graphics.Color", false);
    }
}