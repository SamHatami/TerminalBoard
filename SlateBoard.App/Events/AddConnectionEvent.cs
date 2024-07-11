using SlateBoard.App.Interface;

namespace SlateBoard.App.Events
{
    public class AddConnectionEvent(IWire wire)
    {
        public IWire wire = wire;
    }
}
