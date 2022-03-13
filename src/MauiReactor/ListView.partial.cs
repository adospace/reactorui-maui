//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MauiReactor
//{
//    public partial interface IViewCellListView : IListView
//    {
//        Func<object, VisualNode>? ItemTemplate { get; set; }
//    }

//    public partial class ViewCellListView : ListView, IViewCellListView
//    {
//        Func<object, VisualNode>? IViewCellListView.ItemTemplate { get; set; }

//        private class ViewCellItemTemplatePresenter : Microsoft.Maui.Controls.ViewCell
//        {
//            private ItemTemplateNode? _itemTemplateNode;
//            private readonly CustomDataTemplate _template;

//            public ViewCellItemTemplatePresenter(CustomDataTemplate template)
//            {
//                _template = template;
//            }

//            protected override void OnBindingContextChanged()
//            {
//                while (true)
//                {
//                    var item = BindingContext;

//                    if (item == null)
//                        break;

//                    VisualNode? newRoot = _template.GetVisualNodeForItem(item);

//                    if (newRoot == null)
//                        break;

//                    ((IVisualNodeWithNativeControl)newRoot).Attach(this);

//                    //if (_itemTemplateNode != null)
//                    //{
//                    //    _itemTemplateNode.Root = newRoot;
//                    //}
//                    //else
//                    //{
//                    _itemTemplateNode = new ItemTemplateNode(newRoot, this, _template.Owner);
//                    _itemTemplateNode.Layout();
//                    //}

//                    break;
//                }

//                base.OnBindingContextChanged();
//            }
//        }

//        protected class ViewCellDataTemplate : IVisualNodeForItemFactory
//        {
//            public ViewCellDataTemplate(ViewCellListView owner)
//            {

//            }

//            public DataTemplate DataTemplate { get; }

//            public IGenericItemsView Owner { get; set; }

//            public VisualNode? GetVisualNodeForItem(object item)
//            {
//                IViewCellListView itemsView = (IViewCellListView)Owner;

//                return itemsView.ItemTemplate?.Invoke(item);
//            }
//        }
//    }
//}
