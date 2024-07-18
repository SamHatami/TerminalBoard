using System.Windows;
using Caliburn.Micro;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interface.ViewModel;

namespace TerminalBoard.App.ViewModels;

public class SocketViewModel : PropertyChangedBase, ISocket, IHandle<TerminalRemovedEvent>
{
    public SocketViewModel(ITerminal parent, IEventAggregator events, SocketTypeEnum type)
    {
        ParentTerminal = parent;
        Events = events;
        Type = type;
        Events.SubscribeOnBackgroundThread(this);
    }
    
    private bool _isConnected;
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

    private double relativeDistanceToParent_X; //From Code behind in View
    private double relativeDistanceToParent_Y; //From Code Behind in View

    private double _x;
    public double X
    {
        get => _x;
        set
        {
            _x = value;
            NotifyOfPropertyChange(nameof(X));
            NotifyWires();
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
            NotifyWires();
        }
    }

    public Guid Id { get; } = Guid.NewGuid();
    public List<IWire> Wires { get; set; } = [];
    public SocketTypeEnum Type { get; set; }
    public ITerminal ParentTerminal { get; set; }
    public IEventAggregator Events { get; set; }
    public void UpdatePosition()
    {
        _x = ParentTerminal.X + relativeDistanceToParent_X;
        _y = ParentTerminal.Y + relativeDistanceToParent_Y;

        NotifyWires();
    }

    private void NotifyWires()
    {
        foreach (var wire in Wires)
        {
            wire.UpdatePosition(this);
        }
    }

    public void SetRelativeDistances(Vector v)
    {
        relativeDistanceToParent_X = v.X;
        relativeDistanceToParent_Y = v.Y;
    }


    //public Task HandleAsync(TerminalMovedEvent message, CancellationToken cancellationToken)
    //{
    //    if (message.TerminalViewModel == ParentTerminal)
    //    {
    //        _x = message.TerminalViewModel.X + relativeDistanceToParent_X;
    //        _y = message.TerminalViewModel.Y + relativeDistanceToParent_Y;
    //    }

    //    Events.PublishOnBackgroundThreadAsync(new SocketMovedEvent(this));
    //    return Task.CompletedTask;

    //}

    public Task HandleAsync(TerminalRemovedEvent message, CancellationToken cancellationToken)
    {
        //if(message.Terminal != Parent)

        return Task.CompletedTask;
    }

    
}