// Authors:
//   Jose Medrano <josmed@microsoft.com>
//
// Copyright (C) 2018 Microsoft, Corp
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the
// following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN
// NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE
// USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Newtonsoft.Json;

namespace FigmaSharp
{
    public class Padding
    {
        public Padding() { }

        public Padding(float value) : this(value, value, value, value)
        {

        }
        public Padding(float top, float left, float bottom, float right)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }
    }

    [Flags]
    public enum Key : ulong
    {
        A = 0uL,
        S = 1uL,
        D = 2uL,
        F = 3uL,
        H = 4uL,
        G = 5uL,
        Z = 6uL,
        X = 7uL,
        C = 8uL,
        V = 9uL,
        B = 11uL,
        Q = 12uL,
        W = 13uL,
        E = 14uL,
        R = 0xF,
        Y = 0x10,
        T = 17uL,
        D1 = 18uL,
        D2 = 19uL,
        D3 = 20uL,
        D4 = 21uL,
        D6 = 22uL,
        D5 = 23uL,
        Equal = 24uL,
        D9 = 25uL,
        D7 = 26uL,
        Minus = 27uL,
        D8 = 28uL,
        D0 = 29uL,
        RightBracket = 30uL,
        O = 0x1F,
        U = 0x20,
        LeftBracket = 33uL,
        I = 34uL,
        P = 35uL,
        L = 37uL,
        J = 38uL,
        Quote = 39uL,
        K = 40uL,
        Semicolon = 41uL,
        Backslash = 42uL,
        Comma = 43uL,
        Slash = 44uL,
        N = 45uL,
        M = 46uL,
        Period = 47uL,
        Grave = 50uL,
        KeypadDecimal = 65uL,
        KeypadMultiply = 67uL,
        KeypadPlus = 69uL,
        KeypadClear = 71uL,
        KeypadDivide = 75uL,
        KeypadEnter = 76uL,
        KeypadMinus = 78uL,
        KeypadEquals = 81uL,
        Keypad0 = 82uL,
        Keypad1 = 83uL,
        Keypad2 = 84uL,
        Keypad3 = 85uL,
        Keypad4 = 86uL,
        Keypad5 = 87uL,
        Keypad6 = 88uL,
        Keypad7 = 89uL,
        Keypad8 = 91uL,
        Keypad9 = 92uL,
        Return = 36uL,
        Tab = 48uL,
        Space = 49uL,
        Delete = 51uL,
        Escape = 53uL,
        Command = 55uL,
        Shift = 56uL,
        CapsLock = 57uL,
        Option = 58uL,
        Control = 59uL,
        RightShift = 60uL,
        RightOption = 61uL,
        RightControl = 62uL,
        Function = 0x3F,
        VolumeUp = 72uL,
        VolumeDown = 73uL,
        Mute = 74uL,
        ForwardDelete = 117uL,
        ISOSection = 10uL,
        JISYen = 93uL,
        JISUnderscore = 94uL,
        JISKeypadComma = 95uL,
        JISEisu = 102uL,
        JISKana = 104uL,
        F18 = 79uL,
        F19 = 80uL,
        F20 = 90uL,
        F5 = 96uL,
        F6 = 97uL,
        F7 = 98uL,
        F3 = 99uL,
        F8 = 100uL,
        F9 = 101uL,
        F11 = 103uL,
        F13 = 105uL,
        F16 = 106uL,
        F14 = 107uL,
        F10 = 109uL,
        F12 = 111uL,
        F15 = 113uL,
        Help = 114uL,
        Home = 115uL,
        PageUp = 116uL,
        F4 = 118uL,
        End = 119uL,
        F2 = 120uL,
        PageDown = 121uL,
        F1 = 122uL,
        LeftArrow = 123uL,
        RightArrow = 124uL,
        DownArrow = 125uL,
        UpArrow = 126uL
    }

    [Flags]
    public enum AnchorStyles
    {
        //
        // Summary:
        //     The control is not anchored to any edges of its container.
        None = 0,
        //
        // Summary:
        //     The control is anchored to the top edge of its container.
        Top = 1,
        //
        // Summary:
        //     The control is anchored to the bottom edge of its container.
        Bottom = 2,
        //
        // Summary:
        //     The control is anchored to the left edge of its container.
        Left = 4,
        //
        // Summary:
        //     The control is anchored to the right edge of its container.
        Right = 8
    }

    public class Color
    {
        public Color() { }
        public Color(double r, double g, double b, double a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
            A = 1;
        }

        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }
        public double A { get; set; }


        public static Color Red => new Color(1, 0, 0, 1);
        public static Color Green => new Color(0, 1, 0, 1);
        public static Color Blue => new Color(0, 0, 1, 1);
        public static Color Black => new Color(0, 0, 0, 1);
        public static Color White => new Color(1, 1, 1, 1);
        public static Color Transparent => new Color(0, 0, 0, 0);
    }

    public class Point : IEquatable<Point>
    {
        public Point() { }
        public Point(float? x, float? y)
        {
            X = x;
            Y = y;
        }

        public float? X { get; set; }
        public float? Y { get; set; }

        public bool Equals(Point other)
        {
            if (X != other.X || Y != other.Y)
            {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Size)) return false;
            return Equals((Size)obj);
        }

        public override string ToString()
        {
            return string.Format("{{{0},{1}}}", X, Y);
        }

        public static Point Zero { get; set; } = new Point();

        public Point Substract(Point center)
        {
            return new Point(X - center.X, Y - center.Y);
        }

        public Point UnionWith(Point center)
        {
            return new Point(X + center.X, Y + center.Y);
        }
    }

    public class Size : IEquatable<Size>
    {
        public Size() { }
        public Size(float? width, float? height)
        {
            Width = width;
            Height = height;
        }

        public float? Width { get; set; }
        public float? Height { get; set; }

        public bool Equals(Size other)
        {
            if (Width != other.Width || Height != other.Height)
            {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Size)) return false;
            return Equals((Size)obj);
        }

        public override string ToString()
        {
            return string.Format("{{{0},{1}}}", Width, Height);
        }

        public static Size Zero { get; set; } = new Size();
    }

    public class Rectangle : IEquatable<Rectangle>
    {

        readonly Size size = new Size();
        public Size Size
        {
            get => size;
            set
            {
                size.Width = value.Width;
                size.Height = value.Height;
            }
        }

        readonly Point origin = new Point();
        public Point Origin
        {
            get => origin;
            set
            {
                origin.X = value.X;
                origin.Y = value.Y;
            }
        }

        public float? X
        {
            get => origin.X;
            set => origin.X = value;
        }

        public float? Y
        {
            get => origin.Y;
            set => origin.Y = value;
        }

        public float? Width
        {
            get => size.Width;
            set => size.Width = value;
        }

        public float? Height
        {
            get => size.Height;
            set => size.Height = value;
        }

        public float? Left => X;
        public float? Right => X + Width;
        public float? Top => Y;
        public float? Bottom => Y + Height;

        public static Rectangle Zero { get; set; } = new Rectangle();

        public Rectangle()
        {

        }

        public Rectangle(Point origin, Size size)
        {
            this.origin = origin;
            this.size = size;
        }

        public Rectangle(float? x, float? y, float? width, float? height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        //public Rectangle UnionWith(Rectangle allocation)
        //{
        //    //TODO: improve
        //    float xMin = Math.Min(X, allocation.X);
        //    float yMin = Math.Min(Y, allocation.Y);
        //    float xMax = Math.Max(X + Width, allocation.X + allocation.Width);
        //    float yMax = Math.Max(Y + Height, allocation.Y + allocation.Height);
        //    return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
        //}

        public bool Equals(Rectangle other)
        {
            if (size.Equals(other.Size) && origin.Equals(other.Origin))
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Rectangle)) return false;
            return Equals((Rectangle)obj);
        }

        public bool IntersectsWith(Rectangle allocation)
        {
            return (Left < allocation.Right && Right > allocation.Left &&
     Top < allocation.Bottom && Bottom > allocation.Top);
        }

        public bool IntersectsWith(Rectangle allocation, bool reversed)
        {
            return (Left < allocation.Right && Right > allocation.Left &&
     Top <= allocation.Bottom && Bottom >= allocation.Top);
        }

        public override string ToString()
        {
            return string.Format("{{{0},{1},{2},{3}}}", X, Y, Width, Height);
        }

        public Point Center => new Point(Width / 2f, Height / 2f);

        public Rectangle Copy() => new Rectangle(X, Y, Width, Height);
    }
}

