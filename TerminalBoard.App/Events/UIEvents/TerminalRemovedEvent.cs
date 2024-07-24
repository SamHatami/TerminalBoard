
using TerminalBoard.App.Interfaces.ViewModels;

namespace TerminalBoard.App.Events.UIEvents;

public class TerminalRemovedEvent(ITerminalViewModel terminal)
{
    public ITerminalViewModel Terminal { get; } = terminal;
}