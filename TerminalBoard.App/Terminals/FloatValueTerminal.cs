using TerminalBoard.App.Enum;
using TerminalBoard.App.Functions;
using TerminalBoard.App.Interfaces.Functions;
using TerminalBoard.App.Interfaces.Terminals;

namespace TerminalBoard.App.Terminals;

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
            connectionWire.Value = Function.Output;
        }
    }
    
}