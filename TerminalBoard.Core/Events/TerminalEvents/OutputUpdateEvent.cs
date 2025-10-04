using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;

namespace TerminalBoard.Core.Events.TerminalEvents;

public class OutputUpdateEvent(IValue output, IOutputTerminal terminal)
{
    public IValue Output { get; } = output;
    public IOutputTerminal Terminal { get; } = terminal;
}