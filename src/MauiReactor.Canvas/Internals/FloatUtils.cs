using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals
{
    internal static class FloatUtils
    {
        private const float MIN_FLT = 0.00001f;

        public static bool AreClose(float f1, float f2)
            => Math.Abs(f1 - f2) < MIN_FLT;

        public static bool AreClose(float f1, float f2, float f3)
             => Math.Abs(f1 - f2) < MIN_FLT && Math.Abs(f1 - f3) < MIN_FLT;

        public static bool AreClose(float f1, float f2, float f3, float f4)
             => Math.Abs(f1 - f2) < MIN_FLT && Math.Abs(f1 - f3) < MIN_FLT && Math.Abs(f1 - f4) < MIN_FLT;

        public static bool IsZero(float f)
            => Math.Abs(f) < MIN_FLT;
    }
}
