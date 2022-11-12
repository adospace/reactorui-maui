using System;
using System.Collections;
using System.Collections.Generic;
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

    public class ObservableListItemsSource : ObservableItemsSource, IList
    {
        private readonly IList _itemsSource;

        public ObservableListItemsSource(IList itemsSource, object? itemTemplate)
            : base(itemsSource, itemTemplate)
        {
            _itemsSource = itemsSource;
        }

        public static ObservableListItemsSource Create(IList itemsSource, object? itemTemplate = null)
        {
            return new ObservableListItemsSource(itemsSource, itemTemplate);
        }

        public object? this[int index]{ get=>_itemsSource[index]; set => _itemsSource[index] = value; }

        public int Count => _itemsSource.Count;

        public bool IsFixedSize => _itemsSource.IsFixedSize;

        public bool IsReadOnly => _itemsSource.IsReadOnly;

        public bool IsSynchronized => _itemsSource.IsSynchronized;

        public object SyncRoot => _itemsSource.SyncRoot;

        public int Add(object? value)
        {
            return _itemsSource.Add(value);
        }

        public void Clear()
        {
            _itemsSource.Clear();
        }

        public bool Contains(object? value)
        {
            return _itemsSource.Contains(value);
        }

        public void CopyTo(Array array, int index)
        {
            _itemsSource.CopyTo(array, index);
        }

        public int IndexOf(object? value)
        {
            return _itemsSource.IndexOf(value);
        }

        public void Insert(int index, object? value)
        {
            _itemsSource.Insert(index, value);
        }

        public void Remove(object? value)
        {
            _itemsSource.Remove(value);
        }

        public void RemoveAt(int index)
        {
            _itemsSource.RemoveAt(index);
        }
    }
}
