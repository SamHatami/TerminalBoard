using Caliburn.Micro;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Graphs.Helpers;

public static class TerminalHelper
{
    public static IEventAggregator EventsAggregator { get; set; }
}

public static class OperatorExtensions
{
    public static IConduit[] GetOutGoingConduits(this IOperator op)
    {
        //Should only have one output 

        if (op.Outputs.Count is 0 or > 2)
            return [];

        var outPort = op.Outputs[0];

        return op.Connections.Where(c => c.Start == outPort).ToArray();
    }
}