using Caliburn.Micro;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.Logging;
using TerminalBoard.App.UIComponents.Helpers;
using TerminalBoard.App.ViewModels;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Services;
using TerminalBoard.Core.Terminals;
using TerminalBoard.Providers.Standard;

namespace TerminalBoard.App;

public class Bootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container = new();
    private ILogger _logger;

    public Bootstrapper()
    {
        Initialize();
        LogManager.GetLog = type => new DebugLog(type);

    }

    protected override void OnStartup(object sender, StartupEventArgs e)
    {
        GetWindowManager().ShowDialogAsync(_container.GetInstance<BoardViewModel>());
    }

    protected override void Configure()
    {

        _container.Instance(_container);

        _container.Singleton<IWindowManager, WindowManager>();
        _container.Singleton<IEventAggregator, EventAggregator>();

        BehaviorHelper.EventsAggregator = _container.GetInstance<IEventAggregator>();
        TerminalHelper.EventsAggregator = _container.GetInstance<IEventAggregator>();

        _container.Singleton<WireService>();
        _container.Singleton<TerminalService>();
        _container.Singleton<BoardViewModel>();

        RegisterStandardProviders();
        RegisterExternalProviders();

        foreach (var assembly in SelectAssemblies())
            assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
    }

    private void RegisterExternalProviders()
    {
        _container.Singleton<IProvider, VaultService>();
        _container.GetInstance<TerminalService>().RegisterProviderMethods(_container.GetInstance<VaultService>());
    }

    private void RegisterStandardProviders()
    {
        _container.Singleton<BaseMath>();
        _container.Singleton<VectorMath>();

        _container.GetInstance<TerminalService>().RegisterProviderMethods(_container.GetInstance<BaseMath>());
        _container.GetInstance<TerminalService>().RegisterProviderMethods(_container.GetInstance<VectorMath>());

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