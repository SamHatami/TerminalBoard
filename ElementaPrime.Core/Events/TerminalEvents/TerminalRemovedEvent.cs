using ElementaPrime.Core.Interfaces.Graphs.Terminals;

namespace ElementaPrime.Core.Events.TerminalEvents;

public class TerminalRemovedEvent(IOperator @operator)
{
    public IOperator Operator { get; } = @operator;
}