using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MauiReactor
{
    public interface IRxTheme
    {
        RxTheme StyleFor<TS>(Action<TS> action) where TS : IRxElement;
    }

    public class RxTheme : VisualNode, IEnumerable<VisualNode>, IRxTheme
    {
        private readonly List<VisualNode> _internalChildren = new();

        private readonly Dictionary<Type, Action<IRxElement>> _styles = new();

        internal sealed override void Layout(RxTheme theme/*, VisualNode parent = null*/)
        {
            base.Layout(this/*, parent*/);
        }

        internal Action<IRxElement> GetStyleFor(IRxElement node)
        {
            if (_styles.TryGetValue(node.GetType(), out var styleAction))
            {
                return styleAction;
            }

            return null;
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode[] nodes)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            foreach (var node in nodes)
                _internalChildren.Add(node);
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            Parent.AddChild(this, nativeControl);
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            Parent.RemoveChild(this, nativeControl);
        }

        public RxTheme StyleFor<TS>(Action<TS> action) where TS : IRxElement
        {
            _styles[typeof(TS)] = node => action((TS)node);
            return this;
        }
    }

    public static class RxThemeExtensions
    {
        public static T StyleFor<T, TS>(this T theme, Action<TS> action) where T : IRxTheme where TS : IRxElement
        {
            theme.StyleFor<TS>(action);
            return theme;
        }

        public static RxTheme UseTheme<T>(this T node, RxTheme theme) where T : VisualNode
        {
            theme.Add(node);
            return theme;
        }

        public static RxTheme UseTheme<T>(this IEnumerable<T> node, RxTheme theme) where T : VisualNode
        {
            theme.Add(node.ToArray());
            return theme;
        }
    }
}
