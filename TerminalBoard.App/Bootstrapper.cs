using Caliburn.Micro;
using System.Windows;
using TerminalBoard.App.UIComponents.Helpers;
using TerminalBoard.App.ViewModels;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Services;
using TerminalBoard.Core.Terminals;
using TerminalBoard.Math;
using TerminalBoard.Math.Operators;

namespace TerminalBoard.App;

public class Bootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container = new();

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
        _container.Singleton<TerminalFactory>();
        TerminalService terminalService = new TerminalService(new TerminalFactory());
        _container.Instance(terminalService);
        _container.Singleton<BoardViewModel>();


        RegisterFunctions();
        RegisterTerminals();

        foreach (var assembly in SelectAssemblies())
            assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
    }

    private void RegisterTerminals()
    {
        TerminalFactory.RegisterTerminal(TerminalType.Multiplication, () => new EvaluationTerminal(FunctionFactory.GetFunction(FunctionNames.Multiplication) as IEvaluationFunction));
        TerminalFactory.RegisterTerminal(TerminalType.Division, () => new EvaluationTerminal(FunctionFactory.GetFunction(FunctionNames.Division) as IEvaluationFunction));
        TerminalFactory.RegisterTerminal(TerminalType.Subtraction, () => new EvaluationTerminal(FunctionFactory.GetFunction(FunctionNames.Subtraction) as IEvaluationFunction));
        TerminalFactory.RegisterTerminal(TerminalType.Addition, () => new EvaluationTerminal(FunctionFactory.GetFunction(FunctionNames.Addition) as IEvaluationFunction));

        TerminalFactory.RegisterTerminal(TerminalType.FloatValue, () => new ValueTerminal<float>());
        TerminalFactory.RegisterTerminal(TerminalType.IntegerValue, () => new ValueTerminal<int>());

    }

    private void RegisterFunctions()
    {
        FunctionFactory.RegisterFunction<TypedValueOutputFunction<float>>(FunctionNames.Float); 
        FunctionFactory.RegisterFunction<TypedValueOutputFunction<int>>(FunctionNames.Integer);
        FunctionFactory.RegisterFunction(FunctionNames.Multiplication, () => new Multiplication());
        FunctionFactory.RegisterFunction(FunctionNames.Division, () => new Division());
        FunctionFactory.RegisterFunction(FunctionNames.Subtraction, () => new Subtraction());
        FunctionFactory.RegisterFunction(FunctionNames.Addition, () => new Addition());
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