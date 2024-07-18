using Caliburn.Micro;
using TerminalBoard.App.Events;
using System.Windows;
using System.Windows.Input;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interface.ViewModel;

namespace TerminalBoard.App.ViewModels;

public class WireViewModel : PropertyChangedBase, IWire, IHandle<SelectItemEvent>
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

    public ISocket? StartSocket { get; set; }
    public ISocket? EndSocket { get; set; }
    public ITerminal InputTerminal { get; set; }
    public ITerminal OutputTerminal { get; set; }
    public WireTypeEnum WireType { get; set; }

    public void Delete(KeyEventArgs keyEvent)
    {
        if (Selected && keyEvent.Key == Key.Delete)
            _events.PublishOnBackgroundThreadAsync(new RemoveConnectionEvent(this));
    }
    
    public Guid Id { get; set; } = Guid.NewGuid();

    private readonly IEventAggregator _events;

    public WireViewModel(ISocket startSocket, ISocket endSocket, IEventAggregator events)
    {
        _events = events;
        _events.SubscribeOnBackgroundThread(this);
        SetStartSocket(startSocket);
        SetEndSocket(endSocket);
    }

    public void SetStartSocket(ISocket socket)
    {
        StartSocket = socket;
        SetStartPosition(socket);
    }

    public void SetEndSocket(ISocket socket)
    {
        EndSocket = socket;
        SetEndPosition(socket);
    }

    private void SetEndPosition(ISocket socket)
    {
        _endPoint.X = socket.X;
        _endPoint.Y = socket.Y;
        EndPoint = _endPoint;

        _endExtensionPoint.Y = EndPoint.Y;
        _endExtensionPoint.X = EndPoint.X - 25;
        EndExtensionPoint = _endExtensionPoint;
    }

    private void SetStartPosition(ISocket socket)
    {
        _startPoint.X = socket.X;
        _startPoint.Y = socket.Y;
        StartPoint = _startPoint;

        _startExtensionPoint.Y = StartPoint.Y;
        _startExtensionPoint.X = StartPoint.X + 25;
        StartExtensionPoint = _startExtensionPoint;
    }

    public void UpdatePosition(ISocket socket)
    {
        if (socket.Id != StartSocket.Id && socket.Id != EndSocket.Id)
            return;

        if (StartSocket != null && socket.Id == StartSocket.Id)
            SetStartPosition(socket);

        if (EndSocket != null && socket.Id == EndSocket.Id)
            SetEndPosition(socket);
    }

    public Task HandleAsync(SelectItemEvent message, CancellationToken cancellationToken)
    {
        if(message.Item != this) 
            Selected = false;
        return Task.CompletedTask;
    }
}