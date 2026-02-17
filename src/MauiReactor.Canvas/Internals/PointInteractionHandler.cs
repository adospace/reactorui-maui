using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals
{
    public class PointInteractionHandlerEventArgs : EventArgs
    {
        public PointInteractionHandlerEventArgs(PointF[] points)
        {
            Points = points;
        }

        public PointF[] Points { get; }
    }

    public class PointInteractionHandler : CanvasNode, IDragInteractionHandler, IHoverInteractionHandler
    {
        private RectF _hitRect;

        public event EventHandler<PointInteractionHandlerEventArgs>? TapDown;
        public event EventHandler<PointInteractionHandlerEventArgs>? TapUp;
        public event EventHandler? Tap;

        public event EventHandler<PointInteractionHandlerEventArgs>? HoverIn;
        public event EventHandler? HoverOut;

        public CanvasNode? Child => Children.Count > 0 ? Children[0] : null;

        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(PointInteractionHandler), true);
        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }


        public static readonly BindableProperty TapEventThresholdProperty = BindableProperty.Create(nameof(TapEventThreshold), typeof(float), typeof(PointInteractionHandler), 5f);
        public float TapEventThreshold
        {
            get => (float)GetValue(TapEventThresholdProperty);
            set => SetValue(TapEventThresholdProperty, value);
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

        public void SendTap()
        {
            Tap?.Invoke(this, EventArgs.Empty);
        }

        public void SendTapDown(PointF[]? touchPoints = null)
        {
            TapDown?.Invoke(this, new PointInteractionHandlerEventArgs(touchPoints ?? Array.Empty<PointF>()));
        }

        public void SendTapUp(PointF[]? touchPoints = null)
        {
            TapUp?.Invoke(this, new PointInteractionHandlerEventArgs(touchPoints ?? Array.Empty<PointF>()));
        }

        public void StartDrag(PointF[] touchPoints)
        {
            _touchPoints = touchPoints;
            TapDown?.Invoke(this, new PointInteractionHandlerEventArgs(touchPoints));
        }

        public void StopDrag(PointF[] touchPoints)
        {
            if (_touchPoints != null &&
                _touchPoints.Length > 0)
            {
                if (touchPoints.Length > 0 &&
                    touchPoints[0].Distance(_touchPoints[0]) < TapEventThreshold)
                {
                    Tap?.Invoke(this, EventArgs.Empty);
                }

                _touchPoints = null;
            }

            TapUp?.Invoke(this, new PointInteractionHandlerEventArgs(touchPoints));
        }

        public void StartHover(PointF[] touchPoints)
        {
            HoverIn?.Invoke(this, new PointInteractionHandlerEventArgs(touchPoints));
        }

        public void MoveHover(PointF[] touchPoints)
        {
            HoverIn?.Invoke(this, new PointInteractionHandlerEventArgs(touchPoints));
        }

        public void CancelHover()
        {
            HoverOut?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnDraw(DrawingContext context)
        {
            _hitRect = context.DirtyRect;

            Child?.Draw(context);

            if (Tap != null || TapDown != null || TapUp != null)
            {
                context.DragInteractionHandlers.Add(this);
            }

            if (HoverIn != null || HoverOut != null) 
            {
                context.HoverInteractionHandlers.Add(this);
            }           

            base.OnDraw(context);
        }
    }
}
