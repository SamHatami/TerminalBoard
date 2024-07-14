using Caliburn.Micro;
using SlateBoard.App.Enum;
using SlateBoard.App.Events;
using SlateBoard.App.Interface.ViewModel;

namespace SlateBoard.App.ViewModels;

//TODO: Probably need a shellView as conductor
public class MainViewModel : Screen, IHandle<AddConnectionEvent>
{
    public IEventAggregator Events;
    private bool grid = false;

    public BindableCollection<ITerminal> MoveableItems { get; set; } 
    public BindableCollection<IWire> Wires { get; set; } = [];

    private IWire _selectedWire;
    public IWire SelectedWire 
    { 
        get =>  _selectedWire;
        set 
        {
            _selectedWire = value;
            NotifyOfPropertyChange(nameof(SelectedWire));
        }
    }

    public MainViewModel(IEventAggregator events)
    {
        Events = events;
        Events.SubscribeOnBackgroundThread(this);

        TempInit();
    }

    private void TempInit()
    {
        MoveableItems = new BindableCollection<ITerminal>();
        MoveableItems.Add(new TerminalViewModel(Events) { X = 50, Y = 50, Height = 200, Width = 200 });
        MoveableItems.Add(new TerminalViewModel(Events) { X = 120, Y = 30, Height = 200, Width = 200 });
    }

    public void AddItem() //Future arguments for type or just getting the type directly
    {
        MoveableItems.Add(new TerminalViewModel(Events));
    }

    public void Snap()
    {
        grid = !grid;
        Events.PublishOnBackgroundThreadAsync(new GridChangeEvent(grid, 15, GridTypeEnum.Dots));
    }
    
    public Task HandleAsync(AddConnectionEvent message, CancellationToken cancellationToken)
    {
        var newWire = message.Wire;

        Wires.Add(newWire);

        //Create connection between slates

        newWire.InputTerminal.InputSocket.Add(newWire.StartSocket); //TODO: Handle with addconnector
        newWire.InputTerminal.Connectors.Add(newWire.OutputTerminal);
        newWire.OutputTerminal.Connectors.Add(newWire.InputTerminal);
        newWire.OutputTerminal.Connectors.Add(newWire.OutputTerminal);

        return Task.CompletedTask;
    }
}