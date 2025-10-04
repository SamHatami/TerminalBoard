using TerminalBoard.Core.Enum;

namespace TerminalBoard.Core.Interfaces.Terminals;

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
