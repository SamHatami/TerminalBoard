using System.Reflection;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Extensions;
using TerminalBoard.Core.Interfaces.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;
using TerminalBoard.Core.Interfaces.Graphs.Wires;

namespace TerminalBoard.Core.Graphs.Sockets;

public class ParameterSocket : ISocket 
{
    public string Name { get; }
    public Guid Id { get; }
    public SocketDirection Direction { get; }
    public Type DataType { get; set; }
    public int SocketPosition { get; set; }
    public bool IsConnected { get; set; }
    public bool IsOptional { get; }
    public SocketDirection SocketType { get; }
    public ITerminal ParentTerminal { get; }
    public List<IWire> ConnectedWires { get; }

    public ParameterSocket(string parameterName, Type parameterType, bool isOptional, SocketDirection socketDirection, ITerminal parentTerminal)
    {
        DataType = parameterType;
        IsOptional = isOptional;
        Name = parameterName;
        ParentTerminal = parentTerminal;
        SocketType = socketDirection;
    }

    public ParameterSocket(ParameterInfo? parameter, SocketDirection socketType, ITerminal parentTerminal)
    {
        if (parameter == null)
            return;

        Name = parameter.ParameterType.GetAliasName() ?? "Unknown";
        SocketPosition = parameter.Position;
        IsOptional = parameter.IsOptional;
        DataType = parameter.ParameterType;
        SocketType = socketType;
        ParentTerminal = parentTerminal;
    }

    public void SetToConnected()
    {
        IsConnected = true;
    }
}