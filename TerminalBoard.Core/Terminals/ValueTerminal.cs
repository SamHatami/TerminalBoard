using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class ValueTerminal<T> : IValueTerminal<T>
{
    public string Label { get; }
    public List<ISocket> InputSockets { get; } = [];
    public List<ISocket> OutputSockets { get; } = [];
    public List<IWire> Connections { get; set; } = [];
    public bool RequireInputValue { get; } = true;

    public Guid Id { get; }
    public ITypedValueFunction<T> Function { get; }

    public ValueTerminal()
    {
        Function = new TypedValueOutputFunction<T>();
        Label = typeof(T).Name; //TODO: Perhaps a type name utility to return const strings
        Id = Guid.NewGuid();
        Initialize();
    }

    private void Initialize()
    {
        var socket = new Socket(SocketTypeEnum.Output, "", this) {ValueType = typeof(T)};
        var value = new TypedValue<T>(nameof(T), socket.Id) { Value = default };

        OutputSockets.Add(socket);
        UpdateInput(socket, value);
    }

    public void UpdateInput(ISocket socket, IValue newValue)
    {
        if (newValue is TypedValue<T> value)
        {
            Function.SetValue(value);
            NotifyConnectors();
        }
    }

    public void NotifyConnectors()
    {
        foreach (var connectionWire in Connections)
            if (connectionWire.StartSocket.ParentTerminal.Id == Id) //Only notify outbound connections
                connectionWire.Value = Function.Output;
    }
}