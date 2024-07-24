using TerminalBoard.Core.Enum;

namespace TerminalBoard.Core.Interfaces.Terminals;

public interface ISocket
{
    SocketTypeEnum SocketType { get; }
    ITerminal ParentTerminal { get; }
    string Name { get; }
    Guid Id { get; }

}
