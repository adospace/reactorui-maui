using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals
{
    public class ItemTemplateHost<T> : VisualNode where T : BindableObject
    {
        public ItemTemplateHost(VisualNode root)
        {
            _root = root;
        }

        private VisualNode _root;

        public VisualNode Root
        {
            get => _root;
        }

        public T? NativeElement { get; private set; }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is T nativeElement)
                NativeElement = nativeElement;
            else
            {
                throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Root;
        }

        protected internal override void OnLayoutCycleRequested()
        {
            Layout();
            base.OnLayoutCycleRequested();
        }

        public T? GenerateNativeElement()
        {
            Layout();
            return NativeElement;
        }
    }
}
