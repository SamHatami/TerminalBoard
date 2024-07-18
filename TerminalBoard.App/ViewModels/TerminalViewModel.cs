using Caliburn.Micro;
using TerminalBoard.App.Events;
using System.Diagnostics;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interface.ViewModel;

namespace TerminalBoard.App.ViewModels;

public class TerminalViewModel : PropertyChangedBase, ITerminal, IHandle<CanvasZoomPanEvent>, IHandle<SelectItemEvent>
{
    private readonly IEventAggregator _events;
    public int Height { get; set; }
    public Guid Id { get; }
    public int Width { get; set; }

    public string Title;
    public int GridSize = 100;

    private bool _selected;

    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            NotifyOfPropertyChange(nameof(Selected));
        }
    }

    private double _x;

    public double X
    {
        get => _x;
        set
        {
            _x = value;
            NotifyOfPropertyChange(nameof(X));
            NotifyWiresOfNewPosition();
        }
    }

    private double _y;

    public double Y
    {
        get => _y;
        set
        {
            _y = value;
            NotifyOfPropertyChange(nameof(Y));
            NotifyWiresOfNewPosition();
        }
    }

    public List<IWire> Wires { get; set; } = new();
    public IEventAggregator Events { get; }

    public BindableCollection<ISocket> InputSockets { get; set; } = [];
    public BindableCollection<ISocket> OutputSockets { get; set; } = [];
    public List<ISocket> InputSocket { get; set; } = [];
    public List<ISocket> OutputSocket { get; set; } = [];
    public List<ITerminal> Connectors { get; set; } = [];

    public TerminalViewModel(IEventAggregator events)
    {
        Events = events;
        Events.SubscribeOnBackgroundThread(this);
        Id = Guid.NewGuid();

        TestInit();
    }

    private void NotifyWiresOfNewPosition()
    {
        foreach (var socket in InputSockets)
            socket.UpdatePosition();

        foreach (var socket in OutputSockets)
            socket.UpdatePosition();
    }

    private void TestInit()
    {
        Height = 80;
        Width = 50;

        InputSockets.Add(new SocketViewModel(this, Events, SocketTypeEnum.Input)
            { Label = "Input 1", ParentTerminal = this });
        InputSockets.Add(new SocketViewModel(this, Events, SocketTypeEnum.Input)
            { Label = "Input 2", ParentTerminal = this });
        InputSockets.Add(new SocketViewModel(this, Events, SocketTypeEnum.Input)
            { Label = "Input 3", ParentTerminal = this });
        OutputSockets.Add(new SocketViewModel(this, Events, SocketTypeEnum.Output)
            { Label = "Output 1", ParentTerminal = this });
        OutputSockets.Add(new SocketViewModel(this, Events, SocketTypeEnum.Output)
            { Label = "Output 2", ParentTerminal = this });
    }

    public void Moved()
    {
    }

    public void Dropped()
    {
        throw new NotImplementedException();
    }

    public void AddWire(IWire wire)
    {
        Wires.Add(wire);
    }

    public void Connect()
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(CanvasZoomPanEvent message, CancellationToken cancellationToken)
    {
        var dx = message.dX;
        var dy = message.dY;

        X += dx;
        Y += dy;

        Trace.WriteLine("X: " + X + " Y: " + Y);
        return Task.CompletedTask;
    }

    public Task HandleAsync(SelectItemEvent message, CancellationToken cancellationToken)
    {
        if (message.Item != this)
            Selected = false;
        return Task.CompletedTask;
    }
}