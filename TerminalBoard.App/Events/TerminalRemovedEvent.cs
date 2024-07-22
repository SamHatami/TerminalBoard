using ITerminalViewModel = TerminalBoard.App.Interfaces.ViewModels.ITerminalViewModel;

namespace TerminalBoard.App.Events;

public class TerminalRemovedEvent(ITerminalViewModel terminal)
{
    public ITerminalViewModel Terminal { get; } = terminal;
}