namespace FigmaSharp.Models
{
    #region Based Nodes

    public class FigmaElipse : FigmaVector
    {
        public override bool HasImage() => false;
    }

    public class FigmaStar : FigmaVector
    {
        
    }

    public class FigmaInstance : FigmaFrame
    {
        [Category ("General")]
        [DisplayName ("Component Id")]
        public string componentId { get; set; }

        [Category ("General")]
        [DisplayName ("Component")]
        public FigmaComponent Component { get; set; }

        public override bool HasImage() => false;
    }

    public class FigmaRegularPolygon : FigmaVector
    {
        
    }

    public class FigmaLine : FigmaVector
    {
        public override bool HasImage() => false;
    }

    public class RectangleVector : FigmaVector
    {
        public override bool HasImage() => false;

        [Category ("General")]
        [DisplayName ("Corner Radius")]
        public float cornerRadius { get; set; }

        [Category ("General")]
        [DisplayName ("RectangleCornerRadii")]
        public float[] rectangleCornerRadii { get; set; }
    }

    public class FigmaGroup : FigmaFrame
    {
        public override bool HasImage()
        {
            return false;
        }
    }

    public class FigmaSlice : FigmaNode, IAbsoluteBoundingBox, IConstraints
    {
        [Category("General")]
        [DisplayName("Absolute BoundingBox")]
        public Rectangle absoluteBoundingBox { get; set; }

