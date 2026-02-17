using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Animations;

public static class ExtendedEasing
{
    public static readonly Easing Linear = new(EasingFunctions.Linear);
    public static readonly Easing InQuad = new(EasingFunctions.InQuad);
    public static readonly Easing OutQuad = new(EasingFunctions.OutQuad);
    public static readonly Easing InOutQuad = new(EasingFunctions.InOutQuad);
    public static readonly Easing InCubic = new(EasingFunctions.InCubic);
    public static readonly Easing OutCubic = new(EasingFunctions.OutCubic);
    public static readonly Easing InOutCubic = new(EasingFunctions.InOutCubic);
    public static readonly Easing InQuart = new(EasingFunctions.InQuart);
    public static readonly Easing OutQuart = new(EasingFunctions.OutQuart);
    public static readonly Easing InOutQuart = new(EasingFunctions.InOutQuart);
    public static readonly Easing InQuint = new(EasingFunctions.InQuint);
    public static readonly Easing OutQuint = new(EasingFunctions.OutQuint);
    public static readonly Easing InOutQuint = new(EasingFunctions.InOutQuint);
    public static readonly Easing InSine = new(EasingFunctions.InSine);
    public static readonly Easing OutSine = new(EasingFunctions.OutSine);
    public static readonly Easing InOutSine = new(EasingFunctions.InOutSine);
    public static readonly Easing InExpo = new(EasingFunctions.InExpo);
    public static readonly Easing OutExpo = new(EasingFunctions.OutExpo);
    public static readonly Easing InOutExpo = new(EasingFunctions.InOutExpo);
    public static readonly Easing InCirc = new(EasingFunctions.InCirc);
    public static readonly Easing OutCirc = new(EasingFunctions.OutCirc);
    public static readonly Easing InOutCirc = new(EasingFunctions.InOutCirc);
    public static readonly Easing InElastic = new(EasingFunctions.InElastic);
    public static readonly Easing OutElastic = new(EasingFunctions.OutElastic);
    public static readonly Easing InOutElastic = new(EasingFunctions.InOutElastic);
    public static readonly Easing InBack = new(EasingFunctions.InBack);
    public static readonly Easing OutBack = new(EasingFunctions.OutBack);
    public static readonly Easing InOutBack = new(EasingFunctions.InOutBack);
    public static readonly Easing InBounce = new(EasingFunctions.InBounce);
    public static readonly Easing OutBounce = new(EasingFunctions.OutBounce);
    public static readonly Easing InOutBounce = new(EasingFunctions.InOutBounce);
}

//https://gist.github.com/Kryzarel/bba64622057f21a1d6d44879f9cd7bd4
internal static class EasingFunctions
{
    public static double Linear(double t) => t;

    public static double InQuad(double t) => t * t;
    public static double OutQuad(double t) => 1 - InQuad(1 - t);
    public static double InOutQuad(double t)
    {
        if (t < 0.5) return InQuad(t * 2) / 2;
        return 1 - InQuad((1 - t) * 2) / 2;
    }

    public static double InCubic(double t) => t * t * t;
    public static double OutCubic(double t) => 1 - InCubic(1 - t);
    public static double InOutCubic(double t)
    {
        if (t < 0.5) return InCubic(t * 2) / 2;
        return 1 - InCubic((1 - t) * 2) / 2;
    }

    public static double InQuart(double t) => t * t * t * t;
    public static double OutQuart(double t) => 1 - InQuart(1 - t);
    public static double InOutQuart(double t)
    {
        if (t < 0.5) return InQuart(t * 2) / 2;
        return 1 - InQuart((1 - t) * 2) / 2;
    }

    public static double InQuint(double t) => t * t * t * t * t;
    public static double OutQuint(double t) => 1 - InQuint(1 - t);
    public static double InOutQuint(double t)
    {
        if (t < 0.5) return InQuint(t * 2) / 2;
        return 1 - InQuint((1 - t) * 2) / 2;
    }

    public static double InSine(double t) => (double)-Math.Cos(t * Math.PI / 2);
    public static double OutSine(double t) => (double)Math.Sin(t * Math.PI / 2);
    public static double InOutSine(double t) => (double)(Math.Cos(t * Math.PI) - 1) / -2;

    public static double InExpo(double t) => (double)Math.Pow(2, 10 * (t - 1));
    public static double OutExpo(double t) => 1 - InExpo(1 - t);
    public static double InOutExpo(double t)
    {
        if (t < 0.5) return InExpo(t * 2) / 2;
        return 1 - InExpo((1 - t) * 2) / 2;
    }

    public static double InCirc(double t) => -((double)Math.Sqrt(1 - t * t) - 1);
    public static double OutCirc(double t) => 1 - InCirc(1 - t);
    public static double InOutCirc(double t)
    {
        if (t < 0.5) return InCirc(t * 2) / 2;
        return 1 - InCirc((1 - t) * 2) / 2;
    }

    public static double InElastic(double t) => 1 - OutElastic(1 - t);
    public static double OutElastic(double t)
    {
        double p = 0.3f;
        return (double)Math.Pow(2, -10 * t) * (double)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
    }
    public static double InOutElastic(double t)
    {
        if (t < 0.5) return InElastic(t * 2) / 2;
        return 1 - InElastic((1 - t) * 2) / 2;
    }

    public static double InBack(double t)
    {
        double s = 1.70158f;
        return t * t * ((s + 1) * t - s);
    }
    public static double OutBack(double t) => 1 - InBack(1 - t);
    public static double InOutBack(double t)
    {
        if (t < 0.5) return InBack(t * 2) / 2;
        return 1 - InBack((1 - t) * 2) / 2;
    }

    public static double InBounce(double t) => 1 - OutBounce(1 - t);
    public static double OutBounce(double t)
    {
        double div = 2.75f;
        double mult = 7.5625f;

        if (t < 1 / div)
        {
            return mult * t * t;
        }
        else if (t < 2 / div)
        {
            t -= 1.5f / div;
            return mult * t * t + 0.75f;
        }
        else if (t < 2.5 / div)
        {
            t -= 2.25f / div;
            return mult * t * t + 0.9375f;
        }
        else
        {
            t -= 2.625f / div;
            return mult * t * t + 0.984375f;
        }
    }
    public static double InOutBounce(double t)
    {
        if (t < 0.5) return InBounce(t * 2) / 2;
        return 1 - InBounce((1 - t) * 2) / 2;
    }
}