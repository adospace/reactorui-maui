using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MauiReactor.Canvas.Internals
{
    public sealed class ItemsRepeater<T> : CanvasVisualElement
    {
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(IReadOnlyList<T>), typeof(ItemsRepeater<T>), null);

        public IReadOnlyList<T>? Items
        {
            get => (IReadOnlyList<T>?)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(Func<T, CanvasNode?>), typeof(ItemsRepeater<T>), null);

        public Func<T, CanvasNode?>? ItemTemplate
        {
            get => (Func<T, CanvasNode?>?)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(ItemsLayoutOrientation), typeof(ItemsRepeater<T>), ItemsLayoutOrientation.Vertical);

        public ItemsLayoutOrientation Orientation
        {
            get => (ItemsLayoutOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(nameof(ItemWidth), typeof(float), typeof(ItemsRepeater<T>), 100.0f);

        public float ItemWidth
        {
            get => (float)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create(nameof(ItemHeight), typeof(float), typeof(ItemsRepeater<T>), 100.0f);

        public float ItemHeight
        {
            get => (float)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public static readonly BindableProperty ViewportXProperty = BindableProperty.Create(nameof(ViewportX), typeof(float), typeof(ItemsRepeater<T>), 0.0f);

        public float ViewportX
        {
            get => (float)GetValue(ViewportXProperty);
            set => SetValue(ViewportXProperty, value);
        }

        public static readonly BindableProperty ViewportYProperty = BindableProperty.Create(nameof(ViewportY), typeof(float), typeof(ItemsRepeater<T>), 0.0f);

        public float ViewportY
        {
            get => (float)GetValue(ViewportYProperty);
            set => SetValue(ViewportYProperty, value);
        }

        protected override void OnDraw(DrawingContext context)
        {
            if (Items?.Count > 0 && ItemTemplate != null)
            {
                context.Canvas.SaveState();

                if (Orientation == ItemsLayoutOrientation.Horizontal && ItemWidth > 0)
                {
                    var oldRect = context.DirtyRect;

                    int itemIndex = (int)(ViewportX / ItemWidth);

                    var currentOffset = oldRect.X + ViewportX - itemIndex * ItemWidth;

                    for (; itemIndex < Items.Count; itemIndex++)
                    {
                        if (currentOffset > oldRect.Width)
                        {
                            break;
                        }

                        var nodeToRender = ItemTemplate(Items[itemIndex]);
                        context.DirtyRect = new RectF(currentOffset, oldRect.Y, ItemWidth, oldRect.Height);
                        nodeToRender?.Draw(context);                       

                        currentOffset += ItemWidth;
                    }

                    context.DirtyRect = oldRect;
                }
                else if (Orientation == ItemsLayoutOrientation.Vertical && ItemHeight > 0)
                {
                    var oldRect = context.DirtyRect;

                    int itemIndex = (int)(ViewportY / ItemHeight);

                    var currentOffset = oldRect.Y + ViewportY - itemIndex * ItemHeight;

                    for (; itemIndex < Items.Count; itemIndex++)
                    {
                        if (currentOffset > oldRect.Height)
                        {
                            break;
                        }

                        var nodeToRender = ItemTemplate(Items[itemIndex]);
                        context.DirtyRect = new RectF(oldRect.X, currentOffset, oldRect.Width, ItemHeight);
                        nodeToRender?.Draw(context);

                        currentOffset += ItemHeight;
                    }

                    context.DirtyRect = oldRect;
                }

                context.Canvas.RestoreState();
            }

            base.OnDraw(context);
        }
    }

}
