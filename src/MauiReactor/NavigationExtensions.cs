using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public static class NavigationExtensions
    {
        public static async Task<Microsoft.Maui.Controls.Page> PushAsync<T>(this INavigation navigation) where T : Component, new()
        {
            var page = PageHost<T>.CreatePage(navigation);
            await navigation.PushAsync(page);
            return page;
        }

        public static async Task<Microsoft.Maui.Controls.Page> PushAsync<T, P>(this INavigation navigation, Action<P> propsInitializer) where T : Component, new() where P : class, new()
        {
            var page = PageHost<T, P>.CreatePage(navigation, propsInitializer);
            await navigation.PushAsync(page);
            return page;
        }

        //public static async Task<Microsoft.Maui.Controls.Page> PushAsync<T, P>(this INavigation navigation, P props) where T : Component, new() where P : class, new()
        //{
        //    var page = PageHost<T, P>.CreatePage();
        //    await navigation.PushAsync(page);
        //    return page;
        //}

        public static async Task<Microsoft.Maui.Controls.Page> PushAsync<T>(this INavigation navigation, bool animated) where T : Component, new()
        {
            var page = PageHost<T>.CreatePage(navigation);
            await navigation.PushAsync(page, animated);
            return page;
        }

        public static async Task<Microsoft.Maui.Controls.Page> PushAsync<T, P>(this INavigation navigation, bool animated, Action<P> propsInitializer) where T : Component, new() where P : class, new()
        {
            var page = PageHost<T, P>.CreatePage(navigation, propsInitializer);
            await navigation.PushAsync(page, animated);
            return page;
        }

        public static async Task<Microsoft.Maui.Controls.Page> PushModalAsync<T>(this INavigation navigation) where T : Component, new()
        {
            var page = PageHost<T>.CreatePage(navigation);
            await navigation.PushModalAsync(page);
            return page;
        }

        public static async Task<Microsoft.Maui.Controls.Page> PushModalAsync<T, P>(this INavigation navigation, Action<P> propsInitializer) where T : Component, new() where P : class, new()
        {
            var page = PageHost<T, P>.CreatePage(navigation, propsInitializer);
            await navigation.PushModalAsync(page);
            return page;
        }

        public static async Task<Microsoft.Maui.Controls.Page> PushModalAsync<T>(this INavigation navigation, bool animated) where T : Component, new()
        {
            var page = PageHost<T>.CreatePage(navigation);
            await navigation.PushModalAsync(page, animated);
            return page;
        }

        public static async Task<Microsoft.Maui.Controls.Page> PushModalAsync<T, P>(this INavigation navigation, bool animated, Action<P> propsInitializer) where T : Component, new() where P : class, new()
        {
            var page = PageHost<T, P>.CreatePage(navigation, propsInitializer);
            await navigation.PushModalAsync(page, animated);
            return page;
        }

    }
}
