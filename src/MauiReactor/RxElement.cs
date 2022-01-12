using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace MauiReactor
{
    public interface IRxElement : IVisualNode
    {
        void SetAttachedProperty(BindableProperty property, object value);

        Action<object, System.ComponentModel.PropertyChangingEventArgs> PropertyChangingAction { get; set; }
        Action<object, PropertyChangedEventArgs> PropertyChangedAction { get; set; }
    }

    public abstract class RxElement<T> : VisualNode<T>, IRxElement where T : Element, new()
    {
        protected RxElement()
        { }

        protected RxElement(Action<T> componentRefAction)
            :base(componentRefAction)
        {
        }

        private bool _styled = false;
        private RxTheme _theme = null;

        internal override void Layout(RxTheme theme = null/*, VisualNode parent = null*/)
        {
            _theme = theme;
            base.Layout(theme/*, parent*/);
        }

        protected override void OnMount()
        {
            base.OnMount();

            if (!_styled)
            {
                var styleForMe = _theme?.GetStyleFor(this);
                if (styleForMe != null)
                {
                    var themedNode = (RxElement<T>)Activator.CreateInstance(GetType());
                    styleForMe(themedNode);
                    themedNode._nativeControl = _nativeControl;
                    themedNode.OnUpdate();
                    themedNode.OnMigrated(this);
                }

                _styled = true;
            }
        }
    }

    public static class RxElementExtensions
    {

    }
}