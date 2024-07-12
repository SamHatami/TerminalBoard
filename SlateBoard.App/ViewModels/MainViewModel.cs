using Caliburn.Micro;
using SlateBoard.App.Enum;
using SlateBoard.App.Events;
using SlateBoard.App.Interface;

namespace SlateBoard.App.ViewModels;

//TODO: Probably need a shellView as conductor
public class MainViewModel : Screen, IHandle<AddConnectionEvent>
{
    private readonly IEventAggregator _events;
    private bool grid = false;

    public BindableCollection<ITerminal> MoveableItems { get; set; } 
    public BindableCollection<IWire> Wires { get; set; } = [];

    public MainViewModel(IEventAggregator events)
    {
        _events = events;
        _events.SubscribeOnBackgroundThread(this);

        TempInit();
    }

    private void TempInit()
    {
        MoveableItems = new BindableCollection<ITerminal>();
        MoveableItems.Add(new TerminalViewModel(_events) { X = 50, Y = 50, Height = 200, Width = 200 });
        MoveableItems.Add(new TerminalViewModel(_events) { X = 120, Y = 30, Height = 200, Width = 200 });
    }

    public void AddItem() //Future arguments for type or just getting the type directly
    {
        MoveableItems.Add(new TerminalViewModel(_events));
    }

    public void Snap()
    {
        grid = !grid;
        _events.PublishOnBackgroundThreadAsync(new GridChangeEvent(grid, 15, GridTypeEnum.Dots));
    }
    
    public Task HandleAsync(AddConnectionEvent message, CancellationToken cancellationToken)
    {
        var newWire = message.wire;

        Wires.Add(newWire);

        //Create connection between slates

        newWire.InputTerminal.InputSocket.Add(newWire.StartSocket); //TODO: Handle with addconnector
        newWire.InputTerminal.Connectors.Add(newWire.OutputTerminal);
        newWire.OutputTerminal.Connectors.Add(newWire.InputTerminal);
        newWire.OutputTerminal.Connectors.Add(newWire.OutputTerminal);

        return Task.CompletedTask;
    }
}