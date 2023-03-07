using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals
{
    internal class ItemTemplateNode : ContentView<Microsoft.Maui.Controls.ContentView>, IVisualNode
    {

        public ItemTemplateNode(CustomDataTemplate dataTemplate)
        {
            _dataTemplate = dataTemplate;
        }

        private VisualNode? _root;
        private readonly CustomDataTemplate _dataTemplate;

        public VisualNode? Root
        {
            get => _root;
            set
            {
                if (_root != value)
                {
                    _root = value;

                    try
                    {
                        //we want the animations to restart instead of migrating from an old visual node
                        _skipAnimationMigration = true;
                        Invalidate();
                    }
                    finally
                    {
                        _skipAnimationMigration = false;
                    }
                }
            }
        }

        internal Microsoft.Maui.Controls.ContentView? ItemContainer => NativeControl;

        protected override void OnMount()
        {
            base.OnMount();

            Validate.EnsureNotNull(NativeControl);
            NativeControl.BindingContextChanged += NativeControl_BindingContextChanged;
        }

        private void NativeControl_BindingContextChanged(object? sender, EventArgs e)
        {
            Validate.EnsureNotNull(NativeControl);

            if (NativeControl.BindingContext == null)
            {
                Root = null;
                return;
            }

            Root = _dataTemplate.GetVisualNodeForItem(NativeControl.BindingContext);
        }

        protected override void OnUnmount()
        {
            Validate.EnsureNotNull(NativeControl);
            NativeControl.BindingContextChanged -= NativeControl_BindingContextChanged;

            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            if (_root != null)
            {
                yield return _root;
            }
        }

        internal override VisualNode? Parent
        {
            get => _dataTemplate.Owner as VisualNode;
            set => throw new InvalidOperationException();
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (nativeControl is View view)
                NativeControl.Content = view;
            else
            {
                throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
        }

        protected internal override void OnLayoutCycleRequested()
        {
            Layout();
            base.OnLayoutCycleRequested();
        }
    }
}
