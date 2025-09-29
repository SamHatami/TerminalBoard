using System.Reflection;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Extensions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class MethodTerminal<T> : ClassTerminalBase<T> where T : class
{
    private readonly T _provider;
    private readonly MethodInfo _methodInfo;
    private readonly string _providerCategory;

    private MethodTerminal(T provider, MethodInfo methodInfo, string providerCategory, string alternativeName = "")
    {
        _provider = provider;
        _methodInfo = methodInfo;
        _providerCategory = providerCategory;
        Label = _methodInfo.Name;
        TerminalDefinitionId = GetMethodTerminalId(methodInfo.Name);

        ProviderCategory = providerCategory; //used for grouping in the UI
    }

    public static MethodTerminal<T> Create(T provider, MethodInfo methodInfo, string providerCategory,
        string alternativeName = "")
    {
        var terminal = new MethodTerminal<T>(provider, methodInfo, providerCategory, alternativeName);
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

        foreach (var parameter in _methodInfo.GetParameters())
        {
            var parameterName = parameter.ParameterType.GetAliasName() ?? "Unknown";

            InputSockets.Add(new MethodSocket(parameterName, parameter.ParameterType, false, SocketTypeEnum.Input));
        }
    }

    private protected override void CreateOutputs()
    {
        var returnParameterName = _methodInfo.ReturnType.GetAliasName() ?? "Unknown";

        OutputSockets.Add(new MethodSocket(returnParameterName,
                _methodInfo.ReturnParameter.ParameterType, false,
                SocketTypeEnum.Output));
    }
}