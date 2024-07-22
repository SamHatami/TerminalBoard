using TerminalBoard.App.Enum;

namespace TerminalBoard.App.Interfaces;

public interface ISocket
{
    SocketTypeEnum Type { get; }
    string Name { get; }
}