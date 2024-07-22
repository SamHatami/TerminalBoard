using IWire = TerminalBoard.App.Interfaces.ViewModels.IWire;

namespace TerminalBoard.App.Events;

public class WireRemovedEvent(IWire wire)

{
    public IWire Wire { get; } = wire;
}