using SlateBoard.App.Interface.ViewModel;

namespace SlateBoard.App.Events
{
    public class TerminalRemovedEvent(ITerminal terminal)
    {
        public ITerminal Terminal { get; } = terminal;
    }
}