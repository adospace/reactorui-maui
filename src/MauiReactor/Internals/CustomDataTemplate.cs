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

        public CustomDataTemplate(ICustomDataTemplateOwner owner, Action<ItemTemplatePresenter>? constructorInjector = null)
        {
            Owner = owner;
            DataTemplate = new DataTemplate(() =>
            {
                var itemTemplatePresenter = new ItemTemplatePresenter(this);
                constructorInjector?.Invoke(itemTemplatePresenter);
                return itemTemplatePresenter;
            });
        }

        public VisualNode? GetVisualNodeForItem(object item)
        {
            return Owner.ItemTemplate?.Invoke(item);
        }
    }
}
