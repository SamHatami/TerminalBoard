using ElementaPrime.Core.Enum;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;

namespace ElementaPrime.Core.Graphs.Conduits;

public static class ConduitValidator
{
    public static bool Validate(IDataPort fromDataPort, IDataPort toDataPort)
    {
        if (fromDataPort == null || toDataPort == null)
            return false;

        List<bool> validations =
        [
            CircularLoopValidation(fromDataPort, toDataPort),
            SingleLoopValidation(fromDataPort, toDataPort),
            DirectionValidation(fromDataPort, toDataPort),
            TypeValidation(fromDataPort, toDataPort),
            InputOccupiedValidation(toDataPort),
            SingleInputValidation(fromDataPort, toDataPort)
        ];

        return validations.All(c => c);
        
    }

 //Do some graph sorting and check if there is any circularaity 


    private static bool CircularLoopValidation(IDataPort fromDataPort, IDataPort toDataPort)
    {
        var fromTerminal = fromDataPort.ParentOperator;
        var toTerminal = toDataPort.ParentOperator;

        // If the terminals are the same, it's already a loop
        if (fromTerminal == toTerminal)
            return false;

        // Start DFS from toTerminal, looking for fromTerminal
        return !HasPathToTerminal(toTerminal, fromTerminal, new HashSet<Guid>());
    }

    private static bool HasPathToTerminal(IOperator current, IOperator target, HashSet<Guid> visited)
    {
        if (current == null || visited.Contains(current.Id))
            return false;

        if (current == target)
            return true;

        visited.Add(current.Id);

        // Traverse all output connections from this terminal
        foreach (var outputSocket in current.OutputSockets)
        {
            foreach (var wire in current.Connections)
            {
                if (wire.Start == outputSocket)
                {
                    var nextTerminal = wire.End.ParentOperator;
                    if (HasPathToTerminal(nextTerminal, target, visited))
                        return true;
                }
            }
        }

        return false;
    }
    private static bool SingleInputValidation(IDataPort fromDataPort, IDataPort toDataPort)
    {
        if (toDataPort.Direction != DataPortDirection.Input)
            return false;

        return !toDataPort.IsConnected;
    }

    private static bool SingleLoopValidation(IDataPort fromDataPort, IDataPort toDataPort)
    {
        return toDataPort.ParentOperator == fromDataPort.ParentOperator ? false : true;
    }

    private static bool DirectionValidation(IDataPort fromDataPort, IDataPort toDataPort)
    {
        return fromDataPort.Direction != toDataPort.Direction? true : false;
    }

    private static bool TypeValidation(IDataPort fromDataPort, IDataPort toDataPort)
    {
        //Outputerminal does not manipulate input and dont need type validation
        if (toDataPort.ParentOperator is IOutputOperator) return true; 
        
        return fromDataPort.DataType == toDataPort.DataType ? true : false;

    }

    private static bool InputOccupiedValidation(IDataPort toDataPort)
    {
        return toDataPort.IsConnected != true;
    }
}