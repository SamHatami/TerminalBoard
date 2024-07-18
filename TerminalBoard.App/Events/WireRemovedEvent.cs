using TerminalBoard.App.Interface.ViewModel;

namespace TerminalBoard.App.Events
{
    public class WireRemovedEvent(IWire wire)

    {
        public IWire Wire { get; } = wire;
    }
}