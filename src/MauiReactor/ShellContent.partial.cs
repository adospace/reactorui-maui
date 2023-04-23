using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial interface IShellContent
    {
        Func<VisualNode>? ContentTemplate { get; set; }
    }

    public partial class ShellContent<T>
    {
        public ShellContent(string title)
            => this.Title(title);

        public ShellContent(VisualNode content)
        {
            _internalChildren.Add(content);
        }

        class ContentTemplate : VisualNode
        {
            private VisualNode? _root;
            private BindableObject? _nativeContent;
            private readonly Func<VisualNode> _template;

            public ContentTemplate(ShellContent<T> owner, Func<VisualNode> template)
            {
                Owner = owner;
                _template = template;
                DataTemplate = new DataTemplate(() =>
                {
                    Root = _template();
                    Layout();
                    return _nativeContent ?? throw new InvalidOperationException();
                });
            }

            public ShellContent<T> Owner { get; set; }
            public DataTemplate DataTemplate { get; }

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

            //internal override VisualNode? Parent
            //{
            //    get => Owner;
            //    set => throw new InvalidOperationException();
            //}

            protected override IEnumerable<VisualNode?> RenderChildren()
            {
                if (_root != null)
                {
                    yield return _root;
                }
            }

            protected sealed override void OnAddChild(VisualNode widget, BindableObject childControl)
            {
                _nativeContent = childControl;
            }

            protected sealed override void OnRemoveChild(VisualNode widget, BindableObject childControl)
            {
            }

            public new void Update()
            {
                Root = _template();
            }
        }

        Func<VisualNode>? IShellContent.ContentTemplate { get; set; }
        private ContentTemplate? _contentTemplate;

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            if (_contentTemplate != null)
            {
                return new[] { _contentTemplate };
            }

            return base.RenderChildren();
        }

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsShellContent = (IShellContent)this;

            if (thisAsShellContent.ContentTemplate != null)
            {
                if (_contentTemplate == null)
                {
                    _contentTemplate = new ContentTemplate(this, thisAsShellContent.ContentTemplate);
                    NativeControl.ContentTemplate = _contentTemplate.DataTemplate;
                }
                else
                {
                    _contentTemplate.Owner = this;
                    _contentTemplate.Update();
                }
            }
            else
            {
                NativeControl.ClearValue(Microsoft.Maui.Controls.ShellContent.ContentTemplateProperty);
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            var newShellContent = ((ShellContent<T>)newNode);
            newShellContent._contentTemplate = _contentTemplate;
            if (newShellContent._contentTemplate != null)
            {
                newShellContent._contentTemplate.Owner = newShellContent;
            }

            base.OnMigrated(newNode);
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.Page page)
                NativeControl.Content = page;

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (NativeControl.Content == childControl)
                NativeControl.Content = null;

            base.OnRemoveChild(widget, childControl);
        }

    }

    public partial class ShellContent
    {
        public ShellContent(string title) => this.Title(title);

        public ShellContent(string title, string icon) => this.Title(title).Icon(icon);
    }

    public static partial class ShellContentExtensions
    {
        public static T RenderContent<T>(this T shellContent, Func<VisualNode> template) where T : IShellContent
        {
            shellContent.ContentTemplate = template;
            return shellContent;
        }
    }
}
