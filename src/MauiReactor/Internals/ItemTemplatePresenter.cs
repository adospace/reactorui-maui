using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals
{
    public class ItemTemplatePresenter : Microsoft.Maui.Controls.ContentView
    {
        private ItemTemplateNode? _itemTemplateNode;

        public ItemTemplatePresenter(CustomDataTemplate template)
        {
            Template = template;
            //VisualStateManager.SetVisualStateGroups(this, template.Owner.ItemVisualStateGroups);
        }

        public CustomDataTemplate Template { get; }

        protected override void OnBindingContextChanged()
        {
            while (true)
            {
                var item = BindingContext;

                if (item == null)
                    break;

                VisualNode? newRoot = Template.GetVisualNodeForItem(item);

                if (newRoot == null)
                    break;

                _itemTemplateNode = new ItemTemplateNode(newRoot, this);
                _itemTemplateNode.Layout();

                break;
            }

            base.OnBindingContextChanged();
        }
    }
}
