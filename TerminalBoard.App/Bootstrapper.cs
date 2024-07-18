using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using TerminalBoard.App.UIComponents.Helpers;
using TerminalBoard.App.ViewModels;

namespace TerminalBoard.App
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
            LogManager.GetLog = type => new DebugLog(type);
        }


        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            GetWindowManager().ShowDialogAsync(_container.GetInstance<MainViewModel>());
        }

        protected override void Configure()
        {
            _container.Instance(_container);
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<MainViewModel>();

            BehaviorHelper.EventsAggregator = _container.GetInstance<IEventAggregator>();


            foreach (var assembly in SelectAssemblies())
            {
                assembly.GetTypes()
                    .Where(type => type.IsClass)
                    .Where(type => type.Name.EndsWith("ViewModel"))
                    .ToList()
                    .ForEach(viewModelType => _container.RegisterPerRequest(
                        viewModelType, viewModelType.ToString(), viewModelType));
            }
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
