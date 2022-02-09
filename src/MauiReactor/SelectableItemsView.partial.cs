namespace MauiReactor
{
    public static partial class SelectableItemsViewExtensions
    {
        public static T OnSelected<T, I>(this T collectionView, Action<I?> action) where T : ISelectableItemsView
        {
            collectionView.SelectionChangedActionWithArgs = (sender, args) => action(args.CurrentSelection.Count == 0 ? default : (I)args.CurrentSelection[0]);
            return collectionView;
        }

        public static T OnSelectedMany<T, I>(this T collectionView, Action<I[]> action) where T : ISelectableItemsView
        {
            collectionView.SelectionChangedActionWithArgs = (sender, args) => action(args.CurrentSelection.Cast<I>().ToArray());
            return collectionView;
        }
    }

}
