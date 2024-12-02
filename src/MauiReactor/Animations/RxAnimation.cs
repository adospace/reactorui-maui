namespace MauiReactor.Animations
{
    public abstract class RxAnimation
    {
        public long StartTime { get; protected set; }

        protected RxAnimation()
        {
        }

        public abstract bool IsCompleted();

        internal void Start()
        {
            StartTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        internal void MigrateFrom(RxAnimation previousAnimation)
        {
            OnMigrateFrom(previousAnimation);
        }

        protected virtual void OnMigrateFrom(RxAnimation previousAnimation)
        {
        }

        internal abstract object GetCurrentValue();
    }
}