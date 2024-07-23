using TerminalBoard.App.Enum;
using TerminalBoard.App.Interfaces.Functions;
using TerminalBoard.App.Interfaces.Terminals;

namespace TerminalBoard.App.Terminals;

public class Socket(SocketTypeEnum socketType, string name, ITerminal parentTerminal) : ISocket
{
    public SocketTypeEnum SocketType { get; } = socketType;
    public ITerminal ParentTerminal { get; } = parentTerminal;
    public string Name { get; } = name;
    public Guid Id { get; } = Guid.NewGuid();
}