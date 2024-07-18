using TerminalBoard.App.Interface.ViewModel;

namespace TerminalBoard.App.Events
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
