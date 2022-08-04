namespace MauiReactor
{
    public interface IHostElement
    {
        IHostElement Run();

        void Stop();

        //Microsoft.Maui.Controls.Page? ContainerPage { get; }

        void RequestAnimationFrame(VisualNode visualNode);
    }
}
