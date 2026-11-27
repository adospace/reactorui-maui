using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor.Internals
{
    internal class ComponentNavigationStack
    {
        private INavigation _nativeNavigation;

        // A concrete List (not an array behind IEnumerable<T>): on Windows, CsWinRT's AOT analyzer flags
        // arrays converted to generic collection interfaces (CsWinRT1030).
        private List<Microsoft.Maui.Controls.Page> _pages;

        private readonly List<WeakReference<IComponentNavigationStackEventHandler>> _eventHandlers = [];

        public ComponentNavigationStack(INavigation nativeNavigation)
        {
            _nativeNavigation = nativeNavigation;
            _pages = new List<Microsoft.Maui.Controls.Page>(nativeNavigation.ModalStack.Concat(nativeNavigation.NavigationStack).Distinct());
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
            var currentPages = new List<Microsoft.Maui.Controls.Page>(_nativeNavigation.ModalStack.Concat(_nativeNavigation.NavigationStack));
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
            for (var i = _pages.Count - 1; i >= 0; i--)
            {
                var page = _pages[i];
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
