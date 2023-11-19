using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.XamlConverterTool;

public partial class MauiCodeGrid
{
    protected override string GeneratePropertiesCode()
    {
        return string.Join(Environment.NewLine, _xamlElement
            .Attributes()
            .Where(_ => _.Name.LocalName != "RowDefinitions" && _.Name.LocalName != "ColumnDefinitions")
            .Select(_ => GenerateCodeForProperty(_.Name.LocalName, _.Value))
            .Where(_ => _ != null));
    }

    public override string GenerateCode()
    {
        var rows = GetRowDefinitions();
        var columns = GetColumnDefinitions();

        if (Children.Count == 0)
        {
            return $$"""
            new {{_xamlElement.Name.LocalName}}("{{rows}}", "{{columns}}")
            {{GeneratePropertiesCode()}}
            """;
        }

        return $$"""
            new {{_xamlElement.Name.LocalName}}("{{rows}}", "{{columns}}")
            {
                {{string.Join($",{Environment.NewLine}", Children.Select(_ => _.GenerateCode()))}}
            }
            {{GeneratePropertiesCode()}}
            """;
    }

    private string GetRowDefinitions()
    {
        var rows = _xamlElement.Attribute("RowDefinitions")?.Value ?? "*";

        var listOfRows = new List<string>();
        foreach (var rowDefinitionElement in _xamlElement.Elements("RowDefinition"))
        {
            var width = rowDefinitionElement.Attribute("Width")?.Value ?? "*";
            listOfRows.Add(width);
        }

        if (listOfRows.Count > 0) 
        {
            rows = string.Join(",", listOfRows);
        }

        return rows;
    }

    private string GetColumnDefinitions()
    {
        var columns = _xamlElement.Attribute("ColumnDefinitions")?.Value ?? "*";

        var listOfColumns = new List<string>();
        foreach (var columnDefinitionElement in _xamlElement.Elements("ColumnDefinition"))
        {
            var width = columnDefinitionElement.Attribute("Width")?.Value ?? "*";
            listOfColumns.Add(width);
        }

        if (listOfColumns.Count > 0)
        {
            columns = string.Join(",", listOfColumns);
        }

        return columns;
    }
}