        [Category("General")]
        [DisplayName("Constraints")]
        public FigmaLayoutConstraint constraints { get; set; }
    }

    public class FigmaFrame : FigmaNode, IFigmaDocumentContainer, IAbsoluteBoundingBox, IConstraints, IFigmaImage
    {
        public virtual bool HasImage()
        {
            return false;
        }

        [Category("General")]
        [Obsolete("Please use the fills field instead")]
        [DisplayName("Background Color")]
        [Description("[DEPRECATED] Background color of the node. This is deprecated, as frames now support more than a solid color as a background. Please use the fills field instead.")]
        public Color backgroundColor { get; set; }

        [Category("General")]
        [DisplayName("Export Settings")]
        public FigmaExportSetting[] exportSettings { get; set; }

        [Category("General")]
        [DisplayName("Blend Mode")]
        public string blendMode { get; set; }

        [Category("General")]
        [DisplayName("Preserve Ratio")]
        public bool preserveRatio { get; set; }

        [Category("General")]
        [DisplayName("Constraints")]
        public FigmaLayoutConstraint constraints { get; set; }

        [Category("General")]
        [Obsolete("Please use the fills field instead")]
        [DisplayName("Background")]
        [Description("[DEPRECATED] Background of the node. This is deprecated, as backgrounds for frames are now in the fills field.")]
        public FigmaBackground[] background { get; set; }

        [Category("Transition")]
        [DisplayName("Transition Node ID")]
        public string transitionNodeID { get; set; }

        [Category("Transition")]
        [DisplayName("Transition Duration")]
        public float transitionDuration { get; set; }

        [Category("Transition")]
        [DisplayName("Transition Easing")]
        public string transitionEasing { get; set; }

