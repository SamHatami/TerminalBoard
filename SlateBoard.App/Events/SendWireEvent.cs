using SlateBoard.App.Interface;

namespace SlateBoard.App.Events;

public class SendWireEvent(IWire wire)
{
    public IWire Wire { get; set; } = wire;
}