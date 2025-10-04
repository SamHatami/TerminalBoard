using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;
using TerminalBoard.Core.Interfaces.Graphs.Wires;

namespace TerminalBoard.Core.Graphs.Sockets;

public class ValueSocket: ISocket
{
    public string Name { get; }
    public Guid Id { get; }
    public SocketDirection Direction { get; }
    public Type DataType { get; set; }
    public int SocketPosition { get; set; } = 0;
    public bool IsConnected { get; set; }
    public bool IsOptional { get; } = false;
    public ITerminal ParentTerminal { get; } 
    public List<IWire> ConnectedWires { get; } = new();

    public ValueSocket(SocketDirection socketDirection, string name, ITerminal parentTerminal, Type dataType)
    {
        Direction = socketDirection;
        Name = name;
        ParentTerminal = parentTerminal;
        DataType = dataType;
    }
}