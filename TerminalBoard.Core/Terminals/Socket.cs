using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class Socket(SocketTypeEnum socketType, string name, ITerminal parentTerminal) : ISocket
{
    public SocketTypeEnum SocketType { get; } = socketType;
    public ITerminal ParentTerminal { get; } = parentTerminal;
    public string Name { get; } = name;
    public Guid Id { get; } = Guid.NewGuid();
}