using System.Linq;

namespace MauiReactor.Canvas.Internals
{
    public class Group : CanvasVisualElement
    {
        protected override void OnDraw(DrawingContext context)
        {
            foreach(var child in Children.OrderBy(_=>_.ZIndex))
            {
                child.Draw(context);
            }

            base.OnDraw(context);
        }

    }

}