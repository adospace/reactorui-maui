﻿using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial interface IShellContent
{
    Func<VisualNode>? ContentTemplate { get; set; }
}

public partial class ShellContent<T> : IAutomationItemContainer
{
    Func<VisualNode>? IShellContent.ContentTemplate { get; set; }
    private ContentTemplate? _contentTemplate;

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

        public ContentTemplate(ShellContent<T> owner)
        {
            Owner = owner;
            DataTemplate = new DataTemplate(() =>
            {
                Root = ((IShellContent)Owner).ContentTemplate?.Invoke();
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

        protected override IEnumerable<VisualNode?> RenderChildren()
        {
            if (_root != null)
            {
                yield return _root;
            }
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is Microsoft.Maui.Controls.Page page)
                _nativeContent = page;
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under 'ShellContent'");
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
        }

        public new void Update()
        {
            Root = ((IShellContent)Owner).ContentTemplate?.Invoke();
        }
    }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        if (_contentTemplate != null)
        {
            return [_contentTemplate];
        }

        return base.RenderChildren();
    }

    protected override void OnUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsShellContent = (IShellContent)this;

        if (thisAsShellContent.ContentTemplate != null)
        {
            if (_contentTemplate == null)
            {
                _contentTemplate = new ContentTemplate(this);
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
        base.OnUpdate();
    }

    partial void Migrated(VisualNode newNode)
    {
        var newShellContent = ((ShellContent<T>)newNode);
        newShellContent._contentTemplate = _contentTemplate;
        if (newShellContent._contentTemplate != null)
        {
            newShellContent._contentTemplate.Owner = newShellContent;
        }
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


    WeakReference<object>? _loadedForciblyChild;

    IEnumerable<TChild> IAutomationItemContainer.Descendants<TChild>()
    {
        if (_loadedForciblyChild == null)
        {
            ForceContentLoad();
        }

        if (_loadedForciblyChild != null &&
            _loadedForciblyChild.TryGetTarget(out var child))
        {
            if (child is TChild childT)
            {
                yield return childT;
            }

            if (child is IVisualNodeWithNativeControl childVisualNodeWithNativeControl)
            {
                var childNativeControl = childVisualNodeWithNativeControl.GetNativeControl<BindableObject>();

                if (childNativeControl is TChild childNativeControlAsT)
                {
                    yield return childNativeControlAsT;
                }
            }

            foreach (var childChildT in ((IAutomationItemContainer)child).Descendants<TChild>())
            {
                yield return childChildT;
            }
        }
    }

    private void ForceContentLoad()
    {
        Validate.EnsureNotNull(NativeControl);

        _loadedForciblyChild = null;

        if (_contentTemplate != null)
        {
            NativeControl.ContentTemplate.CreateContent();

            _loadedForciblyChild = new WeakReference<object>(_contentTemplate);
        }
    }

}

public partial class ShellContent
{
    public ShellContent(string title) => 
        this.Title(title);

    public ShellContent(string title, string icon) => 
        this.Title(title).Icon(icon);
}

public static partial class ShellContentExtensions
{
    public static T RenderContent<T>(this T shellContent, Func<VisualNode> template) where T : IShellContent
    {
        shellContent.ContentTemplate = template;
        return shellContent;
    }
}
public partial class Component
{
    public static ShellContent ShellContent(string title)
        => new ShellContent().Title(title);

    public static ShellContent ShellContent(string title, Func<VisualNode> template)
        => new ShellContent().Title(title).RenderContent(template);
}