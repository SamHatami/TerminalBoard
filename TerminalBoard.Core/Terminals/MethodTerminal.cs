using System.Reflection;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Extensions;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;
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
        if(_provider == null)
            return;

        var method = _provider.GetType().GetMethod(_methodInfo.Name);
        if (method == null) return;

        //Check if all mandatory inputs are connected
        foreach (var input in InputSockets)
        {
            if (!input.IsConnected && !input.IsOptional)
                // Mandatory input not connected, cannot execute
                return;
        }

        var orderedSockets = InputSockets.Where(i => i.IsConnected).OrderBy(i => i.ParameterPosition);

        var orderedParameterValues = GetValuesFromConnections(orderedSockets).Select(v => v.Value).ToArray();

        method.Invoke(_provider, orderedParameterValues);

        //get output connections and call updateInput on all of them

        //in async methods, the next in the order still needs to wait for all the terminals before that its dependant on to finish
    }

    private IValue[] GetValuesFromConnections(IEnumerable<ISocket> sockets)
    {
        List<IValue> values = new();
        foreach (var socket in sockets)
        {
            var value = Connections.FirstOrDefault(c => c.EndSocket.Id == socket.Id)?.Value;
            if (value != null)
                values.Add(value);
        }

        return values.ToArray();
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
            InputSockets.Add(new ParameterSocket(parameter, SocketTypeEnum.Input, this));
        }
    }

    private protected override void CreateOutputs()
    {
        var returnParameterName = _methodInfo.ReturnType.GetAliasName() ?? "Unknown";

        OutputSockets.Add(new ParameterSocket(returnParameterName,
                _methodInfo.ReturnParameter.ParameterType, false,
                SocketTypeEnum.Output, this));
    }
}