using System.Security.Cryptography;
using Caliburn.Micro;
using SlateBoard.App.Enum;
using SlateBoard.App.Events;
using SlateBoard.App.Interface;

namespace SlateBoard.App.ViewModels;

public class MainViewModel : Screen
{
    private readonly IEventAggregator _events;
    private bool grid = false;

    public BindableCollection<IMoveableItem> MoveableItems { get; set; }

    public MainViewModel(IEventAggregator events)
    {
        _events = events;
        _events.SubscribeOnBackgroundThread(this);

        TempInit();
    }

    private void TempInit()
    {
        MoveableItems = new BindableCollection<IMoveableItem>();
        MoveableItems.Add(new SlateViewModel(_events) { X = 50, Y = 50, Height = 80 , Width = 60});
        MoveableItems.Add(new SlateViewModel(_events) { X = 120, Y = 30, Height = 60, Width = 60 });
    }

    public void AddItem() //Future arguments for type or just getting the type directly
    {
        MoveableItems.Add(new SlateViewModel(_events));
    }

    public void Snap()
    {
        grid = !grid;
        _events.PublishOnBackgroundThreadAsync(new GridChangeEvent(grid, 15, GridTypeEnum.Dots));
    }
}