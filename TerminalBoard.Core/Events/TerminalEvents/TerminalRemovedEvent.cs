using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Events.TerminalEvents;

public class TerminalRemovedEvent(ITerminal terminal)
{
    public ITerminal Terminal { get; } = terminal;
}