using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Wires;

//Wires hold the value of the terminals that are connected to it
public class WireConnection : IWire
{
    public ISocket StartSocket { get; set; }
    public ISocket EndSocket { get; set; }

    private IValue _value;
    public IValue Value
    {
        get => _value;
        set => _value = value;
    }
    public Guid Id { get; } = Guid.NewGuid();

    public WireConnection(ISocket startSocket, ISocket endSocket, IValue value) //Type check of sockets happens in WireConnectionValidator
    {
        _value = value;
        StartSocket = startSocket;
        EndSocket = endSocket;
    }
    
    public IValue? GetCorrespondingSocketValue(ISocket socket)
    {
        return socket.Id == EndSocket.Id || socket.Id == StartSocket.Id ? Value : null;
    }
}