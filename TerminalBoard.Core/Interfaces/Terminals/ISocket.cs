using TerminalBoard.Core.Enum;

namespace TerminalBoard.Core.Interfaces.Terminals;

public interface ISocket
{
    SocketTypeEnum SocketType { get; }
    Type ParameterType { get; set; }
    bool IsConnected { get; }
    ITerminal ParentTerminal { get; }
    string Name { get; }
    Guid Id { get; }
    
    public bool IsOptional { get; }

}
