
using TerminalBoard.App.Interfaces.ViewModels;

namespace TerminalBoard.App.Events.UIEvents;

public class ConnectionEvents
{
}

public class AddConnectionEvent(IWireViewModel wire)
{
    public IWireViewModel Wire = wire;
}

public class RemoveConnectionEvent(IWireViewModel wire)
{
    public IWireViewModel Wire = wire;
}