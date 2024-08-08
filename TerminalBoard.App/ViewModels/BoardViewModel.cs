using System.Diagnostics;
using System.Windows;
using Caliburn.Micro;
using System.Windows.Input;
using TerminalBoard.App.Events.UIEvents;
using TerminalBoard.App.Interfaces.ViewModels;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Services;
using TerminalBoard.Core.Terminals;
using TerminalBoard.Core.Wires;
using TerminalBoard.Math.Operators;

namespace TerminalBoard.App.ViewModels;

//TODO: Probably need a shellView as conductor

/// <summary>
/// THe Main ViewModel for the board that holds all other viewModels and handles several major events.
/// </summary>
public class BoardViewModel : Screen, IHandle<AddConnectionEvent>, IHandle<RemoveConnectionEvent>,
    IHandle<SelectItemEvent>, IHandle<ClearSelectionEvent>, IHandle<SelectionBoxEvent>
{
    private readonly IEventAggregator _events;
    private readonly WireService _wireService;
    private readonly TerminalService _terminalService;
    private readonly List<ISelectable> _selectables = [];
    private bool _grid = false;
    public bool Grid
    {
        get => _grid;
        set
        {
            _grid = value;
            NotifyOfPropertyChange(nameof(Grid));
        }
    }

    private int _gridSpacing;

    public int GridSpacing
    {
        get => _gridSpacing;
        set
        {
            _gridSpacing = value;
            NotifyOfPropertyChange(nameof(GridSpacing));
        }
    }

    public BindableCollection<ITerminalViewModel> TerminalViewModels { get; set; }
    public BindableCollection<IWireViewModel> WireViewModels { get; set; } = [];

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

    public BoardViewModel(IEventAggregator events, WireService wireService, TerminalService terminalService)
    {
        _events = events;
        _wireService = wireService;
        _terminalService = terminalService;
        _events.SubscribeOnBackgroundThread(this);

        TempInit();
    }

    private void TempInit()
    {
        TerminalViewModels = [];
    }

    public void Snap()
    {
        _grid = !_grid;
        Grid = _grid;
        
        GridSpacing = 15;
        _events.PublishOnBackgroundThreadAsync(new GridChangeEvent(_grid, _gridSpacing, GridTypeEnum.Dots));
    }

    public void AddTerminal(TerminalType terminalType) //Future arguments for type or just getting the type directly
    {
        terminalType = TerminalType.Multiplication;
        var newTerminal = _terminalService.CreateTerminal(terminalType);
        var watch = Stopwatch.StartNew();

        watch.Start();
        TerminalViewModels.Add(new TerminalViewModel(_events, newTerminal)
            { CanvasPositionY = 100, CanvasPositionX = 100 });
        watch.Stop();

        var s = watch.ElapsedMilliseconds;
    }

    public void AddFloatTerminal()
    {
        var floatTerminal = new ValueTerminal<float>();
        var terminalViewModel = new TerminalViewModel(_events, floatTerminal)
            { CanvasPositionY = 100, CanvasPositionX = 100 };
        TerminalViewModels.Add(terminalViewModel);
    }

    public void AddMultiplyTerminal()
    {
        AddTerminal(TerminalType.Multiplication);
        //var multiplier = new Multiplication();
        //var evaluationTerminal = new EvaluationTerminal(multiplier);
        //var terminalViewModel = new TerminalViewModel(_events, evaluationTerminal)
        //{ CanvasPositionY = 100, CanvasPositionX = 100 };
        //;

        //TerminalViewModels.Add(terminalViewModel);
    }

    public void CreateTerminal(TerminalType terminal)
    {
        MessageBox.Show(terminal.ToString());
    }

    public void AddOutputTerminal()
    {
        var terminalViewModel = new TerminalViewModel(_events, new SimpleOutputTerminal())
            { CanvasPositionY = 100, CanvasPositionX = 100 };
        ;
        TerminalViewModels.Add(terminalViewModel);
    }

    public void FutureAddTerminal(string functionName) //Future arguments for type or just getting the type directly
    {
    }

    public void RemoveItem(ActionExecutionContext context)
    {
        if (context.EventArgs is KeyEventArgs keysArgs && keysArgs.Key != Key.Delete)
            return;

        RemoveSelectedTerminals();
        RemoveSelectedWires();
    }

    private void RemoveSelectedTerminals()
    {
        var selectedTerminals = TerminalViewModels.Where(t => t.Selected).ToArray();
        TerminalViewModels.RemoveRange(selectedTerminals);

        foreach (var selectedTerminal in selectedTerminals)
        {
            foreach (var wire in selectedTerminal.WireViewModels) WireViewModels.Remove(wire);
            selectedTerminal.Dispose(); //TODO Remove if undo is supported...
            _events.PublishOnBackgroundThreadAsync(new TerminalRemovedEvent(selectedTerminal));
        }
    }

    private void RemoveSelectedWires()
    {
        var selectedWires = WireViewModels.Where(w => w.Selected).ToArray();
        WireViewModels.RemoveRange(selectedWires);

        foreach (var selectedWire in selectedWires)
        {
            selectedWire.Dispose(); //TODO Remove if undo is supported...
            _events.PublishOnBackgroundThreadAsync(new WireRemovedEvent(selectedWire));
        }
    }

    public Task HandleAsync(AddConnectionEvent message, CancellationToken cancellationToken)
    {
        var start = message.Start;
        var end = message.End;

        if (!WireConnectionValidator.Validate(start.Socket, end.Socket)) return Task.CompletedTask;

        //Create WireViewModel and add to Board
        IWireViewModel newWire = new WireViewModel(start, end, _events); //TODO meh
        WireViewModels.Add(newWire);

        //Add to the SocketViewModels
        start.AddWire(newWire); //TODO use event after validation
        end.AddWire(newWire); //TODO use event after validation
        start.ParentViewModel.AddWireViewModel(newWire);
        end.ParentViewModel.AddWireViewModel(newWire);

        //Connect sockets
        _wireService.ConnectSockets(newWire.StartSocketViewModel.Socket, newWire.EndSocketViewModel.Socket);

        //TODO: Some type of refresh on the values from the connected wires

        return Task.CompletedTask;
    }

    public Task HandleAsync(RemoveConnectionEvent message, CancellationToken cancellationToken)
    {
        var wire = message.Wire;
        WireViewModels.Remove(message.Wire);

        var wireModel = wire.InputTerminal.Connections.SingleOrDefault(c => c.Id == wire.WireConnection.Id);
        wire.InputTerminal.Connections.Remove(wireModel);
        wire.OutputTerminal.Connections.Remove(wireModel);

        return Task.CompletedTask;
    }

    public Task HandleAsync(SelectItemEvent message, CancellationToken cancellationToken)
    {
        if (_selectables.Contains(message.Item) || message.Item == null) return Task.CompletedTask;

        ClearAndSelect(message.Item);

        return Task.CompletedTask;
    }

    public Task HandleAsync(ClearSelectionEvent message, CancellationToken cancellationToken)
    {
        ClearSelection();

        return Task.CompletedTask;
    }

    private void ClearAndSelect(ISelectable item)
    {
        for (int i = 0; i < _selectables.Count; i++)
        {
            if (_selectables[i] == null) continue;
            _selectables[i].Selected = false;
        }
        _selectables.Clear();
        _selectables.Add(item);
        item.Selected = true;
    }

    private void ClearSelection()
    {

        for (int i = 0; i < _selectables.Count; i++)
        {
            _selectables[i].Selected = false;
        }

        _selectables.Clear();
    }

    public Task HandleAsync(SelectionBoxEvent message, CancellationToken cancellationToken)
    {
        ClearSelection();
        foreach (var terminal in message.TerminalViewModels)
        {
            if(_selectables.Contains(terminal)) continue;
            terminal.Selected = true;
            _selectables.Add(terminal);
        }

        foreach (var wire in message.WireViewModels)
        {
            if(_selectables.Contains(wire)) continue;
            wire.Selected = true;
            _selectables.Add(wire);
        }

        return Task.CompletedTask;
    }
}