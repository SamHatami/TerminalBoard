using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Wires;

public static class WireConnectionValidator
{
    public static bool Validate(ISocket fromSocket, ISocket toSocket)
    {
        if (fromSocket == null || toSocket == null)
            return false;

        List<bool> validations = [];

        validations.Add(LoopValidation(fromSocket, toSocket));
        validations.Add(DirectionValidation(fromSocket, toSocket));
        validations.Add(TypeValidation(fromSocket, toSocket));
        validations.Add(InputOccupiedValidation(toSocket));

        return validations.All(c => c);
        
    }

    private static bool LoopValidation(ISocket fromSocket, ISocket toSocket)
    {
        return toSocket.ParentTerminal == fromSocket.ParentTerminal ? false : true;
    }

    private static bool DirectionValidation(ISocket fromSocket, ISocket toSocket)
    {
        return fromSocket.SocketType != toSocket.SocketType? true : false;
    }

    private static bool TypeValidation(ISocket fromSocket, ISocket toSocket)
    {
        //Outputerminal does not manipulate input and dont need type validation
        if (toSocket.ParentTerminal is IOutputTerminal) return true; 
        
        return fromSocket.ValueType == toSocket.ValueType ? true : false;

    }

    private static bool InputOccupiedValidation(ISocket toSocket)
    {
        return toSocket.IsConnected != true;
    }
}