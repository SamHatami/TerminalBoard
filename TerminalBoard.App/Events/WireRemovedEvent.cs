using IWireViewModel = TerminalBoard.App.Interfaces.ViewModels.IWireViewModel;

namespace TerminalBoard.App.Events;

public class WireRemovedEvent(IWireViewModel wire)

{
    public IWireViewModel Wire { get; } = wire;
}