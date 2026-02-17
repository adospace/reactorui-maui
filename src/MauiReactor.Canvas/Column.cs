using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.Column))]
public partial class Column
{
    public Column(string rows)
    {
        this.Rows(rows);
    }
}

//public partial interface IColumn : ICanvasVisualElement
//{
//    PropertyValue<string>? Rows { get; set; }
//}

//public partial class Column<T> : CanvasVisualElement<T>, IColumn where T : Internals.Column, new()
//{
//    public Column()
//    {

//    }

//    public Column(Action<T?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }

//    PropertyValue<string>? IColumn.Rows { get; set; }

//    protected override void OnUpdate()
//    {
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIColumn = (IColumn)this;
//        SetPropertyValue(NativeControl, Internals.Column.RowsProperty, thisAsIColumn.Rows);


//        base.OnUpdate();
//    }
//}

//public partial class Column : Column<Internals.Column>
//{
//    public Column()
//    {

//    }

//    public Column(Action<Internals.Column?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }

//    public Column(string rows)
//    {
//        this.Rows(rows);
//    }
//}

//public static partial class ColumnExtensions
//{
//    public static T Rows<T>(this T node, string value) where T : IColumn
//    {
//        node.Rows = new PropertyValue<string>(value);
//        return node;
//    }

//    public static T Rows<T>(this T node, Func<string> valueFunc) where T : IColumn
//    {
//        node.Rows = new PropertyValue<string>(valueFunc);
//        return node;
//    }
//}
