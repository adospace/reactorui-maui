using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MauiReactor.XamlConverterTool;

public class XamlParser
{
    public XamlParser()
    {
    }

    public void Parse(string xaml)
    {
        var xamlDocument = XDocument.Parse(xaml);


    }
}
