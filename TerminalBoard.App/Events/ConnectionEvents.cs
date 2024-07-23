using IWireViewModel = TerminalBoard.App.Interfaces.ViewModels.IWireViewModel;

namespace TerminalBoard.App.Events;

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