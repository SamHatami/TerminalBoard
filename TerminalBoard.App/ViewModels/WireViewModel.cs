using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interfaces.Terminals;
using TerminalBoard.App.Interfaces.ViewModels;

namespace TerminalBoard.App.ViewModels;

public class WireViewModel : PropertyChangedBase, IWireViewModel, IHandle<SelectItemEvent>
{
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
        _endExtensionPoint.X = EndPoint.X - 25;
        EndExtensionPoint = _endExtensionPoint;
    }

    private void SetStartPosition(ISocketViewModel socketViewModel)
    {
        _startPoint.X = socketViewModel.X;
        _startPoint.Y = socketViewModel.Y;
        StartPoint = _startPoint;

        _startExtensionPoint.Y = StartPoint.Y;
        _startExtensionPoint.X = StartPoint.X + 25;
        StartExtensionPoint = _startExtensionPoint;
    }

    public void UpdatePosition(ISocketViewModel socketViewModel)
    {
        if (socketViewModel.Id != StartSocketViewModel.Id && socketViewModel.Id != EndSocketViewModel.Id)
            return;

        if (StartSocketViewModel != null && socketViewModel.Id == StartSocketViewModel.Id)
            SetStartPosition(socketViewModel);

        if (EndSocketViewModel != null && socketViewModel.Id == EndSocketViewModel.Id)
            SetEndPosition(socketViewModel);
    }

    public Task HandleAsync(SelectItemEvent message, CancellationToken cancellationToken)
    {
        if (message.Item != this)
            Selected = false;
        return Task.CompletedTask;
    }
}