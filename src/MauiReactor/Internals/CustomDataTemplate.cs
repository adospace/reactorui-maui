using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals
{
    public interface ICustomDataTemplateOwner
    {
        Func<object, VisualNode>? ItemTemplate { get; }
    }

    public class CustomDataTemplate
    {
        public DataTemplate DataTemplate { get; }
        public ICustomDataTemplateOwner Owner { get; set; }

        public CustomDataTemplate(ICustomDataTemplateOwner owner, Action<Microsoft.Maui.Controls.ContentView?>? constructorInjector = null)
        {
            Owner = owner;
            DataTemplate = new DataTemplate(() =>
            {
                var itemTemplateNode = new ItemTemplateNode(this);
                itemTemplateNode.Layout();
                constructorInjector?.Invoke(itemTemplateNode.ItemContainer);
                return itemTemplateNode.ItemContainer;
            });
        }

        public VisualNode? GetVisualNodeForItem(object item)
        {
            return Owner.ItemTemplate?.Invoke(item);
        }
    }
}
