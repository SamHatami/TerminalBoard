using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;

namespace TerminalBoard.Core.Graphs.Wires;

public static class WireConnectionValidator
{
    public static bool Validate(ISocket fromSocket, ISocket toSocket)
    {
        if (fromSocket == null || toSocket == null)
            return false;

        List<bool> validations =
        [
            CircularLoopValidation(fromSocket, toSocket),
            SingleLoopValidation(fromSocket, toSocket),
            DirectionValidation(fromSocket, toSocket),
            TypeValidation(fromSocket, toSocket),
            InputOccupiedValidation(toSocket),
            SingleInputValidation(fromSocket, toSocket)
        ];

        return validations.All(c => c);
        
    }

 //Do some graph sorting and check if there is any circularaity 


    private static bool CircularLoopValidation(ISocket fromSocket, ISocket toSocket)
    {
        var fromTerminal = fromSocket.ParentTerminal;
        var toTerminal = toSocket.ParentTerminal;

        // If the terminals are the same, it's already a loop
        if (fromTerminal == toTerminal)
            return false;

        // Start DFS from toTerminal, looking for fromTerminal
        return !HasPathToTerminal(toTerminal, fromTerminal, new HashSet<Guid>());
    }

    private static bool HasPathToTerminal(ITerminal current, ITerminal target, HashSet<Guid> visited)
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
                if (wire.StartSocket == outputSocket)
                {
                    var nextTerminal = wire.EndSocket.ParentTerminal;
                    if (HasPathToTerminal(nextTerminal, target, visited))
                        return true;
                }
            }
        }

        return false;
    }
    private static bool SingleInputValidation(ISocket fromSocket, ISocket toSocket)
    {
        if (toSocket.Direction != SocketDirection.Input)
            return false;

        return !toSocket.IsConnected;
    }

    private static bool SingleLoopValidation(ISocket fromSocket, ISocket toSocket)
    {
        return toSocket.ParentTerminal == fromSocket.ParentTerminal ? false : true;
    }

    private static bool DirectionValidation(ISocket fromSocket, ISocket toSocket)
    {
        return fromSocket.Direction != toSocket.Direction? true : false;
    }

    private static bool TypeValidation(ISocket fromSocket, ISocket toSocket)
    {
        //Outputerminal does not manipulate input and dont need type validation
        if (toSocket.ParentTerminal is IOutputTerminal) return true; 
        
        return fromSocket.DataType == toSocket.DataType ? true : false;

    }

    private static bool InputOccupiedValidation(ISocket toSocket)
    {
        return toSocket.IsConnected != true;
    }
}