using Caliburn.Micro;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Events.TerminalEvents;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class SimpleOutputTerminal : IOutputTerminal
{
    public string Label { get; } = "Output";
    public List<ISocket> InputSockets { get; } = [];
    public List<ISocket> OutputSockets { get; } = [];
    public List<IWire> Connections { get; set; } = [];
    public bool ShowFinalOutputValue { get; } = true;

    private IEventAggregator _events;
    public IValue Output { get; set; }

    
    public void UpdateInput(ISocket socket, IValue newValue)
    {
        Output = newValue;
        _events.PublishOnBackgroundThreadAsync(new OutputUpdateEvent(Output, this));
    }

    


    public Guid Id { get; }

    public SimpleOutputTerminal()
    {
        Id = Guid.NewGuid();
        Output = new StringValue("0", "", new Guid());

        _events = TerminalHelper.EventsAggregator;
        _events.SubscribeOnBackgroundThread(this);
        Initialize();
    }

    private void Initialize()
    {
        InputSockets.Add(new Socket(SocketTypeEnum.Input, "", this));
    }
}