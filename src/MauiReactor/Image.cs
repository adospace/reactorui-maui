using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IImage
    {
        Microsoft.Maui.Controls.ImageSource Source { get; set; }
        Microsoft.Maui.Aspect Aspect { get; set; }
        bool IsOpaque { get; set; }
        bool IsAnimationPlaying { get; set; }


    }
    public partial class Image<T> : View<T>, IImage where T : Microsoft.Maui.Controls.Image, new()
    {
        public Image()
        {

        }

        public Image(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.ImageSource IImage.Source { get; set; } = (Microsoft.Maui.Controls.ImageSource)Microsoft.Maui.Controls.Image.SourceProperty.DefaultValue;
        Microsoft.Maui.Aspect IImage.Aspect { get; set; } = (Microsoft.Maui.Aspect)Microsoft.Maui.Controls.Image.AspectProperty.DefaultValue;
        bool IImage.IsOpaque { get; set; } = (bool)Microsoft.Maui.Controls.Image.IsOpaqueProperty.DefaultValue;
        bool IImage.IsAnimationPlaying { get; set; } = (bool)Microsoft.Maui.Controls.Image.IsAnimationPlayingProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIImage = (IImage)this;
            if (NativeControl.Source != thisAsIImage.Source) NativeControl.Source = thisAsIImage.Source;
            if (NativeControl.Aspect != thisAsIImage.Aspect) NativeControl.Aspect = thisAsIImage.Aspect;
            if (NativeControl.IsOpaque != thisAsIImage.IsOpaque) NativeControl.IsOpaque = thisAsIImage.IsOpaque;
            if (NativeControl.IsAnimationPlaying != thisAsIImage.IsAnimationPlaying) NativeControl.IsAnimationPlaying = thisAsIImage.IsAnimationPlaying;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class Image : Image<Microsoft.Maui.Controls.Image>
    {
        public Image()
        {

        }

        public Image(Action<Microsoft.Maui.Controls.Image?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ImageExtensions
    {
        public static T Source<T>(this T image, Microsoft.Maui.Controls.ImageSource source) where T : IImage
        {
            image.Source = source;
            return image;
        }
        public static T Source<T>(this T image, string file) where T : IImage
        {
            image.Source = Microsoft.Maui.Controls.ImageSource.FromFile(file);
            return image;
        }
        public static T Source<T>(this T image, string fileAndroid, string fileiOS) where T : IImage
        {
            image.Source = Device.RuntimePlatform == Device.Android ? Microsoft.Maui.Controls.ImageSource.FromFile(fileAndroid) : Microsoft.Maui.Controls.ImageSource.FromFile(fileiOS);
            return image;
        }
        public static T Source<T>(this T image, string resourceName, Assembly sourceAssembly) where T : IImage
        {
            image.Source = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
            return image;
        }
        public static T Source<T>(this T image, Uri imageUri) where T : IImage
        {
            image.Source = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
            return image;
        }
        public static T Source<T>(this T image, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IImage
        {
            image.Source = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return image;
        }
        public static T Source<T>(this T image, Func<Stream> imageStream) where T : IImage
        {
            image.Source = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
            return image;
        }

        public static T Aspect<T>(this T image, Microsoft.Maui.Aspect aspect) where T : IImage
        {
            image.Aspect = aspect;
            return image;
        }

        public static T IsOpaque<T>(this T image, bool isOpaque) where T : IImage
        {
            image.IsOpaque = isOpaque;
            return image;
        }

        public static T IsAnimationPlaying<T>(this T image, bool isAnimationPlaying) where T : IImage
        {
            image.IsAnimationPlaying = isAnimationPlaying;
            return image;
        }


    }
}
