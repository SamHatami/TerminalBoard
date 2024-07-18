using TerminalBoard.App.Interface.ViewModel;

namespace TerminalBoard.App.Events
{
    public class SelectItemEvent(ISelectable item)
    {
        public ISelectable Item { get; set; } = item;
    }
}