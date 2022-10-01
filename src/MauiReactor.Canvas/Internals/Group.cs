﻿namespace MauiReactor.Canvas.Internals
{
    public class Group : NodeContainer
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