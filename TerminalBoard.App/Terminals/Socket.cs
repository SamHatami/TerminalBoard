using ISocket = TerminalBoard.App.Interfaces.ISocket;
using SocketTypeEnum = TerminalBoard.App.Enum.SocketTypeEnum;

namespace TerminalBoard.App.Terminals;

public class Socket(SocketTypeEnum type, string name) : ISocket
{
    public string Name { get; } = name;
    public SocketTypeEnum Type { get; } = type;
}