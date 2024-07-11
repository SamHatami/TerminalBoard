using SlateBoard.App.Interface;

namespace SlateBoard.App.Events;

public class CreateWireEvent(ISocket point)
{
    public ISocket Point = point;
}