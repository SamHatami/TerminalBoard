using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class FloatValueTerminal : IValueTerminal
{
    public string Label { get; }
    public List<ISocket> InputSockets { get; } = [];
    public List<ISocket> OutputSockets { get; } = [];
    public List<IWire> Connections { get; set; } = [];
    public bool RequireInputValue { get; } = true;
    
    public Guid Id { get; }
    public IValueFunction Function { get; }

    public FloatValueTerminal()
    {
        Function = new Functions.ValueOutputFunction();
        Label = Function.Label;
        Id = Guid.NewGuid();
        Initialize();

    }

    private void Initialize()
    {
        var socket = new Socket(SocketTypeEnum.Output, "", this);
        var floatValue = new FloatValue(1.0f, "", socket.Id);

        OutputSockets.Add(socket);
        UpdateInput(socket,floatValue);
    }

    public void UpdateInput(ISocket socket, IValue newValue)
    {
        if(newValue is FloatValue floatValue)
        {
            Function.SetValue(floatValue);
            NotifyConnectors();
        }
    }
    public void NotifyConnectors()
    {
        foreach (var connectionWire in Connections)
        {
            if (connectionWire.StartSocket.ParentTerminal.Id == this.Id) //Only notify outbound connections
                connectionWire.Value = Function.Output;
        }
    }
    
}