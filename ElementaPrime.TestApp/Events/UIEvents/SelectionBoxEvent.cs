using ElementaPrime.TestApp.Interfaces.ViewModels;

namespace ElementaPrime.TestApp.Events.UIEvents;

public class SelectionBoxEvent(List<ITerminalViewModel> terminalViewModels, List<IWireViewModel> wireViewModels)
{
    public List<ITerminalViewModel> TerminalViewModels { get; } = terminalViewModels;
    public List<IWireViewModel> WireViewModels { get; } = wireViewModels;
}