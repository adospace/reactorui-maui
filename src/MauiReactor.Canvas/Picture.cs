using MauiReactor.Internals;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface IPicture : ICanvasVisualElement
    {
        PropertyValue<Microsoft.Maui.Graphics.IImage?>? Source { get; set; }
        PropertyValue<Aspect>? Aspect { get; set; }
    }

    public partial class Picture<T> : CanvasVisualElement<T>, IPicture where T : Internals.Picture, new()
    {
        public Picture()
        {

        }

        public Picture(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Graphics.IImage?>? IPicture.Source { get; set; }
        PropertyValue<Aspect>? IPicture.Aspect { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIPicture = (IPicture)this;

            SetPropertyValue(NativeControl, Internals.Picture.SourceProperty, thisAsIPicture.Source);
            SetPropertyValue(NativeControl, Internals.Picture.AspectProperty, thisAsIPicture.Aspect);

            base.OnUpdate();
        }
    }

    public partial class Picture : Picture<Internals.Picture>
    {
        public Picture()
        {

        }

        public Picture(string imageSource)
        {
            this.Source(imageSource, true, Assembly.GetCallingAssembly());
        }

        public Picture(Action<Internals.Picture?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class PictureExtensions
    {
        private static readonly Dictionary<string, Microsoft.Maui.Graphics.IImage> _imageCache = new();

        public static T Source<T>(this T node, Microsoft.Maui.Graphics.IImage? value) where T : IPicture
        {
            node.Source = new PropertyValue<Microsoft.Maui.Graphics.IImage?>(value);
            return node;
        }

        public static T Source<T>(this T node, Func<Microsoft.Maui.Graphics.IImage?> valueFunc) where T : IPicture
        {
            node.Source = new PropertyValue<Microsoft.Maui.Graphics.IImage?>(valueFunc);
            return node;
        }

        public static T Source<T>(this T node, string? imageSource, bool cacheImage = true, Assembly? resourceAssembly = null) where T : IPicture
        {
            resourceAssembly ??= Assembly.GetCallingAssembly();
            node.Source = new PropertyValue<Microsoft.Maui.Graphics.IImage?>(LoadImage(imageSource, cacheImage, resourceAssembly));
            return node;
        }

        public static T Source<T>(this T node, Func<string?> valueFunc, bool cacheImage = true, Assembly? resourceAssembly = null) where T : IPicture
        {
            resourceAssembly ??= Assembly.GetCallingAssembly();
            node.Source = new PropertyValue<Microsoft.Maui.Graphics.IImage?>(() => LoadImage(valueFunc.Invoke(), cacheImage, resourceAssembly));
            return node;
        }

        private static Microsoft.Maui.Graphics.IImage? LoadImage(string? imageSource, bool cacheImage, Assembly resourceAssembly)
        {
            if (imageSource == null)
            {
                return null;
            }
            else
            {
                if (_imageCache.TryGetValue(imageSource, out var cachedImage))
                {
                    return cachedImage;
                }
                else
                {
                    Microsoft.Maui.Graphics.IImage? image = null;
                    var imageResourceStream = resourceAssembly.GetManifestResourceStream(imageSource);

                    if (imageResourceStream == null)
                    {
                        throw new InvalidOperationException($"Unable to load resource: '{imageSource}'. Available resources: {string.Join(",", resourceAssembly.GetManifestResourceNames())}");
                    }

                    using (Stream stream = imageResourceStream)
                    {
#if !WINDOWS
                        image = Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(stream);
#endif
                    }

                    if (cacheImage && image != null)
                    {
                        _imageCache.Add(imageSource, image);
                    }

                    return image;
                }
            }
        }


        public static T Aspect<T>(this T node, Aspect value) where T : IPicture
        {
            node.Aspect = new PropertyValue<Aspect>(value);
            return node;
        }

        public static T Aspect<T>(this T node, Func<Aspect> valueFunc) where T : IPicture
        {
            node.Aspect = new PropertyValue<Aspect>(valueFunc);
            return node;
        }
    }
}
