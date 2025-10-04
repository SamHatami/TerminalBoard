using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;
using TerminalBoard.Core.Interfaces.Graphs.Wires;

namespace TerminalBoard.Core.Interfaces.Graphs.Sockets;

public interface ISocket
{
    string Name { get; }
    Guid Id { get; }
    SocketDirection Direction { get; }
    Type DataType { get; set; }
    public int SocketPosition { get; set; }

    bool IsConnected { get; set; }
    public bool IsOptional { get; }
    
    ITerminal ParentTerminal { get; }

    List<IWire> ConnectedWires { get; }

}
