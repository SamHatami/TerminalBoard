using ISocket = TerminalBoard.App.Interfaces.ISocket;
using SocketTypeEnum = TerminalBoard.App.Enum.SocketTypeEnum;

namespace TerminalBoard.App.Terminals;

public class Socket : ISocket
{
    #region Properties

    public string Name { get; }
    public SocketTypeEnum Type { get; }

    #endregion Properties

    #region Constructors

    public Socket(SocketTypeEnum type, string name)
    {
        Type = type;
        Name = name;
    }

    #endregion Constructors
}