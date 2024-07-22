using ISelectable = TerminalBoard.App.Interfaces.ViewModels.ISelectable;

namespace TerminalBoard.App.Events;

public class SelectItemEvent(ISelectable item)
{
    public ISelectable Item { get; set; } = item;
}