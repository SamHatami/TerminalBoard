using ElementaPrime.Core.Enum;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Interfaces.Graphs.Sockets;

public interface IDataPort
{
    string Name { get; }
    Guid Id { get; }
    DataPortDirection Direction { get; }
    Type DataType { get; set; }
    public int SocketPosition { get; set; }

    bool IsConnected { get; set; }
    public bool IsOptional { get; }
    
    IOperator ParentOperator { get; }

    List<IConduit> ConnectedWires { get; }

}
