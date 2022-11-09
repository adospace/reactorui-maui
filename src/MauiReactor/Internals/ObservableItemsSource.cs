using System.Collections;
using System.Collections.Specialized;

namespace MauiReactor.Internals
{
    public class ObservableItemsSource<T> : IEnumerable<T>, IEnumerable, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        private readonly bool _collectionWithNotifyEvent;

        public ObservableItemsSource(IEnumerable<T> itemsSource)
        {
            ItemsSource = itemsSource;
            if (ItemsSource is INotifyCollectionChanged notifyCollectionChanged)
            {
                _collectionWithNotifyEvent = true;
                notifyCollectionChanged.CollectionChanged += NotifyCollectionChanged_CollectionChanged;
            }
        }

        private void NotifyCollectionChanged_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ItemsSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static ObservableItemsSource<T> Create(IEnumerable<T> itemsSource)
        {
            if (itemsSource is IReadOnlyList<T> readOnlyList)
            {
                return new ObservableListItemsSource<T>(readOnlyList);
            }

            return new ObservableItemsSource<T>(itemsSource);
        }

        public void NotifyCollectionChanged()
        {
            if (!_collectionWithNotifyEvent)
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public IEnumerable<T> ItemsSource { get; }
    }

    public class ObservableListItemsSource<T> : ObservableItemsSource<T>, IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _itemsSource;

        public ObservableListItemsSource(IReadOnlyList<T> itemsSource) 
            : base(itemsSource)
        {
            _itemsSource = itemsSource;
        }

        public T this[int index] => _itemsSource[index];

        public int Count => _itemsSource.Count;
    }

    public class ObservableItemsSource : IEnumerable, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        private readonly bool _collectionWithNotifyEvent;

        public ObservableItemsSource(IEnumerable itemsSource, object? itemTemplate)
        {
            ItemsSource = itemsSource;
            ItemTemplate = itemTemplate;

            if (ItemsSource is INotifyCollectionChanged notifyCollectionChanged)
            {
                _collectionWithNotifyEvent = true;
                notifyCollectionChanged.CollectionChanged += NotifyCollectionChanged_CollectionChanged;
            }
        }

        public IEnumerable ItemsSource { get; }

        public object? ItemTemplate { get; }

        private void NotifyCollectionChanged_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public IEnumerator GetEnumerator()
        {
            return ItemsSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static ObservableItemsSource Create(IEnumerable itemsSource, object? itemTemplate = null)
        {
            return new ObservableItemsSource(itemsSource, itemTemplate);
        }

        public void NotifyCollectionChanged()
        {
            if (!_collectionWithNotifyEvent)
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }


}
