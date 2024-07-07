using SlateBoard.App.Interface;

namespace SlateBoard.App.Events;

public class UpdateWireVisualsEvent(IWire wire)
{
    public IWire Wire { get; set; } = wire;
}