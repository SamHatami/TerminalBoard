using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Graph.Sockets;

public abstract class ProviderSocketBase: ISocket
{


    protected ProviderSocketBase(string name, SocketDirection direction, Type dataType, ITerminal parentTerminal)
    {
        Name = name;
        Direction = direction;
        DataType = dataType;
        ParentTerminal = parentTerminal;
    }

    public string Name { get; }
    public Guid Id { get; } = Guid.NewGuid();
    public SocketDirection Direction { get; } 
    public Type DataType { get; set; }
    public int SocketPosition { get; set; }
    public bool IsConnected { get; set; }
    public bool IsOptional { get; }
    public ITerminal ParentTerminal { get; } 

    public List<IWire> ConnectedWires { get; }

    internal void AddWire(IWire wire) => ConnectedWires.Add(wire);
    internal void RemoveWire(IWire wire) => ConnectedWires.Remove(wire);
}