        [Category("General")]
        [DisplayName("Opacity")]
        [Description("Opacity of the node")]
        [DefaultValue(1f)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public float opacity { get; set; }

        [Category("General")]
        [DisplayName("Absolute BoundingBox")]
        [Description("Bounding box of the node in absolute space coordinates")]
        public Rectangle absoluteBoundingBox { get; set; }

        [Category("General")]
        [DisplayName("Size")]
        [Description("Width and height of element. This is different from the width and height of the bounding box in that the absolute bounding box represents the element after scaling and rotation. Only present if geometry=paths is passed")]
        public FigmaPoint size { get; set; }

        [Category("General")]
        [DisplayName("Relative Transform")]
        [Description("The top two rows of a matrix that represents the 2D transform of this node relative to its parent. The bottom row of the matrix is implicitly always (0, 0, 1). Use to transform coordinates in geometry. Only present if geometry=paths is passed")]
        public FigmaTransform relativeTransform { get; set; }

        [Category("General")]
        [DisplayName("Clips Content")]
        public bool clipsContent { get; set; }

        [Category("General")]
        [DisplayName("Layout Grids")]
        public FigmaLayoutGrid[] layoutGrids { get; set; }

        [Category("General")]
        [DisplayName("Effects")]
        public FigmaEffect[] effects { get; set; }

        [Category("General")]
        [DisplayName("Is Mask")]
        [Description("Does this node mask sibling nodes in front of it?")]
        public bool isMask { get; set; }

        [DisplayName("Children")]
        public FigmaNode[] children { get; set; }

        [DisplayName("Fills")]
        public FigmaPaint[] fills { get; set; }
        public bool HasFills => fills?.Length > 0;

        [DisplayName("Strokes")]
        public FigmaPaint[] strokes { get; set; }
        public bool HasStrokes => strokes?.Length > 0;

        [Category("Stroke")]
        [DisplayName("Stroke Weight")]
        public int? strokeWeight { get; set; }

        [Category("Stroke")]
        [DisplayName("Stroke Geometry")]
        public FigmaPath[] strokeGeometry { get; set; }

        [Category("Stroke")]
        [DisplayName("Stroke Align")]
        public string strokeAlign { get; set; }

        [Category("AutoLayout")]
        [DisplayName("Layout Align")]
        public string layoutAlign { get; set; }

        [Category("AutoLayout")]
        [DisplayName("Layout Mode")]
        public string layoutMode { get; set; }

        [Category("AutoLayout")]
        [DisplayName("Layout Mode")]
        public FigmaLayoutMode LayoutMode
        {
            get
            {
                if (string.IsNullOrEmpty(layoutMode))
                    return FigmaLayoutMode.None;

                foreach (var item in Enum.GetValues(typeof(FigmaLayoutMode)))
                {
                    if (item.ToString().ToUpper() == layoutMode)
                        return (FigmaLayoutMode)item;
                }
                return FigmaLayoutMode.None;
            }
        }

        [Category("AutoLayout")]
        [DisplayName("Item Spacing")]
        public float itemSpacing { get; set; }

        [Category("AutoLayout")]
        [DisplayName("Horizontal Padding")]
        public float horizontalPadding { get; set; }

        [DisplayName("Vertical Padding")]
        public float verticalPadding { get; set; }

        [Category("General")]
        [DisplayName("Corner Radius")]
        public float cornerRadius { get; set; }

        [Category("General")]
        [DisplayName("RectangleCornerRadii")]
        public float[] rectangleCornerRadii { get; set; }

        [Category("Style")]
        [DisplayName("Styles")]
        public Dictionary<string, string> styles { get; set; }
    }

    public class FigmaPoint : FigmaNode
    {
        [Category("General")]
        [DisplayName("X")]
        public float x { get; set; }

        [Category("General")]
        [DisplayName("Y")]
        public float y { get; set; }
    }

    public class FigmaVector : FigmaNode, IAbsoluteBoundingBox, IConstraints, IFigmaImage
    {
        public virtual bool HasImage()
        {
            return true;
        }

        [Category("General")]
        [DisplayName("Export Settings")]
        public FigmaExportSetting[] exportSettings { get; set; }

        [Category("General")]
        [DisplayName("Blend Mode")]
        public FigmaBlendMode blendMode { get; set; }

        [Category("General")]
        [DisplayName("Preserve Ratio")]
        public bool preserveRatio { get; set; }

        [Category("General")]
        [DisplayName("Constraints")]
        public FigmaLayoutConstraint constraints { get; set; }

        [Category("Transition")]
        [DisplayName("Transition Node ID")]
        public string transitionNodeID { get; set; }

        [Category("Transition")]
        [DisplayName("Transition Duration")]
        public float transitionDuration { get; set; }

        [Category("Transition")]
        [DisplayName("Transition Easing")]
        public FigmaEasingType transitionEasing { get; set; }

        [Category("General")]
        [DisplayName("Opacity")]
        [DefaultValue(1f)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public float opacity { get; set; }

        [Category("General")]
        [DisplayName("Absolute BoundingBox")]
        public Rectangle absoluteBoundingBox { get; set; }

        [Category("General")]
        [DisplayName("Effects")]
        public FigmaEffect[] effects { get; set; }

        [Category("General")]
        [DisplayName("Size")]
        public FigmaPoint size { get; set; }

        [Category("General")]
        [DisplayName("Relative Transform")]
        public FigmaTransform relativeTransform { get; set; }

        [Category("General")]
        [DisplayName("Is Mask")]
        public bool isMask { get; set; }

        [Category("General")]
        [DisplayName("FillGeometry")]
        public FigmaPath[] fillGeometry { get; set; }

        [Category("Stroke")]
        [DisplayName("Strokes")]
        public FigmaPaint[] strokes { get; set; }
        public bool HasStrokes => strokes?.Length > 0;

        [Category("Stroke")]
        [DisplayName("Stroke Weight")]
        public int? strokeWeight { get; set; }

        [Category("Stroke")]
        [DisplayName("Stroke Geometry")]
        public FigmaPath[] strokeGeometry { get; set; }

        [Category("Stroke")]
        [DisplayName("Stroke Align")]
        public string strokeAlign { get; set; }

        [Category("Stroke")]
        [DisplayName("Stroke Dashes")]
        public float[] strokeDashes { get; set; }

        [DisplayName("Fills")]
        public FigmaPaint[] fills { get; set; }
        public bool HasFills => fills?.Length > 0;

        [Category("Style")]
        [DisplayName("Styles")]
        public Dictionary<string, string> styles { get; set; }
    }

