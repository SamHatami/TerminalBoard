
using ElementaPrime.TestApp.Interfaces.ViewModels;

namespace ElementaPrime.TestApp.Events.UIEvents;

public class AddConnectionEvent(ISocketViewModel start,  ISocketViewModel end)
{
    public ISocketViewModel Start { get; } = start;
    public ISocketViewModel End { get;  }= end;
}

public class RemoveConnectionEvent(IWireViewModel wire)
{
    public IWireViewModel Wire = wire;
}