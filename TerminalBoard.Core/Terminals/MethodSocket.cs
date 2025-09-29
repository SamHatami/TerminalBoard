using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;
public class MethodSocket : ISocket
{
    public Type ParameterType { get; set; }
    public bool IsOptional { get; }
    public SocketTypeEnum SocketType { get; }
    public bool IsConnected { get; }
    public ITerminal ParentTerminal { get; }
    public string Name { get; }
    public Guid Id { get; }

    public MethodSocket(string parameterName, Type parameterType, bool isOptional, SocketTypeEnum socketType)
    {
        ParameterType = parameterType;
        IsOptional = isOptional;
        Name = parameterName;
    }

}