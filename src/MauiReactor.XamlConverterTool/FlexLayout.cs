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
public partial class MauiCodeFlexLayout : MauiCodeLayout
{
    public MauiCodeFlexLayout(XElement xamlElement) : base(xamlElement)
    {
        RegisterProperty("Direction", "Microsoft.Maui.Layouts.FlexDirection", true);
        RegisterProperty("JustifyContent", "Microsoft.Maui.Layouts.FlexJustify", true);
        RegisterProperty("AlignContent", "Microsoft.Maui.Layouts.FlexAlignContent", true);
        RegisterProperty("AlignItems", "Microsoft.Maui.Layouts.FlexAlignItems", true);
        RegisterProperty("Position", "Microsoft.Maui.Layouts.FlexPosition", true);
        RegisterProperty("Wrap", "Microsoft.Maui.Layouts.FlexWrap", true);
    }
}