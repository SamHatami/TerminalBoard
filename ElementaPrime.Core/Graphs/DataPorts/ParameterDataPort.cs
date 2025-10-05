using System.Reflection;
using ElementaPrime.Core.Enum;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;
using ElementaPrime.Core.Extensions;

namespace ElementaPrime.Core.Graphs.DataPorts;

public class ParameterDataPort : IDataPort 
{
    public string Name { get; }
    public Guid Id { get; }
    public DataPortDirection Direction { get; }
    public Type DataType { get; set; }
    public int SocketPosition { get; set; }
    public bool IsConnected { get; set; }
    public bool IsOptional { get; }
    public DataPortDirection DataPortType { get; }
    public IOperator ParentOperator { get; }
    public List<IConduit> ConnectedWires { get; }

    public ParameterDataPort(string parameterName, Type parameterType, bool isOptional, DataPortDirection dataPortDirection, IOperator parentOperator)
    {
        DataType = parameterType;
        IsOptional = isOptional;
        Name = parameterName;
        ParentOperator = parentOperator;
        DataPortType = dataPortDirection;
    }

    public ParameterDataPort(ParameterInfo? parameter, DataPortDirection dataPortType, IOperator parentOperator)
    {
        if (parameter == null)
            return;

        Name = parameter.ParameterType.GetAliasName() ?? "Unknown";
        SocketPosition = parameter.Position;
        IsOptional = parameter.IsOptional;
        DataType = parameter.ParameterType;
        DataPortType = dataPortType;
        ParentOperator = parentOperator;
    }

    public void SetToConnected()
    {
        IsConnected = true;
    }
}