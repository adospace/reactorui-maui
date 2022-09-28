using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System.Diagnostics;

namespace MauiReactor.Canvas.Internals
{
    [DebuggerDisplay("Left={Left}, Top={Top}, Right={Right}, Bottom={Bottom}, HorizontalThickness={HorizontalThickness}, VerticalThickness={VerticalThickness}")]
    public struct ThicknessF
    {
        public float Left { get; set; }

        public float Top { get; set; }

        public float Right { get; set; }

        public float Bottom { get; set; }

        public float HorizontalThickness => Left + Right;

        public float VerticalThickness => Top + Bottom;

        public bool IsEmpty => Left == 0 && Top == 0 && Right == 0 && Bottom == 0;

        public bool IsNaN => float.IsNaN(Left) && float.IsNaN(Top) && float.IsNaN(Right) && float.IsNaN(Bottom);

        public ThicknessF(float uniformSize) : this(uniformSize, uniformSize, uniformSize, uniformSize)
        {
        }

        public ThicknessF(float horizontalSize, float verticalSize) : this(horizontalSize, verticalSize, horizontalSize, verticalSize)
        {
        }

        public ThicknessF(float left, float top, float right, float bottom) : this()
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static implicit operator ThicknessF(SizeF size)
        {
            return new ThicknessF(size.Width, size.Height, size.Width, size.Height);
        }

        public static implicit operator ThicknessF(float uniformSize)
        {
            return new ThicknessF(uniformSize);
        }

        bool Equals(ThicknessF other)
        {
            return Left.Equals(other.Left) && Top.Equals(other.Top) && Right.Equals(other.Right) && Bottom.Equals(other.Bottom);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is ThicknessF && Equals((ThicknessF)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Left.GetHashCode();
                hashCode = (hashCode * 397) ^ Top.GetHashCode();
                hashCode = (hashCode * 397) ^ Right.GetHashCode();
                hashCode = (hashCode * 397) ^ Bottom.GetHashCode();
                return hashCode;
            }
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

        public static ThicknessF Zero = new ThicknessF(0);

        public static ThicknessF operator +(ThicknessF left, float addend) =>
            new ThicknessF(left.Left + addend, left.Top + addend, left.Right + addend, left.Bottom + addend);

        public static ThicknessF operator +(ThicknessF left, ThicknessF right) =>
            new ThicknessF(left.Left + right.Left, left.Top + right.Top, left.Right + right.Right, left.Bottom + right.Bottom);

        public static ThicknessF operator -(ThicknessF left, float addend) =>
            left + (-addend);
    }

}