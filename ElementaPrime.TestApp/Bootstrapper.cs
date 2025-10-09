using Caliburn.Micro;
using ElementaPrime.Core;
using ElementaPrime.Core.Graphs.Helpers;
using ElementaPrime.Core.Graphs.Management;
using ElementaPrime.Core.Graphs.Services;
using ElementaPrime.Providers.Standard;
using ElementaPrime.TestApp.UIComponents.Helpers;
using ElementaPrime.TestApp.ViewModels;
using System.Windows;

namespace ElementaPrime.TestApp;

public class Bootstrapper : BootStrapperCore
{
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

        RegisterStandardProviders();
        RegisterExternalProviders();


    }

    private void RegisterExternalProviders()
    {
        _container.Singleton<VaultService>();
        _container.GetInstance<IOperatorFactory>().RegisterProviderMethods(_container.GetInstance<VaultService>());
    }

    private void RegisterStandardProviders()
    {
        _container.Singleton<BaseMath>();
        _container.Singleton<VectorMath>();

        _container.GetInstance<IOperatorFactory>().RegisterProviderMethods(_container.GetInstance<BaseMath>());
        _container.GetInstance<IOperatorFactory>().RegisterProviderMethods(_container.GetInstance<VectorMath>());
    }

}