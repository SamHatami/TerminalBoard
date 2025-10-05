
using ElementaPrime.TestApp.Interfaces.ViewModels;

namespace ElementaPrime.TestApp.Events.UIEvents;

public class WireRemovedEvent(IWireViewModel wire)

{
    public IWireViewModel Wire { get; } = wire;
}