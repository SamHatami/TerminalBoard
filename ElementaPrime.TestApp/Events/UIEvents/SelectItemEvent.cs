

using ElementaPrime.TestApp.Interfaces.ViewModels;

namespace ElementaPrime.TestApp.Events.UIEvents;

public class SelectItemEvent(ISelectable item)
{
    public ISelectable Item { get; set; } = item;
}