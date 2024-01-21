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

    public class CustomDataTemplate
    {
        public DataTemplate DataTemplate { get; }

        private readonly List<WeakReference<ItemTemplateNode>> _itemTemplateNodeRefs = [];

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

        public CustomDataTemplate(ICustomDataTemplateOwner owner, Action<Microsoft.Maui.Controls.ContentView?>? constructorInjector = null)
        {
            Owner = owner;
            DataTemplate = new DataTemplate(() =>
            {
                var itemTemplateNode = new ItemTemplateNode(this);
                itemTemplateNode.Layout();
                constructorInjector?.Invoke(itemTemplateNode.ItemContainer);

                _itemTemplateNodeRefs.Add(new WeakReference<ItemTemplateNode>(itemTemplateNode));

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
}
