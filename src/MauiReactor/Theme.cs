//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MauiReactor;

//public class Theme
//{
//    private static readonly Dictionary<Type, List<StyleItem>> _styles = [];

//    public record StyleItem(Action<VisualNode> StyleAction, string? StyleKey = null);

//    public static void RemoveAllStyles()
//    {
//        _styles.Clear();
//    }

//    public static void RemoveStyleFor<T>(string? StyleKey = null)
//    {
//        if (StyleKey == null)
//        {
//            _styles.Remove(typeof(T));
//        }
//        else
//        {
//            if (_styles.TryGetValue(typeof(T), out var listOfStyleItems))
//            {
//                listOfStyleItems.RemoveAll(_ => _.StyleKey == StyleKey);
//                if (listOfStyleItems.Count == 0)
//                {
//                    _styles.Remove(typeof(T));
//                }
//            }
//        }
//    }

//    public static void StyleFor<T>(Action<T> StyleAction, string? StyleKey = null) where T : VisualNode
//    {
//        if (!_styles.TryGetValue(typeof(T), out var listOfStyleItems))
//        {
//            _styles[typeof(T)] = listOfStyleItems = [];
//        }

//        listOfStyleItems.Add(new StyleItem(visualNode => StyleAction((T)visualNode), StyleKey));
//    }

//    internal static void Style(VisualNode visualNode, string? StyleKey = null)
//    {
//        if (_styles.TryGetValue(visualNode.GetType(), out var listOfStyleItems))
//        {
//            var styleItem = listOfStyleItems.FirstOrDefault(_=>_.StyleKey == StyleKey);
//            styleItem?.StyleAction(visualNode);
//        }
//    }
//}
