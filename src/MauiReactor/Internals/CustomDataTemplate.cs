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
        public ICustomDataTemplateOwner Owner { get; set; }

        private readonly List<ItemTemplateNode> _itemTemplateNodes = new();

        public CustomDataTemplate(ICustomDataTemplateOwner owner, Action<Microsoft.Maui.Controls.ContentView?>? constructorInjector = null)
        {
            Owner = owner;
            DataTemplate = new DataTemplate(() =>
            {
                var itemTemplateNode = new ItemTemplateNode(this);
                itemTemplateNode.Layout();
                constructorInjector?.Invoke(itemTemplateNode.ItemContainer);

                _itemTemplateNodes.Add(itemTemplateNode);

                return itemTemplateNode.ItemContainer;
            });
        }

        public virtual VisualNode? GetVisualNodeForItem(object item)
        {
            return Owner.GetVisualNodeForItem(item);
        }

        public void Update()
        {
            foreach(var templateNodeItem in _itemTemplateNodes)
            {
                templateNodeItem.Update();
            }
        }
    }
}
