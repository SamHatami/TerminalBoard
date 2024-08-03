using Caliburn.Micro;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Events.TerminalEvents;
using TerminalBoard.Core.Interfaces;
using TerminalBoard.Core.Interfaces.Terminals;
using TerminalBoard.Core.Terminals;

namespace TerminalBoard.Core.Services;

public class TerminalService
{
    private readonly ITerminalFactory _factory;
    private readonly IEventAggregator _events;

    public TerminalService(ITerminalFactory factory) //the factory is App or module dependant
    {
        _factory = factory;
        _events = TerminalHelper.EventsAggregator;
        _events.PublishOnBackgroundThreadAsync(this);
    }

    public ITerminal CreateTerminal(TerminalType terminalType)
    {
        return _factory.Instantiate(terminalType);
    }

    public void UpdateTerminal()
    {
    }

    public void RemoveTerminal(ITerminal terminal)
    {
        _events.PublishOnBackgroundThreadAsync(new TerminalRemovedEvent(terminal), CancellationToken.None);
    }
}