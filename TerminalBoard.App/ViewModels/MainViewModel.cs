using System.Windows.Input;
using Caliburn.Micro;
using TerminalBoard.App.Events;
using TerminalBoard.App.ViewModels;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interface.ViewModel;
using System.Windows.Controls.Primitives;

namespace TerminalBoard.App.ViewModels;

//TODO: Probably need a shellView as conductor
public class MainViewModel : Screen, IHandle<AddConnectionEvent>, IHandle<RemoveConnectionEvent>,
    IHandle<SelectItemEvent>, IHandle<ClearSelectionEvent>
{
    public IEventAggregator Events;
    private bool grid = false;

    public BindableCollection<ITerminal> Terminals { get; set; }
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
        Events = events;
        Events.SubscribeOnBackgroundThread(this);

        TempInit();
    }

    private void TempInit()
    {
        Terminals = new BindableCollection<ITerminal>();

        Terminals.Add(new TerminalViewModel(Events) { X = 50, Y = 50, Height = 200, Width = 200 });
        Terminals.Add(new TerminalViewModel(Events) { X = 120, Y = 30, Height = 200, Width = 200 });
    }



    public void Snap()
    {
        grid = !grid;
        Events.PublishOnBackgroundThreadAsync(new GridChangeEvent(grid, 15, GridTypeEnum.Dots));
    }

    public void AddTerminal() //Future arguments for type or just getting the type directly
    {
        Terminals.Add(new TerminalViewModel(Events) { X = 50, Y = 50, Height = 200, Width = 200 });
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
            Events.PublishOnBackgroundThreadAsync(new TerminalRemovedEvent(selectedTerminal));
        }
    }

    private void RemoveSelectedWire()
    {
        var selectedWire = Wires.SingleOrDefault(w => w.Selected);
        if (selectedWire != null)
        {
            Wires.Remove(selectedWire);
            Events.PublishOnBackgroundThreadAsync(new WireRemovedEvent(selectedWire));
        }
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
        if(Terminals.Any(t => t.Selected))
        {
            foreach (var terminal in Terminals)
            {
                terminal.Selected = false;
            }
        }

        SelectedItem = null;
    }
}