using Caliburn.Micro;
using System.Windows.Input;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Events;
using TerminalBoard.App.Functions.Math;
using TerminalBoard.App.Interfaces.ViewModels;
using TerminalBoard.App.Terminals;

namespace TerminalBoard.App.ViewModels;

//TODO: Probably need a shellView as conductor
public class MainViewModel : Screen, IHandle<AddConnectionEvent>, IHandle<RemoveConnectionEvent>,
    IHandle<SelectItemEvent>, IHandle<ClearSelectionEvent>
{

    private readonly IEventAggregator _events;
    private bool _grid = false;

    public BindableCollection<ITerminalViewModel> Terminals { get; set; }
    public BindableCollection<IWire> Wires { get; set; } = [];

    private ISelectable? _selectedItem;

    public ISelectable? SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            NotifyOfPropertyChange(nameof(SelectedItem));
        }
    }

    public MainViewModel(IEventAggregator events)
    {
        _events = events;
        _events.SubscribeOnBackgroundThread(this);

        TempInit();
    }

    private void TempInit()
    {
        Terminals = [];
    }

    public void Snap()
    {
        _grid = !_grid;
        _events.PublishOnBackgroundThreadAsync(new GridChangeEvent(_grid, 15, GridTypeEnum.Dots));
    }

    public void AddTerminal() //Future arguments for type or just getting the type directly
    {
        //TODO: Add terminalcreator
    }

    public void AddFloatTerminal()
    {
        var floatTerminal = new FloatValueTerminal();
        var terminalViewModel = new TerminalViewModel(_events, floatTerminal){CanvasPositionY = 50, CanvasPositionX = 50};
        Terminals.Add(terminalViewModel);
    }

    public void AddMultiplyTerminal()
    {
        var multiplier = new Multiplication<float>();
    }

    public void FutureAddTerminal(string functionName) //Future arguments for type or just getting the type directly
    {
    }

    public void RemoveItem(ActionExecutionContext context)
    {
        if (context.EventArgs is KeyEventArgs keysArgs && keysArgs.Key != Key.Delete)
            return;

        RemoveSelectedTerminal();
        RemoveSelectedWire();
    }

    private void RemoveSelectedTerminal()
    {
        var selectedTerminal = Terminals.SingleOrDefault(t => t.Selected);
        if (selectedTerminal != null)
        {
            Terminals.Remove(selectedTerminal);
            _events.PublishOnBackgroundThreadAsync(new TerminalRemovedEvent(selectedTerminal));
        }
    }

    private void RemoveSelectedWire()
    {
        var selectedWire = Wires.SingleOrDefault(w => w.Selected);
        if (selectedWire != null)
        {
            Wires.Remove(selectedWire);
            _events.PublishOnBackgroundThreadAsync(new WireRemovedEvent(selectedWire));
        }
    }

    public Task HandleAsync(AddConnectionEvent message, CancellationToken cancellationToken)
    {
        var newWire = message.Wire;

        Wires.Add(newWire);

        //Todo: add connectors in terminalviewmodels

        return Task.CompletedTask;
    }

    public Task HandleAsync(RemoveConnectionEvent message, CancellationToken cancellationToken)
    {
        Wires.Remove(message.Wire);

        return Task.CompletedTask;
    }

    public Task HandleAsync(SelectItemEvent message, CancellationToken cancellationToken)
    {
        if (SelectedItem == null) return Task.CompletedTask;

        ClearSelection();

        SelectedItem = message.Item;

        return Task.CompletedTask;
    }

    public Task HandleAsync(ClearSelectionEvent message, CancellationToken cancellationToken)
    {
        ClearSelection();

        return Task.CompletedTask;
    }

    private void ClearSelection()
    {
        if (Wires.Any(w => w.Selected))
        {
            var selected = Wires.Single(w => w.Selected);
            selected.Selected = false;
        }

        if (Terminals.Any(t => t.Selected))
            foreach (var terminal in Terminals)
                terminal.Selected = false;

        SelectedItem = null;
    }
}