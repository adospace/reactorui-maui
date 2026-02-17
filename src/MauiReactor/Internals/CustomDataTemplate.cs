using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals
{
    public interface ICustomDataTemplateOwner
    {
        VisualNode? GetVisualNodeForItem(object item);
    }

    public class CustomDataTemplate<T> where T : Microsoft.Maui.Controls.Element, Microsoft.Maui.IContentView, new()
    {
        public DataTemplate DataTemplate { get; }

        private readonly List<WeakReference<ItemDataTemplateNode<T>>> _itemTemplateNodeRefs = [];

        WeakReference<ICustomDataTemplateOwner>? _ownerRef;

        public ICustomDataTemplateOwner? Owner
        {
            get
            {
                if (_ownerRef != null)
                {
                    _ownerRef.TryGetTarget(out var owner);
                    return owner;
                }

                return null;
            }
            set
            {
                _ownerRef = null;
                if (value != null)
                {
                    _ownerRef = new WeakReference<ICustomDataTemplateOwner>(value);
                }
            }
        }

        public CustomDataTemplate(ICustomDataTemplateOwner owner, Action<T?>? constructorInjector = null)
        {
            Owner = owner;
            DataTemplate = new DataTemplate(() =>
            {
                var itemTemplateNode = new ItemDataTemplateNode<T>(this);
                itemTemplateNode.Layout();
                constructorInjector?.Invoke(itemTemplateNode.ItemContainer);

                _itemTemplateNodeRefs.Add(new WeakReference<ItemDataTemplateNode<T>>(itemTemplateNode));

                return itemTemplateNode.ItemContainer;
            });
        }

        public virtual VisualNode? GetVisualNodeForItem(object item)
        {
            return Owner?.GetVisualNodeForItem(item);
        }

        public void Update()
        {
            foreach(var templateNodeItemRef in _itemTemplateNodeRefs)
            {
                if (templateNodeItemRef.TryGetTarget(out var itemTemplateNode))
                {
                    itemTemplateNode.Update();
                }
            }
        }
    }

    public class CustomDataTemplate : CustomDataTemplate<Microsoft.Maui.Controls.ContentView>
    {
        public CustomDataTemplate(ICustomDataTemplateOwner owner, Action<Microsoft.Maui.Controls.ContentView?>? constructorInjector = null)
            : base(owner, constructorInjector)
        {
        }
    }

    public class CustomControlTemplate
    {
        public ControlTemplate ControlTemplate { get; }

        private readonly List<WeakReference<ItemControlTemplateNode>> _itemTemplateNodeRefs = [];

        WeakReference<ICustomDataTemplateOwner>? _ownerRef;

        public ICustomDataTemplateOwner? Owner
        {
            get
            {
                if (_ownerRef != null)
                {
                    _ownerRef.TryGetTarget(out var owner);
                    return owner;
                }

                return null;
            }
            set
            {
                _ownerRef = null;
                if (value != null)
                {
                    _ownerRef = new WeakReference<ICustomDataTemplateOwner>(value);
                }
            }
        }

        public CustomControlTemplate(ICustomDataTemplateOwner owner, Action<Microsoft.Maui.Controls.ContentView?>? constructorInjector = null)
        {
            Owner = owner;
            ControlTemplate = new ControlTemplate(() =>
            {
                var itemTemplateNode = new ItemControlTemplateNode(this);
                itemTemplateNode.Layout();
                constructorInjector?.Invoke(itemTemplateNode.ItemContainer);

                _itemTemplateNodeRefs.Add(new WeakReference<ItemControlTemplateNode>(itemTemplateNode));

                return itemTemplateNode.ItemContainer;
            });
        }

        public virtual VisualNode? GetVisualNodeForItem(object item)
        {
            return Owner?.GetVisualNodeForItem(item);
        }

        public void Update()
        {
            foreach (var templateNodeItemRef in _itemTemplateNodeRefs)
            {
                if (templateNodeItemRef.TryGetTarget(out var itemTemplateNode))
                {
                    itemTemplateNode.Update();
                }
            }
        }
    }

}
