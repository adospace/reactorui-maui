using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.Internals;

namespace MauiReactor
{
    public class TemplateHost<T> : VisualNode where T : BindableObject
    {
        public TemplateHost(VisualNode root)
            : this(root, SetupServiceProvider())
        {
        }

        public TemplateHost(VisualNode root, Action<ServiceCollection> serviceCollectionSetupAction)
            : this(root, SetupServiceProvider(serviceCollectionSetupAction))
        {
        }

        public TemplateHost(VisualNode root, IServiceProvider serviceProvider)
        {
            ServiceCollectionProvider.ServiceProvider ??= serviceProvider;

            _root = root;

            Layout();
        }

        private static IServiceProvider SetupServiceProvider(Action<ServiceCollection>? serviceCollectionSetupAction = null)
        {
            if (ServiceCollectionProvider.ServiceProvider != null)
            {
                return ServiceCollectionProvider.ServiceProvider;
            }

            var services = new ServiceCollection();
            serviceCollectionSetupAction?.Invoke(services);
            return services.BuildServiceProvider();
        }

        private readonly VisualNode _root;

        public VisualNode Root
        {
            get => _root;
        }

        public T? NativeElement { get; private set; }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is T nativeElement)
                NativeElement = nativeElement;
            else
            {
                throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Root;
        }

        protected internal override void OnLayoutCycleRequested()
        {
            Layout();
            base.OnLayoutCycleRequested();
        }
    }
}
