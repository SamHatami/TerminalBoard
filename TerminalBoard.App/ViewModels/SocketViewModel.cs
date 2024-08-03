
using System.Windows;
using Caliburn.Micro;
using TerminalBoard.App.Events.UIEvents;
using TerminalBoard.App.Interfaces.ViewModels;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.App.ViewModels;

public class SocketViewModel : PropertyChangedBase, ISocketViewModel, IHandle<TerminalRemovedEvent>, IHandle<WireRemovedEvent>
{
    #region Fields

    private bool _isConnected;
    
    private double _relativeDistanceToParentX; //From Code behind in View

    private double _relativeDistanceToParentY; //From Code behind in View

    #endregion Fields

    #region Properties

    public ISocket Socket { get; }
    public IEventAggregator Events { get; set; }

    public Guid Id { get; } = Guid.NewGuid();

    public bool IsConnected
    {
        get => _isConnected;
        set
        {
            _isConnected = value;
            NotifyOfPropertyChange(nameof(IsConnected));
        }
    }
    public string Label { get; set; }

    public ITerminalViewModel ParentViewModel { get; set; }

    public SocketTypeEnum Type { get; set; }

    public List<IWireViewModel> Wires { get; } = [];

    private double _x;
    public double X
    {
        get => _x;
        set
        {
            _x = value;
            NotifyOfPropertyChange(nameof(X));
            if(Wires.Count > 0) NotifyWires();
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
            if (Wires.Count > 0) NotifyWires();
        }
    }

    #endregion Properties

    #region Constructors

    public SocketViewModel(ITerminalViewModel parent, IEventAggregator events, SocketTypeEnum type, ISocket socket)
    {
        ParentViewModel = parent;
        Events = events;
        Type = type;
        Socket = socket;
        Events.SubscribeOnBackgroundThread(this);
    }

    #endregion Constructors

    #region Methods

    public Task HandleAsync(TerminalRemovedEvent message, CancellationToken cancellationToken)
    {
        //if(message.Terminal != Parent)

        return Task.CompletedTask;
    }

    public void AddWire(IWireViewModel wire)
    {
        if(Wires.Contains(wire)) return;

        Wires.Add(wire);
        IsConnected = true;
    }
    public void SetRelativeDistances(Vector v)
    {
        _relativeDistanceToParentX = v.X;
        _relativeDistanceToParentY = v.Y;
    }



    public void UpdatePosition()
    {
        _x = ParentViewModel.CanvasPositionX + _relativeDistanceToParentX;
        _y = ParentViewModel.CanvasPositionY + _relativeDistanceToParentY;

        if (Wires.Count > 0)
            NotifyWires();
    }

    private void NotifyWires()
    {
        foreach (var wire in Wires) wire.UpdatePosition(this);
    }

    #endregion Methods

    public Task HandleAsync(WireRemovedEvent message, CancellationToken cancellationToken)
    {
        if(Wires.Contains(message.Wire)) 
            Wires.Remove(message.Wire);

        if(Wires.Count == 0)
            IsConnected = false;
            
        return Task.CompletedTask;
    }
}