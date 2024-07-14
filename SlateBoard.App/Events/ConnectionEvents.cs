using SlateBoard.App.Interface.ViewModel;

namespace SlateBoard.App.Events
{
    public class ConnectionEvents
    {

    }
    
    public class AddConnectionEvent(IWire wire)
    {
        public IWire Wire = wire;
    }

    public class RemoveConnectionEvent(IWire wire)
    {
        public IWire Wire = wire;
    }
}
