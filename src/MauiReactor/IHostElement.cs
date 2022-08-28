namespace MauiReactor
{
    public interface IHostElement
    {
        IHostElement Run();

        void Stop();

        void RequestAnimationFrame(VisualNode visualNode);
    }
}
