using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Graph.Sockets;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Graph.Terminals;


//Not sure of this class yet, this terminal should be able to take any value type and show it. But not all outputs will be able to show a value.
//it just might aswell not be used for antying at all.
public class OutputValueTerminal : IOutputTerminal
{
    public Guid Id { get; }
    public string Label { get; } = "Result";
    public string TerminalDefinitionId { get; }
    public List<ISocket> InputSockets { get; } = [];
    public List<ISocket> OutputSockets { get; } = [];
    public TerminalExcecutionResult Execute() => TerminalExcecutionResult.NotApplicable;
    public List<IWire> Connections { get; set; } = [];
    public bool ShowFinalOutputValue { get; } = true;

    public IValue Value { get; set; } 
    public OutputValueTerminal()
    {
        Id = Guid.NewGuid();
        TerminalDefinitionId = $"SimpleOutputTerminal";
        Initialize();
    }

    private void Initialize()
    {
        InputSockets.Add(new ValueSocket(SocketDirection.Input, "", this, typeof(string))); //PLaceholder
    }
}