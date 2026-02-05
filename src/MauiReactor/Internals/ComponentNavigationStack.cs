using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor.Internals
{
    internal class ComponentNavigationStack
    {
        private INavigation _nativeNavigation;

        private IEnumerable<Microsoft.Maui.Controls.Page> _pages;

        private readonly List<WeakReference<IComponentNavigationStackEventHandler>> _eventHandlers = [];

        public ComponentNavigationStack(INavigation nativeNavigation)
        {
            _nativeNavigation = nativeNavigation;
            _pages = [.. nativeNavigation.ModalStack.Concat(nativeNavigation.NavigationStack).Distinct()];
        }

        public void RegisterEventHandler(IComponentNavigationStackEventHandler eventHandler)
        {
            _eventHandlers.Add(new WeakReference<IComponentNavigationStackEventHandler>(eventHandler));
        }

        public void UnregisterEventHandler(IComponentNavigationStackEventHandler eventHandler)
        {
            _eventHandlers.RemoveAll(wr =>
            {
                if (wr.TryGetTarget(out var target))
                {
                    return target == eventHandler;
                }
                return true;
            });
        }

        public void Refresh()
        {
            var currentPages = new List<Microsoft.Maui.Controls.Page>([.. _nativeNavigation.ModalStack, .. _nativeNavigation.NavigationStack]);
            // Check for pushed pages
            foreach (var page in currentPages)
            {
                if (!_pages.Contains(page))
                {
                    // New page pushed
                    foreach (var weakRef in _eventHandlers)
                    {
                        if (weakRef.TryGetTarget(out var handler))
                        {
                            handler.OnPagePushed(page);
                        }
                    }
                }
            }
            // Check for popped pages
            foreach (var page in _pages.Reverse())
            {
                if (!currentPages.Contains(page))
                {
                    // Page popped
                    foreach (var weakRef in _eventHandlers)
                    {
                        if (weakRef.TryGetTarget(out var handler))
                        {
                            handler.OnPagePopped(page);
                        }
                    }
                }
            }
            _pages = currentPages;
        }
    }

    internal interface IComponentNavigationStackEventHandler
    {
        void OnPagePushed(Microsoft.Maui.Controls.Page page);
        void OnPagePopped(Microsoft.Maui.Controls.Page page);
    }
}
