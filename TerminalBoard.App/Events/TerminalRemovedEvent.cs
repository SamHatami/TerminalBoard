using TerminalBoard.App.Interface.ViewModel;

namespace TerminalBoard.App.Events
{
    public class TerminalRemovedEvent(ITerminal terminal)
    {
        public ITerminal Terminal { get; } = terminal;
    }
}