    public class FigmaBoolean : FigmaVector, IFigmaNodeContainer
    {
        [Category("General")]
        [DisplayName("Children")]
        public FigmaNode[] children { get; set; }

        [Category("General")]
        [DisplayName("Boolean Operation")]
        public string booleanOperation { get; set; }

        public override bool HasImage()
        {
            return false;
        }
    }

    public class FigmaCanvas : FigmaNode, IFigmaDocumentContainer
    {
        [Category("General")]
        [DisplayName("Background")]
        public FigmaPaint[] background { get; set; }

        [Category("General")]
        [DisplayName("Background Color")]
        public Color backgroundColor { get; set; }

        [Category("General")]
        [DisplayName("PrototypeStartNodeID")]
        public string prototypeStartNodeID { get; set; }

        [Category("General")]
        [DisplayName("Export Settings")]
        public FigmaExportSetting[] exportSettings { get; set; }

        [Category("General")]
        [DisplayName("Children")]
        public FigmaNode[] children { get; set; }

        [Category("General")]
        [DisplayName("Absolute BoundingBox")]
        public Rectangle absoluteBoundingBox { get; set; }
    }

    public class FigmaDocument : FigmaNode
    {
        public FigmaCanvas[] children { get; set; }
    }

    public class FigmaComponentEntity : FigmaFrame
    {

    }

    public class FigmaNode
    {
        [JsonIgnore()]
        [Category("General")]
        [DisplayName("Parent")]
        public FigmaNode Parent { get; set; }

        [Category("General")]
        [DisplayName("Id")]
        public string id { get; set; }

        [Category("General")]
        [DisplayName("Name")]
        public string name { get; set; }

        [Category("General")]
        [DisplayName("Type")]
        public string type { get; set; }

        [Category("General")]
        [DisplayName("Visible")]
        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool visible { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}:{1}:{2}]", type, id, name);
        }
    }

    #endregion

    public class FigmaPaint
    {
        [Category ("General")]
        [DisplayName ("ID")]
        public string ID { get; internal set; }

        [Category ("General")]
        [DisplayName ("Type")]
        public string type { get; set; }

        [Category ("General")]
        [DisplayName ("Visible")]
        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool visible { get; set; }

        [Category ("General")]
        [DisplayName ("Opacity")]
        [DefaultValue(1f)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public float opacity { get; set; }

        [Category ("General")]
        [DisplayName ("Color")]
        public Color color { get; set; }

        [Category ("General")]
        [DisplayName ("GradientHandlePositions")]
        public FigmaPoint[] gradientHandlePositions { get; set; }

        [Category ("General")]
        [DisplayName ("Gradient Stops")]
        public ColorStop[] gradientStops { get; set; }

        [Category ("General")]
        [DisplayName ("Scale Mode")]
        public string scaleMode { get; set; }

        [Category ("General")]
        [DisplayName ("ImageTransform")]
        public FigmaTransform imageTrandform { get; set; }

        [Category ("General")]
        [DisplayName ("Scaling Factor")]
        public float scalingFactor { get; set; }

        [Category ("General")]
        [DisplayName ("ImageRef")]
        public string imageRef { get; set; }
    }

    public class ColorStop
    {
    }

    public class FigmaConstraint
    {
        [Category ("General")]
        [DisplayName ("Type")]
        public string type { get; set; }

        [Category ("General")]
        [DisplayName ("Value")]
        public float value { get; set; }
    }

    public class FigmaExportSetting
    {
        [Category ("General")]
        [DisplayName ("Suffix")]
        public string suffix { get; set; }

        [Category ("General")]
        [DisplayName ("Format")]
        public string format { get; set; }

        [Category ("General")]
        [DisplayName ("Constraint")]
        public FigmaConstraint constraint { get; set; }
    }

    public class FigmaBackground
    {
        [Category ("General")]
        [DisplayName ("Type")]
        public string type { get; set; }

