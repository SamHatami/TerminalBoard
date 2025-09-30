using Caliburn.Micro;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Events.TerminalEvents;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class SimpleOutputTerminal : IOutputTerminal
{
    public string Label { get; } = "Result";
    public string TerminalDefinitionId { get; }
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
    public void Execute()
    {

        return;
    }

    public SimpleOutputTerminal()
    {
        Id = Guid.NewGuid();
        Output = new TypedValue<string>("", Guid.Empty){Value = "0"};
        TerminalDefinitionId = $"SimpleOutputTerminal";
        _events = TerminalHelper.EventsAggregator;
        _events.SubscribeOnBackgroundThread(this);
        Initialize();
    }

    private void Initialize()
    {
        InputSockets.Add(new Socket(SocketTypeEnum.Input, "", this){ParameterType = typeof(float)});
    }
}