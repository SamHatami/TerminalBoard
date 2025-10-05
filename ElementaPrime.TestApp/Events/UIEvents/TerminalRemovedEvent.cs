
using ElementaPrime.TestApp.Interfaces.ViewModels;

namespace ElementaPrime.TestApp.Events.UIEvents;

public class TerminalRemovedEvent(ITerminalViewModel terminal)
{
    public ITerminalViewModel Terminal { get; } = terminal;
}