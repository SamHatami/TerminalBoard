using System.Reflection;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core;

public class MethodClassTerminal<T> : ClassTerminalBase<T> where T : class
{
    private readonly T _provider;
    private readonly MethodInfo _methodInfo;
    private readonly string _providerCategory;

    private MethodClassTerminal(T provider, MethodInfo methodInfo, string providerCategory, string alternativeName = "")
    {
        _provider = provider;
        _methodInfo = methodInfo;
        _providerCategory = providerCategory;
    }

    public static MethodClassTerminal<T> Create(T provider, MethodInfo methodInfo, string providerCategory,
        string alternativeName = "")
    {
        var terminal = new MethodClassTerminal<T>(provider, methodInfo, providerCategory, alternativeName);
        terminal.Initialize();
        return terminal;
    }

    private void Initialize()
    {
        CreateInputs();
        CreateOutputs();
    }

    public override void Execute()
    {
    }

    private protected override bool ValidateInput(ISocket socket, object? value)
    {
        if (value == null)
            // Accept null if the parameter type is a reference type or nullable
            return !socket.ParameterType.IsValueType || Nullable.GetUnderlyingType(socket.ParameterType) != null;

        return socket.ParameterType.IsInstanceOfType(value);
    }

    private protected override void CreateInputs()
    {
        if (_methodInfo.GetParameters().Length == 0) return;
        foreach (ParameterInfo parameter in _methodInfo.GetParameters())
        {
            InputSockets.Add(new MethodSocket(parameter.Name, parameter.ParameterType, false, SocketTypeEnum.Input));
        }
    }

    private protected override void CreateOutputs()
    {
        OutputSockets.Add(new MethodSocket(_methodInfo.ReturnParameter.Name, _methodInfo.ReturnParameter.ParameterType, false,
            SocketTypeEnum.Output));
    }
}