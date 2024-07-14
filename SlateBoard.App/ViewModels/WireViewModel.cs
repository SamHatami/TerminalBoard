using Caliburn.Micro;
using SlateBoard.App.Enum;
using System.Windows;
using SlateBoard.App.Events;
using System.Net.Sockets;
using SlateBoard.App.Interface.ViewModel;

namespace SlateBoard.App.ViewModels;

public class WireViewModel : PropertyChangedBase, IWire, IHandle<SocketMovedEvent>
{
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
    public ISocket StartSocket { get; set; }
    public ISocket  EndSocket { get; set; }
    public ITerminal InputTerminal { get; set; }
    public ITerminal OutputTerminal { get; set; }
    public WireTypeEnum WireType { get; set; }


    public Guid Id { get; set; } = Guid.NewGuid();

    private IEventAggregator _events;


    public WireViewModel(ISocket startSocket, ISocket endSocket, IEventAggregator events)
    {
;
        _events = events;
        _events.Subscribe(this);
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


    public void RemoveThis()
    {
        _events.PublishOnBackgroundThreadAsync(new RemoveConnectionEvent(this));
    }

    public Task HandleAsync(SocketMovedEvent message, CancellationToken cancellationToken)
    {
        var socket = message.Socket;

        if (socket.Id != StartSocket.Id && socket.Id != EndSocket.Id)
            return Task.CompletedTask;

        if (StartSocket != null && socket.Id == StartSocket.Id)
        {
            SetStartPosition(socket);
            return Task.CompletedTask;
        }

        SetEndPosition(socket);


        return Task.CompletedTask;
    }
}