        [Category ("General")]
        [DisplayName ("BlendMode")]
        public string blendMode { get; set; }

        [Category ("General")]
        [DisplayName ("Color")]
        public Color color { get; set; }
    }

    public class FigmaPath
    {

    }

    public class FigmaTransform
    {
        //public int[][] data { get; set; }
    }

    public class FigmaEffect
    {
        [Category ("General")]
        [DisplayName ("Type")]
        public string type { get; set; }

        [Category ("General")]
        [DisplayName ("Visible")]
        [DefaultValue(true)]
        public bool visible { get; set; }

        [Category ("General")]
        [DisplayName ("Radius")]
        public float radius { get; set; }

        [Category ("General")]
        [DisplayName ("Color")]
        public Color color { get; set; }

        [Category ("General")]
        [DisplayName ("Blend Mode")]
        public FigmaBlendMode blendMode { get; set; }

        [Category ("General")]
        [DisplayName ("Offset")]
        public FigmaPoint offset { get; set; }
    }

	public class FigmaLayoutConstraint
    {
        [Category ("General")]
        [DisplayName ("Vertical")]
        public string vertical { get; set; }

        [Category ("General")]
        [DisplayName ("Horizontal")]
        public string horizontal { get; set; }

        public bool IsFlexibleHorizontal => (horizontal.Contains("LEFT") && horizontal.Contains("RIGHT")) || horizontal == "SCALE";
        public bool IsFlexibleVertical => (vertical.Contains("TOP") && vertical.Contains("BOTTOM")) || vertical == "SCALE";

        public bool HasAnyCenterConstraint => vertical == "CENTER" || vertical == "SCALE" || horizontal == "CENTER" || horizontal == "SCALE";
    }

    public class FigmaLayoutGrid
    {
        [Category ("General")]
        [DisplayName ("Pattern")]
        public string pattern { get; set; }

        [Category ("General")]
        [DisplayName ("Section Size")]
        public float sectionSize { get; set; }

        [Category ("General")]
        [DisplayName ("Visible")]
        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool visible { get; set; }

        [Category ("General")]
        [DisplayName ("Color")]
        public Color color { get; set; }

        [Category ("General")]
        [DisplayName ("Aligment")]
        public string aligment { get; set; }

        [Category ("General")]
        [DisplayName ("Gutter Size")]
        public int gutterSize { get; set; }

        [Category ("General")]
        [DisplayName ("Offset")]
        public int offset { get; set; }

        [Category ("General")]
        [DisplayName ("Count")]
        public int count { get; set; }
    }

    public class FigmaText : FigmaVector
    {
        public override bool HasImage()
        {
            return false;
        }

        [Category ("Text Data")]
        [DisplayName ("Characters")]
        [DefaultValue (true)]
        [JsonProperty (DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string characters { get; set; }

        [Category ("Text Data")]
        [DisplayName ("Style")]
        public FigmaTypeStyle style { get; set; }

        [Category ("Text Data")]
        [DisplayName ("Character Style Overrides")]
        public int[] characterStyleOverrides { get; set; }

        [Category ("Text Data")]
        [DisplayName ("Style Override Table")]
        public Dictionary<string, FigmaTypeStyle> styleOverrideTable { get; set; }
    }

    public class FigmaTypeStyle
    {
        [Category ("General")]
        [DisplayName ("Font Family")]
        public string fontFamily { get; set; }

        [Category ("General")]
        [DisplayName ("Font PostScriptName")]
        public string fontPostScriptName { get; set; }

        [Category ("General")]
        [DisplayName ("Font Weight")]
        public int fontWeight { get; set; }

        [Category ("General")]
        [DisplayName ("Font Size")]
        public int fontSize { get; set; }

        [Category ("General")]
        [DisplayName ("TextAlignHorizontal")]
        public string textAlignHorizontal { get; set; }

        [Category ("General")]
        [DisplayName ("TextAlignVertical")]
        public string textAlignVertical { get; set; }

        [Category ("General")]
        [DisplayName ("Letter Spacing")]
        public float letterSpacing { get; set; }

        [Category ("General")]
        [DisplayName ("Line Height Px")]
        public float lineHeightPx { get; set; }

        [Category ("General")]
        [DisplayName ("Line Height Percent")]
        public int lineHeightPercent { get; set; }

        [Category ("General")]
        [DisplayName ("Fills")]
        public FigmaPaint[] fills { get; set; }
    }
}