using System.Reflection;
using ElementaPrime.Core.Enum;
using ElementaPrime.Core.Graphs.DataPorts;
using ElementaPrime.Core.Graphs.Excecution;
using ElementaPrime.Core.Interfaces.Functions;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Extensions;
using ElementaPrime.Core.Functions;
using ElementaPrime.Core.Graphs.Helpers;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Graphs.Operators;

public class ProviderOperator : ProviderOperatorBase
{
    private readonly MethodInfo _methodInfo;
    private readonly string _providerCategory;

    private ProviderOperator(object provider, MethodInfo methodInfo, string providerCategory,
        string alternativeName = "")
    {
        ProviderInstance = provider;
        ProviderCategory = providerCategory; //used for grouping in the UI
        _methodInfo = methodInfo;
        _providerCategory = providerCategory;
        Label = _methodInfo.Name; //Get the name of the method from the attribute
        TerminalDefinitionId = GetMethodTerminalId(methodInfo.Name, provider);
    }

    public static IProviderOperator Create(object provider, MethodInfo methodInfo, string providerCategory,
        string alternativeName = "")
    {
        var terminal = new ProviderOperator(provider, methodInfo, providerCategory, alternativeName);
        terminal.Initialize();
        return terminal;
    }

    private void Initialize()
    {
        CreateInputs();
        CreateOutputs();
    }
    
    public override OperationResult Execute()
    {
        var method = ProviderInstance.GetType().GetMethod(_methodInfo.Name);
        if (method == null) return OperationResult.Failed;

        //Check if all mandatory inputs are connected
        foreach (var input in InputSockets)
        {
            if (!input.IsConnected && !input.IsOptional)
                return OperationResult.Failed;
        }

        var orderedSockets = InputSockets.Where(i => i.IsConnected).OrderBy(i => i.SocketPosition);

        var orderedParameterValues = GetValuesFromConnections(orderedSockets).Select(v => v.Value).ToArray();

        var result = method.Invoke(ProviderInstance, orderedParameterValues);

        if (result == null && _methodInfo.ReturnType == null)
            return OperationResult.Success;


        var outConduits = GetOutGoingConduits();
        var returnParameterValue = new ReturnValue() { Value = result };

        //set the values into the outgoing conduits
        foreach (var conduit in outConduits)
        {
            conduit.Value = returnParameterValue;
        }
        //add the return into the outgoing conduit
        //get output connections and call updateInput on all of them

            //in async methods, the next in the order still needs to wait for all the terminals before that its dependant on to finish

        return OperationResult.Success;
    }

    private protected override bool ValidateInput(IDataPort dataPort, object? value)
    {
        if (value == null)
            // Accept null if the parameter type is a reference type or nullable
            return !dataPort.DataType.IsValueType || Nullable.GetUnderlyingType(dataPort.DataType) != null;

        return dataPort.DataType.IsInstanceOfType(value);
    }

    private protected override void CreateInputs()
    {
        if (_methodInfo.GetParameters().Length == 0) return;

        foreach (var parameter in _methodInfo.GetParameters())
        {
            InputSockets.Add(new ParameterDataPort(parameter, DataPortDirection.Input, this));
        }
    }

    private protected override void CreateOutputs()
    {
        var returnParameterName = _methodInfo.ReturnType.GetAliasName() ?? "Unknown";

        Outputs.Add(new ParameterDataPort(returnParameterName,
            _methodInfo.ReturnParameter.ParameterType, false,
            DataPortDirection.Output, this));
    }

    private IValue[] GetValuesFromConnections(IEnumerable<IDataPort> sockets)
    {
        List<IValue> values = new();
        foreach (var socket in sockets)
        {
            var value = Connections.FirstOrDefault(c => c.End.Id == socket.Id)?.Value;
            if (value != null)
                values.Add(value);
        }

        return values.ToArray();
    }

    private IConduit[] GetOutGoingConduits()
    {
        //Should only have one output 

        if (Outputs.Count is 0 or > 2)
            return [];

        return Connections.Where(c => c.Start == Outputs[0]).ToArray();
    }
}