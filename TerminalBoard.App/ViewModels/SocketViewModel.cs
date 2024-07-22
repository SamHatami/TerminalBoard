using Caliburn.Micro;
using System.Windows;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interfaces.ViewModels;

namespace TerminalBoard.App.ViewModels;

public class SocketViewModel : PropertyChangedBase, ISocketViewModel, IHandle<TerminalRemovedEvent>
{
    #region Fields

    private bool _isConnected;
    
    private double _relativeDistanceToParentX; //From Code behind in View

    private double _relativeDistanceToParentY; //From Code behind in View

    #endregion Fields

    #region Properties

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

    public List<IWire> Wires { get; set; } = [];
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

    #endregion Properties

    #region Constructors

    public SocketViewModel(ITerminalViewModel parent, IEventAggregator events, SocketTypeEnum type)
    {
        ParentViewModel = parent;
        Events = events;
        Type = type;
        Events.SubscribeOnBackgroundThread(this);
    }

    #endregion Constructors

    #region Methods

    public Task HandleAsync(TerminalRemovedEvent message, CancellationToken cancellationToken)
    {
        //if(message.Terminal != Parent)

        return Task.CompletedTask;
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

        NotifyWires();
    }

    private void NotifyWires()
    {
        foreach (var wire in Wires) wire.UpdatePosition(this);
    }

    #endregion Methods
}