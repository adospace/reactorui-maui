using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals
{
    public class PointInterationHandler : CanvasNode, IDragInteractionHandler
    {
        private RectF _hitRect;

        public event EventHandler? Tap;
        public CanvasNode? Child => Children.Count > 0 ? Children[0] : null;

        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(PointInterationHandler), true);
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

        private PointF[]? _touchPoints;

        public void StartDrag(PointF[] touchPoints)
        {
            _touchPoints = touchPoints;
        }

        public void StopDrag(PointF[] touchPoints)
        {
            if (_touchPoints != null &&
                _touchPoints.Length > 0)
            {
                if (touchPoints.Length > 0 &&
                    touchPoints[0].Distance(_touchPoints[0]) < 0.1)
                {
                    Tap?.Invoke(this, EventArgs.Empty);
                }

                _touchPoints = null;
            }
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
