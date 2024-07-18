using TerminalBoard.App.Interface.ViewModel;

namespace TerminalBoard.App.Events;

public class SocketMovedEvent(ISocket socket)
{
    public ISocket Socket { get; set; } = socket;
}