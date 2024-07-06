using SlateBoard.App.ViewModels;

namespace SlateBoard.App.Events;

public class SlateMovedEvent
{
    public SlateViewModel SlateViewModel { get; set; }
    public SlateMovedEvent(SlateViewModel viewModel)
    {
        SlateViewModel = viewModel;
    }
}