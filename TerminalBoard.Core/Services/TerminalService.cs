using Caliburn.Micro;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Events.TerminalEvents;
using TerminalBoard.Core.Interfaces;
using TerminalBoard.Core.Interfaces.Terminals;
using TerminalBoard.Core.Terminals;

namespace TerminalBoard.Core.Services;

public class TerminalService
{
    private readonly ITerminalCreator _creator;
    private readonly IEventAggregator _events;

    public TerminalService(ITerminalCreator creator) //the creator is App or module dependant
    {
        _creator = creator;
        _events = TerminalHelper.EventsAggregator;
        _events.PublishOnBackgroundThreadAsync(this);
    }

    private ITerminal CreateTerminal(TerminalType terminalType)
    {
        return _creator.Instantiate(terminalType);
    }

    public void UpdateTerminal()
    {
    }

    public void RemoveTerminal(ITerminal terminal)
    {
        _events.PublishOnBackgroundThreadAsync(new TerminalRemovedEvent(terminal), CancellationToken.None);
    }
}