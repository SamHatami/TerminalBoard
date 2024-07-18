using TerminalBoard.App.ViewModels;

namespace TerminalBoard.App.Events;

public class TerminalMovedEvent
{
    public TerminalViewModel TerminalViewModel { get; set; }
    public TerminalMovedEvent(TerminalViewModel viewModel)
    {
        TerminalViewModel = viewModel;
    }
}