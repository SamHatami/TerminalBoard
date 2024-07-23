using TerminalBoard.App.Enum;
using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Interfaces.Terminals;

public interface ISocket
{
    SocketTypeEnum SocketType { get; }
    ITerminal ParentTerminal { get; }
    string Name { get; }
    Guid Id { get; }

}
