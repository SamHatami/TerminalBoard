using SlateBoard.App.Interface.ViewModel;
using SlateBoard.App.ViewModels;

namespace SlateBoard.App.Events;

public class SocketMovedEvent(ISocket socket)
{
    public ISocket Socket { get; set; } = socket;
}