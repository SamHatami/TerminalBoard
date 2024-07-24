

using TerminalBoard.App.Interfaces.ViewModels;

namespace TerminalBoard.App.Events.UIEvents;

public class SelectItemEvent(ISelectable item)
{
    public ISelectable Item { get; set; } = item;
}