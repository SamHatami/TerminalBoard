using SlateBoard.App.ViewModels;

namespace SlateBoard.App.Events;

public class TerminalMovedEvent
{
    public TerminalViewModel TerminalViewModel { get; set; }
    public TerminalMovedEvent(TerminalViewModel viewModel)
    {
        TerminalViewModel = viewModel;
    }
}