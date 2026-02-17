using MauiReactor.Internals;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System;
using System.Diagnostics;

namespace MauiReactor
{
    [DebuggerDisplay("Left={Left}, Top={Top}, Right={Right}, Bottom={Bottom}, HorizontalThickness={HorizontalThickness}, VerticalThickness={VerticalThickness}")]
    public struct ThicknessF : IEquatable<ThicknessF>
    {
        public float Left { get; set; }

        public float Top { get; set; }

        public float Right { get; set; }

        public float Bottom { get; set; }

        public float HorizontalThickness => Left + Right;

        public float VerticalThickness => Top + Bottom;

        public bool IsEmpty => Left == 0 && Top == 0 && Right == 0 && Bottom == 0;

        public bool IsNaN => float.IsNaN(Left) && float.IsNaN(Top) && float.IsNaN(Right) && float.IsNaN(Bottom);

        public ThicknessF()
            :this(0.0f)
        { }

        public ThicknessF(float uniformSize) : this(uniformSize, uniformSize, uniformSize, uniformSize)
        {
        }

        public ThicknessF(float horizontalSize, float verticalSize) : this(horizontalSize, verticalSize, horizontalSize, verticalSize)
        {
        }

        public ThicknessF(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static implicit operator ThicknessF(SizeF size)
        {
            return new(size.Width, size.Height, size.Width, size.Height);
        }

        public static implicit operator ThicknessF(float uniformSize)
        {
            return new(uniformSize);
        }

        public bool Equals(ThicknessF other)
        {
            return Left.Equals(other.Left) && Top.Equals(other.Top) && Right.Equals(other.Right) && Bottom.Equals(other.Bottom);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            return obj is ThicknessF f && Equals(f);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Left, Top, Right, Bottom);
        }

        public static bool operator ==(ThicknessF left, ThicknessF right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ThicknessF left, ThicknessF right)
        {
            return !left.Equals(right);
        }

        public void Deconstruct(out float left, out float top, out float right, out float bottom)
        {
            left = Left;
            top = Top;
            right = Right;
            bottom = Bottom;
        }

        private static readonly ThicknessF _zero = new(0);
        public static ThicknessF Zero => _zero;

        public static ThicknessF operator +(ThicknessF left, float addend) =>
            new(left.Left + addend, left.Top + addend, left.Right + addend, left.Bottom + addend);

        public static ThicknessF operator +(ThicknessF left, ThicknessF right) =>
            new(left.Left + right.Left, left.Top + right.Top, left.Right + right.Right, left.Bottom + right.Bottom);

        public static ThicknessF operator -(ThicknessF left, float addend) =>
            left + (-addend);

        public bool UniformSize()
            => FloatUtils.AreClose(Left, Top, Right, Bottom);
    }

}