using ElementaPrime.Core.Interfaces.Functions;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;

namespace ElementaPrime.Core.Events.TerminalEvents;

public class OutputUpdateEvent(IValue output, IOutputOperator @operator)
{
    public IValue Output { get; } = output;
    public IOutputOperator Operator { get; } = @operator;
}