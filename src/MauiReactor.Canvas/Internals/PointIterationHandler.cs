using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals
{
    public class PointIterationHandler : CanvasNode, IDragInteractionHandler
    {
        private RectF _hitRect;

        public event EventHandler? Tap;
        public CanvasNode? Child => Children.Count > 0 ? Children[0] : null;

        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(PointIterationHandler), true);
        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public void CancelDrag()
        {
        }

        public void Drag(PointF[] touchPoints)
        {
        }

        public bool HitTest(PointF[] touchPoints)
        {
            if (IsEnabled)
            {
                return touchPoints.Any(_ => _hitRect.Contains(_));
            }

            return false;
        }

        public void StartDrag(PointF[] touchPoints)
        {
            Tap?.Invoke(this, EventArgs.Empty);
        }

        public void StopDrag(PointF[] touchPoints)
        {
        }

        protected override void OnDraw(DrawingContext context)
        {
            _hitRect = context.DirtyRect;

            Child?.Draw(context);

            context.DragInteractionHandlers.Insert(0, this);

            base.OnDraw(context);
        }
    }
}
