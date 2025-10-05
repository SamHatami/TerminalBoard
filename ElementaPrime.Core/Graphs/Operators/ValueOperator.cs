using ElementaPrime.Core.Enum;
using ElementaPrime.Core.Functions;
using ElementaPrime.Core.Graphs.DataPorts;
using ElementaPrime.Core.Graphs.Excecution;
using ElementaPrime.Core.Interfaces.Functions;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;
using ElementaPrime.Core.Extensions;

namespace ElementaPrime.Core.Graphs.Operators;

public class ValueOperator<T> : IValueOperator<T>
{
    public T Value { get; set; }
    public string Label { get; }
    public string TerminalDefinitionId { get; }
    public List<IDataPort> InputSockets { get; } = []; //None for Value Terminals, since they only produce an output
    public List<IDataPort> OutputSockets { get; } = [];
    public List<IConduit> Connections { get; set; } = [];
    public bool RequireInputValue { get; } = true;
    public Guid Id { get; }

    public OperationResult Execute() => OperationResult.NotApplicable;

    public ValueOperator()
    {
        Label = typeof(T).GetAliasName(); //TODO: Perhaps a type name utility to return const strings
        Id = Guid.NewGuid();
        TerminalDefinitionId = $"ValueTerminal<{Label}>";
        Initialize();
    }

    private void Initialize()
    {
        var socket = new ValueDataPort(DataPortDirection.Output, "hej", this,typeof(T));
        var value = new TypedValue<T>(nameof(T), socket.Id) { Value = default };

        OutputSockets.Add(socket);
        UpdateInput(socket, value);
    }

    public void UpdateInput(IDataPort dataPort, IValue newValue)
    {
        if (newValue is TypedValue<T> value)
            NotifyConnectors(newValue);
    }

    public void NotifyConnectors(IValue newValue)
    {
        foreach (var connectionWire in Connections)
            if (connectionWire.Start.ParentOperator.Id == Id) //Only notify outbound connections
                connectionWire.Value = newValue;
    }
    
}