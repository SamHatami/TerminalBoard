using SlateBoard.App.Interface;

namespace SlateBoard.App.Events;

public class CreateWireEvent(INode point)
{
    public INode Point = point;
}