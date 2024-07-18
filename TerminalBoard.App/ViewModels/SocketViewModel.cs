using System.Windows;
using Caliburn.Micro;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interface.ViewModel;

namespace TerminalBoard.App.ViewModels;

public class SocketViewModel : PropertyChangedBase, ISocket, IHandle<TerminalMovedEvent>, IHandle<TerminalRemovedEvent>
{
    public SocketViewModel(ITerminal parent, IEventAggregator events, SocketTypeEnum type)
    {
        ParentTerminal = parent;
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

    public Guid Id { get; } = Guid.NewGuid();
    public SocketTypeEnum Type { get; set; }
    public ITerminal ParentTerminal { get; set; }
    public IEventAggregator Events { get; set; }

    public void SetRelativeDistances(Vector v)
    {
        relativeDistanceToParent_X = v.X;
        relativeDistanceToParent_Y = v.Y;
    }


    public Task HandleAsync(TerminalMovedEvent message, CancellationToken cancellationToken)
    {
        if (message.TerminalViewModel == ParentTerminal)
        {
            _x = message.TerminalViewModel.X + relativeDistanceToParent_X;
            _y = message.TerminalViewModel.Y + relativeDistanceToParent_Y;
        }

        Events.PublishOnBackgroundThreadAsync(new SocketMovedEvent(this));
        return Task.CompletedTask;

    }

    public Task HandleAsync(TerminalRemovedEvent message, CancellationToken cancellationToken)
    {
        //if(message.Terminal != Parent)

        return Task.CompletedTask;
    }
}