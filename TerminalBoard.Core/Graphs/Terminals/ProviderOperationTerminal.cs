using System.Reflection;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Extensions;
using TerminalBoard.Core.Graphs.Excecution;
using TerminalBoard.Core.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;

namespace TerminalBoard.Core.Graphs.Terminals;

public class ProviderOperationTerminal : ProviderTerminalBase
{
    private readonly MethodInfo _methodInfo;
    private readonly string _providerCategory;

    private ProviderOperationTerminal(object provider, MethodInfo methodInfo, string providerCategory,
        string alternativeName = "")
    {
        ProviderInstance = provider;
        ProviderCategory = providerCategory; //used for grouping in the UI
        _methodInfo = methodInfo;
        _providerCategory = providerCategory;
        Label = _methodInfo.Name; //Get the name of the method from the attribute
        TerminalDefinitionId = GetMethodTerminalId(methodInfo.Name, provider);
    }

    public static IProviderTerminal Create(object provider, MethodInfo methodInfo, string providerCategory,
        string alternativeName = "")
    {
        var terminal = new ProviderOperationTerminal(provider, methodInfo, providerCategory, alternativeName);
        terminal.Initialize();
        return terminal;
    }

    private void Initialize()
    {
        CreateInputs();
        CreateOutputs();
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

    public override TerminalExcecutionResult Execute()
    {
        var method = ProviderInstance.GetType().GetMethod(_methodInfo.Name);
        if (method == null) return TerminalExcecutionResult.Failed;

        //Check if all mandatory inputs are connected
        foreach (var input in InputSockets)
        {
            if (!input.IsConnected && !input.IsOptional)
                return TerminalExcecutionResult.Failed;
        }

        var orderedSockets = InputSockets.Where(i => i.IsConnected).OrderBy(i => i.SocketPosition);

        var orderedParameterValues = GetValuesFromConnections(orderedSockets).Select(v => v.Value).ToArray();

        method.Invoke(ProviderInstance, orderedParameterValues);

        //get output connections and call updateInput on all of them

        //in async methods, the next in the order still needs to wait for all the terminals before that its dependant on to finish

        return TerminalExcecutionResult.Success;
    }

    private protected override bool ValidateInput(ISocket socket, object? value)
    {
        if (value == null)
            // Accept null if the parameter type is a reference type or nullable
            return !socket.DataType.IsValueType || Nullable.GetUnderlyingType(socket.DataType) != null;

        return socket.DataType.IsInstanceOfType(value);
    }

    private protected override void CreateInputs()
    {
        if (_methodInfo.GetParameters().Length == 0) return;

        foreach (var parameter in _methodInfo.GetParameters())
        {
            InputSockets.Add(new ParameterSocket(parameter, SocketDirection.Input, this));
        }
    }

    private protected override void CreateOutputs()
    {
        var returnParameterName = _methodInfo.ReturnType.GetAliasName() ?? "Unknown";

        OutputSockets.Add(new ParameterSocket(returnParameterName,
            _methodInfo.ReturnParameter.ParameterType, false,
            SocketDirection.Output, this));
    }
}