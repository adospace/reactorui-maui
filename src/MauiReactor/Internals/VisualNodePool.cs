namespace MauiReactor.Internals;

//internal class VisualNodePool
//{
//    private readonly Stack<VisualNode> _availableObjects = [];
//    private readonly HashSet<VisualNode> _pooledObjects = [];

//    public T GetObject<T>() where T : VisualNode, new()
//    {
//        if (_availableObjects.Count > 0)
//        {
//            //System.Diagnostics.Debug.WriteLine($"Reusing visual node: {typeof(T)}");
//            var reusedNode = (T)_availableObjects.Pop();
//            reusedNode.Reset();
//            return reusedNode;
//        }
//        else
//        {
//            //System.Diagnostics.Debug.WriteLine($"Generating new visual node: {typeof(T)}");
//            var newInstance = new T();
//            _pooledObjects.Add(newInstance);
//            return newInstance;
//        }
//    }

//    public void ReleaseObject(VisualNode obj)
//    {
//        if (_pooledObjects.Contains(obj))
//        {
//            //System.Diagnostics.Debug.WriteLine($"Releasing new visual node: {obj.GetType()}");
//            _availableObjects.Push(obj);
//        }
//    }
//}