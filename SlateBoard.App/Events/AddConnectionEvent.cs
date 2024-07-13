using SlateBoard.App.Interface.ViewModel;

namespace SlateBoard.App.Events
{
    public class AddConnectionEvent(IWire wire)
    {
        public IWire wire = wire;
    }
}
