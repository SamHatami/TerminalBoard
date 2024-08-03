using System.Numerics;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;
using TerminalBoard.App.Events.UIEvents;
using TerminalBoard.App.Interfaces.ViewModels;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;
using Vector = System.Windows.Vector;

namespace TerminalBoard.App.ViewModels;

public class WireViewModel : PropertyChangedBase, IWireViewModel, IHandle<SelectItemEvent>
{
    private Vector xAxis = new Vector(1, 0);

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

    private Point _startPoint;

    public Point StartPoint
    {
        get => _startPoint;
        set
        {
            _startPoint = value;
            NotifyOfPropertyChange(nameof(StartPoint));
        }
    }

    private Point _endPoint;

    public Point EndPoint
    {
        get => _endPoint;
        set
        {
            _endPoint = value;
            NotifyOfPropertyChange(nameof(EndPoint));
        }
    }

    private Point _startExtensionPoint;

    public Point StartExtensionPoint
    {
        get => _startExtensionPoint;
        set
        {
            _startExtensionPoint = value;
            NotifyOfPropertyChange(nameof(StartExtensionPoint));
        }
    }

    private Point _endExtensionPoint;

    public Point EndExtensionPoint
    {
        get => _endExtensionPoint;
        set
        {
            _endExtensionPoint = value;
            NotifyOfPropertyChange(nameof(EndExtensionPoint));
        }
    }

    private Point _lowerControlPoint;

    public Point LowerControlPoint
    {
        get => _lowerControlPoint;
        set
        {
            _lowerControlPoint = value;
            NotifyOfPropertyChange(nameof(LowerControlPoint));
        }
    }


    private Point _upperControlPoint;

    public Point UpperControlPoint
    {
        get => _upperControlPoint;
        set
        {
            _upperControlPoint = value;
            NotifyOfPropertyChange(nameof(UpperControlPoint));
        }
    }

    private Point _midControlPoint;

    public Point MidControlPoint
    {
        get => _midControlPoint;
        set
        {
            _midControlPoint = value;
            NotifyOfPropertyChange(nameof(MidControlPoint));
        }
    }

    public ISocketViewModel? StartSocketViewModel { get; set; }
    public ISocketViewModel? EndSocketViewModel { get; set; }
    public ITerminal InputTerminal { get; set; }
    public ITerminal OutputTerminal { get; set; }
    public WireTypeEnum WireType { get; set; }
    public IWire WireConnection { get; set; }

    public void Delete(KeyEventArgs keyEvent)
    {
        if (Selected && keyEvent.Key == Key.Delete)
            _events.PublishOnBackgroundThreadAsync(new RemoveConnectionEvent(this));
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    private readonly IEventAggregator _events;

    public WireViewModel(ISocketViewModel startSocketViewModel, ISocketViewModel endSocketViewModel,
        IEventAggregator events)
    {
        _events = events;
        _events.SubscribeOnBackgroundThread(this);
        SetStartSocket(startSocketViewModel);
        SetEndSocket(endSocketViewModel);
        SetMidControlPoint();
        SetParentTerminals(startSocketViewModel, endSocketViewModel);
    }

    private void SetParentTerminals(ISocketViewModel start, ISocketViewModel end)
    {
        InputTerminal = start.ParentViewModel.Terminal;
        OutputTerminal = end.ParentViewModel.Terminal;
    }

    public void SetStartSocket(ISocketViewModel socketViewModel)
    {
        StartSocketViewModel = socketViewModel;
        SetStartPosition(socketViewModel);
    }

    public void SetEndSocket(ISocketViewModel socketViewModel)
    {
        EndSocketViewModel = socketViewModel;
        SetEndPosition(socketViewModel);
    }

    private void SetEndPosition(ISocketViewModel socketViewModel)
    {
        _endPoint.X = socketViewModel.X;
        _endPoint.Y = socketViewModel.Y;
        EndPoint = _endPoint;

        _endExtensionPoint.Y = EndPoint.Y;
        _endExtensionPoint.X = EndPoint.X - 15;
        EndExtensionPoint = _endExtensionPoint;

        _upperControlPoint.Y = _endExtensionPoint.Y;
        _upperControlPoint.X = _endExtensionPoint.X - 10;
        UpperControlPoint = _upperControlPoint;
    }

    private void SetStartPosition(ISocketViewModel socketViewModel)
    {
        _startPoint.X = socketViewModel.X;
        _startPoint.Y = socketViewModel.Y;
        StartPoint = _startPoint;

        _startExtensionPoint.Y = StartPoint.Y;
        _startExtensionPoint.X = StartPoint.X + 15;
        StartExtensionPoint = _startExtensionPoint;

        _lowerControlPoint.Y = _startExtensionPoint.Y;
        _lowerControlPoint.X = _startExtensionPoint.X + 10;
        LowerControlPoint = _lowerControlPoint;
    }

    private void SetMidControlPoint()
    {
        //Well...a bit overworked
        var distanceX = Math.Abs((_upperControlPoint.X - _lowerControlPoint.X) / 2);
        var distanceY = Math.Abs((_upperControlPoint.Y - _lowerControlPoint.Y) / 2);

        var length = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

        Vector directionalVector = new Vector(_upperControlPoint.X - _lowerControlPoint.X,
            _upperControlPoint.Y - _lowerControlPoint.Y);

        var angle = Vector.AngleBetween(xAxis, directionalVector)*(Math.PI/180);
        
        //midpoint behaves like a point on a circle
        _midControlPoint.X = length * Math.Cos(angle) + _lowerControlPoint.X;
        _midControlPoint.Y = length * Math.Sin(angle) + _lowerControlPoint.Y;

        MidControlPoint = _midControlPoint;
    }

    public void UpdatePosition(ISocketViewModel socketViewModel)
    {
        if (socketViewModel.Id != StartSocketViewModel.Id && socketViewModel.Id != EndSocketViewModel.Id)
            return;

        if (StartSocketViewModel != null && socketViewModel.Id == StartSocketViewModel.Id)
            SetStartPosition(socketViewModel);

        if (EndSocketViewModel != null && socketViewModel.Id == EndSocketViewModel.Id)
            SetEndPosition(socketViewModel);

        SetMidControlPoint();
    }

    public Task HandleAsync(SelectItemEvent message, CancellationToken cancellationToken)
    {
        if (message.Item != this)
            Selected = false;
        return Task.CompletedTask;
    }
}