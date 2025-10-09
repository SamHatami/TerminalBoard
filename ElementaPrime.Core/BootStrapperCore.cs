using Caliburn.Micro;
using ElementaPrime.Core.Graphs.Management;
using ElementaPrime.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace ElementaPrime.Core
{
    //Core bootstrapper to allow registration of internal core services
    
    public class BootStrapperCore : BootstrapperBase
    {
        protected readonly SimpleContainer _container = new();
        private ILogger _logger;

        public BootStrapperCore()
        {
            Initialize();
            LogManager.GetLog = type => new DebugLog(type);
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            _container.Singleton<IConduitManager,ConduitManager>();
            _container.Singleton<IOperatorFactory,OperatorFactory>();

            foreach (var assembly in SelectAssemblies())
                assembly.GetTypes()
                    .Where(type => type.IsClass)
                    .Where(type => type.Name.EndsWith("ViewModel"))
                    .ToList()
                    .ForEach(viewModelType => _container.RegisterPerRequest(
                        viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        public IWindowManager GetWindowManager()
        {
            return (IWindowManager)_container.GetInstance(typeof(IWindowManager), null);
        }
    }
}