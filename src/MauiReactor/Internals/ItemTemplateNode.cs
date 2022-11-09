using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals
{
    public class ItemTemplateNode : VisualNode, IVisualNode//, IHostElement
    {
        private readonly ItemTemplatePresenter _presenter;

        public ItemTemplateNode(VisualNode root, ItemTemplatePresenter presenter)
        {
            _root = root;
            _presenter = presenter;
        }

        private VisualNode _root;

        public VisualNode Root
        {
            get => _root;
            set
            {
                if (_root != value)
                {
                    _root = value;
                    Invalidate();
                }
                else
                {
                    _root.Update();
                }
            }
        }

        internal override VisualNode? Parent
        {
            get => _presenter.Template.Owner as VisualNode;
            set => throw new InvalidOperationException();
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            Validate.EnsureNotNull(_presenter);

            if (nativeControl is View view)
                _presenter.Content = view;
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
    }
}
