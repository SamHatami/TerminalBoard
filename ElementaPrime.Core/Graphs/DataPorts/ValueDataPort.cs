using ElementaPrime.Core.Enum;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Graphs.DataPorts;

public class ValueDataPort: IDataPort
{
    public string Name { get; }
    public Guid Id { get; }
    public DataPortDirection Direction { get; }
    public Type DataType { get; set; }
    public int SocketPosition { get; set; } = 0;
    public bool IsConnected { get; set; }
    public bool IsOptional { get; } = false;
    public IOperator ParentOperator { get; } 
    public List<IConduit> ConnectedWires { get; } = new();

    public ValueDataPort(DataPortDirection dataPortDirection, string name, IOperator parentOperator, Type dataType)
    {
        Direction = dataPortDirection;
        Name = name;
        ParentOperator = parentOperator;
        DataType = dataType;
    }
}