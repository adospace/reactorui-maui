namespace MauiReactor.Canvas.Internals
{
    public class Group : CanvasNode
    {
        protected override void OnDraw(DrawingContext context)
        {
            foreach(var child in Children)
            {
                child.Draw(context);
            }

            base.OnDraw(context);
        }

    }

}