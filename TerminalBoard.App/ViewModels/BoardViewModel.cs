﻿using Caliburn.Micro;
using System.Windows.Input;
using TerminalBoard.App.Events.UIEvents;
using TerminalBoard.App.Interfaces.ViewModels;
using TerminalBoard.App.Views;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Functions.Math;
using TerminalBoard.Core.Services;
using TerminalBoard.Core.Terminals;
using TerminalBoard.Core.Wires;

namespace TerminalBoard.App.ViewModels;

//TODO: Probably need a shellView as conductor

/// <summary>
/// THe Main ViewModel for the board that holds all other viewModels and handles several major events.
/// </summary>
public class BoardViewModel : Screen, IHandle<AddConnectionEvent>, IHandle<RemoveConnectionEvent>,
    IHandle<SelectItemEvent>, IHandle<ClearSelectionEvent>
{
    private readonly IEventAggregator _events;
    private readonly WireService _wireService;
    private readonly TerminalService _terminalService;
    private bool _grid = false;

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
        _events.PublishOnBackgroundThreadAsync(new GridChangeEvent(_grid, 15, GridTypeEnum.Dots));
    }

    public void AddTerminal() //Future arguments for type or just getting the type directly
    {
        //TODO: Add terminalcreator
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
        var multiplier = new Multiplication();
        var evaluationTerminal = new EvaluationTerminal(multiplier);
        var terminalViewModel = new TerminalViewModel(_events, evaluationTerminal) { CanvasPositionY = 100, CanvasPositionX = 100 }; ;

        TerminalViewModels.Add(terminalViewModel);
    }

    public void AddOutputTerminal()
    {
        var terminalViewModel = new TerminalViewModel(_events, new SimpleOutputTerminal()) { CanvasPositionY = 100, CanvasPositionX = 100 }; ;
        TerminalViewModels.Add(terminalViewModel);
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
        var selectedTerminal = TerminalViewModels.SingleOrDefault(t => t.Selected);
        if (selectedTerminal != null)
        {
            foreach (var wire in selectedTerminal.WireViewModels)
            {
                WireViewModels.Remove(wire);
            }

            TerminalViewModels.Remove(selectedTerminal);

            _events.PublishOnBackgroundThreadAsync(new TerminalRemovedEvent(selectedTerminal));
        }
    }

    private void RemoveSelectedWire()
    {
        var selectedWire = WireViewModels.SingleOrDefault(w => w.Selected);
        if (selectedWire != null)
        {
            WireViewModels.Remove(selectedWire);
            _events.PublishOnBackgroundThreadAsync(new WireRemovedEvent(selectedWire));
        }
    }

    public Task HandleAsync(AddConnectionEvent message, CancellationToken cancellationToken)
    {
        var newWire = message.Wire;

        WireViewModels.Add(newWire);

        _wireService.ConnectSockets(newWire.StartSocketViewModel.Socket, newWire.EndSocketViewModel.Socket);

        WireConnection newConnection = new WireConnection(newWire.StartSocketViewModel.Socket,
            newWire.EndSocketViewModel.Socket, new TypedValue<float>("", Guid.NewGuid()){Value = 1.0f});

        newWire.InputTerminal.Connections.Add(newConnection);
        newWire.OutputTerminal.Connections.Add(newConnection);

        TerminalViewModels.SingleOrDefault(T => T.Terminal == newWire.InputTerminal).WireViewModels.Add(newWire);
        TerminalViewModels.SingleOrDefault(T => T.Terminal == newWire.OutputTerminal).WireViewModels.Add(newWire);


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
        if (WireViewModels.Any(w => w.Selected))
        {
            var selected = WireViewModels.Single(w => w.Selected);
            selected.Selected = false;
        }

        if (TerminalViewModels.Any(t => t.Selected))
            foreach (var terminal in TerminalViewModels)
                terminal.Selected = false;

        SelectedItem = null;
    }
}