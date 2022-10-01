using System;
using System.Collections.Generic;
using System.Linq;
using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public class Column : NodeContainer
    {
        private static readonly GridLengthTypeConverter _gridLengthTypeConverter = new GridLengthTypeConverter();

        public static readonly BindableProperty RowsProperty = BindableProperty.Create(nameof(Rows), typeof(string), typeof(Column), "*",
            coerceValue: (bindableObject, value) => string.IsNullOrWhiteSpace((string)value) ? "*" : value);


        public string Rows
        {
            get => (string)GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        private static IEnumerable<RowDefinition> ParseRows(string rows)
        {
            foreach (var rowDefinition in rows.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
                .Select(_ => new RowDefinition() { Height = _ }))
            {
                yield return rowDefinition;
            }
        }

        private IEnumerable<float> GetRowWidths(double width)
        {
            var rows = ParseRows(Rows).ToList();

            while (rows.Count < Children.Count)
            {
                rows.Add(new RowDefinition() { Height = rows[rows.Count - 1].Height });
            }

            var heightForStarRows = Math.Max(0.0, width - rows.Where(_ => _.Height.IsAbsolute).Sum(_ => _.Height.Value));
            var starSum = rows.Where(_ => _.Height.IsStar).Sum(_ => _.Height.Value);
            var starHeight = starSum > 0 ? heightForStarRows / starSum : 0.0;

            foreach (var row in rows)
            {
                if (row.Height.IsAbsolute)
                {
                    yield return (float)row.Height.Value;
                }
                else if (row.Height.IsStar)
                {
                    yield return (float)(row.Height.Value * starHeight);
                }
                else
                {
                    throw new NotSupportedException("Auto sizing is not supported");
                }
            }
        }

        protected override void OnDraw(DrawingContext context)
        {
            var rowHeights = GetRowWidths(context.DirtyRect.Width).ToArray();
            var dirtyRect = context.DirtyRect;
            var y = dirtyRect.Y;
            for (int i = 0; i < Children.Count; i++)
            {
                context.DirtyRect = new RectF(
                    dirtyRect.Left,
                    y,
                    dirtyRect.Height,
                    rowHeights[i]
                    );

                Children[i].Draw(context);

                y += rowHeights[i];
            }

            base.OnDraw(context);
        }
    }

}