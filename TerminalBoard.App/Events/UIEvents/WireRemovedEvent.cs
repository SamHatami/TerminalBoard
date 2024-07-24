
using TerminalBoard.App.Interfaces.ViewModels;

namespace TerminalBoard.App.Events.UIEvents;

public class WireRemovedEvent(IWireViewModel wire)

{
    public IWireViewModel Wire { get; } = wire;
}