using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    /// 

    /// CornerRadiusF is a value type used to describe the radius of a rectangle's corners (controlled independently).
    /// It contains four float structs each corresponding to a corner: TopLeft, TopRight, BottomLeft, BottomRight.
    /// The corner radii cannot be negative. 
    /// 

    public struct CornerRadiusF : IEquatable<CornerRadiusF>
    {
        //------------------------------------------------------------------- 
        //
        //  Constructors
        //
        //------------------------------------------------------------------- 
        #region Constructors
        /// 

        /// This constructor builds a CornerRadiusF with a specified uniform float radius value on every corner. 
        /// 

        /// The specified uniform radius. 
        public CornerRadiusF(float uniformRadius)
        {
            _topLeft = _topRight = _bottomLeft = _bottomRight = uniformRadius;
        }

        /// 

        /// This constructor builds a CornerRadiusF with the specified doubles on each corner. 
        /// 

        /// The thickness for the top left corner. 
        /// The thickness for the top right corner.
        /// The thickness for the bottom right corner.
        /// The thickness for the bottom left corner.
        public CornerRadiusF(float topLeft, float topRight, float bottomRight, float bottomLeft)
        {
            _topLeft = topLeft;
            _topRight = topRight;
            _bottomRight = bottomRight;
            _bottomLeft = bottomLeft;
        }

        #endregion Constructors

        //--------------------------------------------------------------------
        // 
        //  Public Methods 
        //
        //------------------------------------------------------------------- 
        #region Public Methods

        /// 

        /// This function compares to the provided object for type and value equality. 
        /// 

        /// Object to compare 
        /// True if object is a CornerRadiusF and all sides of it are equal to this CornerRadiusF'. 
        public override bool Equals(object? obj)
        {
            if (obj is CornerRadiusF otherObj)
            {
                return (this == otherObj);
            }
            return (false);
        }

        /// 

        /// Compares this instance of CornerRadiusF with another instance.
        /// 

        /// CornerRadiusF instance to compare.
        /// trueif this CornerRadiusF instance has the same value 
        /// and unit type as cornerRadius.
        public bool Equals(CornerRadiusF cornerRadius)
        {
            return (this == cornerRadius);
        }

        /// 

        /// This function returns a hash code.
        /// 

        /// Hash code
        public override int GetHashCode()
        {
            return _topLeft.GetHashCode() ^ _topRight.GetHashCode() ^ _bottomLeft.GetHashCode() ^ _bottomRight.GetHashCode();
        }

        /// 

        /// Converts this Thickness object to a string.
        /// 

        /// String conversion.
        public override string ToString()
        {
            return $"TopLeft={_topLeft} TopRight={_topRight} BottomRight={_bottomRight} BottomLeft={_bottomLeft}";
        }

        #endregion Public Methods

        //-------------------------------------------------------------------- 
        //
        //  Public Operators 
        // 
        //--------------------------------------------------------------------
        #region Public Operators 

        /// 

        /// Overloaded operator to compare two CornerRadiuses for equality.
        /// 

        /// First CornerRadiusF to compare
        /// Second CornerRadiusF to compare 
        /// True if all sides of the CornerRadiusF are equal, false otherwise 
        //  SEEALSO
        public static bool operator ==(CornerRadiusF cr1, CornerRadiusF cr2)
        {
            return ((cr1._topLeft == cr2._topLeft || (float.IsNaN(cr1._topLeft) && float.IsNaN(cr2._topLeft)))
                    && (cr1._topRight == cr2._topRight || (float.IsNaN(cr1._topRight) && float.IsNaN(cr2._topRight)))
                    && (cr1._bottomRight == cr2._bottomRight || (float.IsNaN(cr1._bottomRight) && float.IsNaN(cr2._bottomRight)))
                    && (cr1._bottomLeft == cr2._bottomLeft || (float.IsNaN(cr1._bottomLeft) && float.IsNaN(cr2._bottomLeft)))
                    );
        }

        /// 

        /// Overloaded operator to compare two CornerRadiuses for inequality.
        /// 

        /// First CornerRadiusF to compare
        /// Second CornerRadiusF to compare 
        /// False if all sides of the CornerRadiusF are equal, true otherwise
        //  SEEALSO 
        public static bool operator !=(CornerRadiusF cr1, CornerRadiusF cr2)
        {
            return (!(cr1 == cr2));
        }

        #endregion Public Operators


        //------------------------------------------------------------------- 
        // 
        //  Public Properties
        // 
        //--------------------------------------------------------------------

        #region Public Properties

        public float TopLeft
        {
            get { return _topLeft; }
            set { _topLeft = value; }
        }

        public float TopRight
        {
            get { return _topRight; }
            set { _topRight = value; }
        }


        public float BottomRight
        {
            get { return _bottomRight; }
            set { _bottomRight = value; }
        }


        public float BottomLeft
        {
            get { return _bottomLeft; }
            set { _bottomLeft = value; }
        }

        public bool UniformSize()
            => FloatUtils.AreClose(_topLeft, _topRight, _bottomRight, _bottomLeft);

        #endregion Public Properties 


        #region Internal Methods Properties

        internal bool IsValid(bool allowNegative, bool allowNaN, bool allowPositiveInfinity, bool allowNegativeInfinity)
        {
            if (!allowNegative)
            {
                if (_topLeft < 0f || _topRight < 0f || _bottomLeft < 0f || _bottomRight < 0f)
                {
                    return (false);
                }
            }

            if (!allowNaN)
            {
                if (float.IsNaN(_topLeft) || float.IsNaN(_topRight) || float.IsNaN(_bottomLeft) || float.IsNaN(_bottomRight))
                {
                    return (false);
                }
            }

            if (!allowPositiveInfinity)
            {
                if (float.IsPositiveInfinity(_topLeft) || float.IsPositiveInfinity(_topRight) || float.IsPositiveInfinity(_bottomLeft) || float.IsPositiveInfinity(_bottomRight))
                {
                    return (false);
                }
            }

            if (!allowNegativeInfinity)
            {
                if (float.IsNegativeInfinity(_topLeft) || float.IsNegativeInfinity(_topRight) || float.IsNegativeInfinity(_bottomLeft) || float.IsNegativeInfinity(_bottomRight))
                {
                    return (false);
                }
            }

            return (true);
        }

        public bool IsZero
        {
            get
            {
                return (FloatUtils.IsZero(_topLeft)
                        && FloatUtils.IsZero(_topRight)
                        && FloatUtils.IsZero(_bottomRight)
                        && FloatUtils.IsZero(_bottomLeft)
                        );
            }
        }

        #endregion Internal Methods Properties

        //------------------------------------------------------------------- 
        //
        //  Private Fields 
        // 
        //--------------------------------------------------------------------

        #region Private Fields
        private float _topLeft;
        private float _topRight;
        private float _bottomLeft;
        private float _bottomRight;
        #endregion
    }
}


