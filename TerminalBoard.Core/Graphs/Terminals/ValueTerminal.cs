using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Extensions;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Graphs.Excecution;
using TerminalBoard.Core.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;
using TerminalBoard.Core.Interfaces.Graphs.Wires;

namespace TerminalBoard.Core.Graphs.Terminals;

public class ValueTerminal<T> : IValueTerminal<T>
{
    public T Value { get; set; }
    public string Label { get; }
    public string TerminalDefinitionId { get; }
    public List<ISocket> InputSockets { get; } = []; //None for Value Terminals, since they only produce an output
    public List<ISocket> OutputSockets { get; } = [];
    public List<IWire> Connections { get; set; } = [];
    public bool RequireInputValue { get; } = true;
    public Guid Id { get; }

    public TerminalExcecutionResult Execute() => TerminalExcecutionResult.NotApplicable;

    public ValueTerminal()
    {
        Label = typeof(T).GetAliasName(); //TODO: Perhaps a type name utility to return const strings
        Id = Guid.NewGuid();
        TerminalDefinitionId = $"ValueTerminal<{Label}>";
        Initialize();
    }

    private void Initialize()
    {
        var socket = new ValueSocket(SocketDirection.Output, "hej", this,typeof(T));
        var value = new TypedValue<T>(nameof(T), socket.Id) { Value = default };

        OutputSockets.Add(socket);
        UpdateInput(socket, value);
    }

    public void UpdateInput(ISocket socket, IValue newValue)
    {
        if (newValue is TypedValue<T> value)
            NotifyConnectors(newValue);
    }

    public void NotifyConnectors(IValue newValue)
    {
        foreach (var connectionWire in Connections)
            if (connectionWire.StartSocket.ParentTerminal.Id == Id) //Only notify outbound connections
                connectionWire.Value = newValue;
    }
    
}