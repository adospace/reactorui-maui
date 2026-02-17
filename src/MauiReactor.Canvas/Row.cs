using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.Row))]
public partial class Row 
{
    public Row(string columns)
    {
        this.Columns(columns);
    }
}



//public partial interface IRow : ICanvasVisualElement
//{
//    PropertyValue<string>? Columns { get; set; }
//}

//public partial class Row<T> : CanvasVisualElement<T>, IRow where T : Internals.Row, new()
//{
//    public Row()
//    {

//    }

//    public Row(Action<T?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }

//    PropertyValue<string>? IRow.Columns { get; set; }

//    protected override void OnUpdate()
//    {
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIRow = (IRow)this;
//        SetPropertyValue(NativeControl, Internals.Row.ColumnsProperty, thisAsIRow.Columns);


//        base.OnUpdate();
//    }
//}

//public partial class Row : Row<Internals.Row>
//{
//    public Row()
//    {

//    }

//    public Row(Action<Internals.Row?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }

//    public Row(string columns)
//    {
//        this.Columns(columns);
//    }
//}

//public static partial class RowExtensions
//{
//    public static T Columns<T>(this T node, string value) where T : IRow
//    {
//        node.Columns = new PropertyValue<string>(value);
//        return node;
//    }

//    public static T Columns<T>(this T node, Func<string> valueFunc) where T : IRow
//    {
//        node.Columns = new PropertyValue<string>(valueFunc);
//        return node;
//    }
//}
