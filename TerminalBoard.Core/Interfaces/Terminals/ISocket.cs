using TerminalBoard.Core.Enum;

namespace TerminalBoard.Core.Interfaces.Terminals;

public interface ISocket
{
    SocketTypeEnum SocketType { get; }
    Type ParameterType { get; set; }
    int ParameterPosition { get; set; }
    bool IsConnected { get; set; }
    ITerminal ParentTerminal { get; }
    string Name { get; }
    Guid Id { get; }
    public bool IsOptional { get; }



}
