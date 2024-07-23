using TerminalBoard.App.Enum;
using TerminalBoard.App.Interfaces.Functions;
using TerminalBoard.App.Interfaces.Terminals;

namespace TerminalBoard.App.Terminals;

internal class SimpleOutputTerminal : IOutputTerminal
{
    public string Label { get; } = "Output";
    public List<ISocket> InputSockets { get; } = [];
    public List<ISocket> OutputSockets { get; } = [];
    public List<IWire> Connections { get; set; } = [];
    public bool ShowFinalOutputValue { get; } = true;
    public IValue Output { get; set; }


    public void UpdateInput(ISocket socket, IValue newValue)
    {
        Output = newValue;
    }


    public Guid Id { get; }

    public SimpleOutputTerminal()
    {
        Id = Guid.NewGuid();

        Initialize();
    }

    private void Initialize()
    {
        InputSockets.Add(new Socket(SocketTypeEnum.Input, "", this));
    }
}