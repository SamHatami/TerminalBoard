using System.Windows;
using Caliburn.Micro;
using SlateBoard.App.Enum;
using SlateBoard.App.Events;
using SlateBoard.App.Interface;

namespace SlateBoard.App.ViewModels;

public class SocketViewModel: PropertyChangedBase, ISocket, IHandle<TerminalMovedEvent>
{
    public SocketViewModel(ITerminal parent, IEventAggregator events, SocketTypeEnum type)
    {
        Parent = parent;
        Events = events;
        Type = type;



        Events.SubscribeOnBackgroundThread(this);
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
        }
    }

    public Guid Id { get;} = Guid.NewGuid(); 
    public ITerminal Slate { get; set; }
    public SocketTypeEnum Type { get; set; }
    public ITerminal Parent { get; set; }
    public IEventAggregator Events { get; set; }

    public void SetRelativeDistances(Vector v)
    {
        relativeDistanceToParent_X = v.X;
        relativeDistanceToParent_Y = v.Y;
    }


    public Task HandleAsync(TerminalMovedEvent message, CancellationToken cancellationToken)
    {
        if(message.TerminalViewModel == Parent)
        {
            _x = message.TerminalViewModel.X + relativeDistanceToParent_X;
            _y = message.TerminalViewModel.Y + relativeDistanceToParent_Y;
        }

        Events.PublishOnBackgroundThreadAsync(new SocketMovedEvent(this));
        return Task.CompletedTask;
        
    }
}