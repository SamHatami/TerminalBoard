using SlateBoard.App.Interface.ViewModel;

namespace SlateBoard.App.Events
{
    public class SelectItemEvent(ISelectable item)
    {
        public ISelectable Item { get; set; } = item;
    }
}