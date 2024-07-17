using SlateBoard.App.Interface.ViewModel;

namespace SlateBoard.App.Events
{
    public class WireRemovedEvent(IWire wire)

    {
        public IWire Wire { get; } = wire;
    }
}