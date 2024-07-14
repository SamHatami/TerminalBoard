using System.Diagnostics;
using System.Drawing;
using Caliburn.Micro;
using SlateBoard.App.Enum;
using SlateBoard.App.Events;
using SlateBoard.App.Interface.ViewModel;

namespace SlateBoard.App.ViewModels;

public class TerminalViewModel : PropertyChangedBase, ITerminal, IHandle<CanvasZoomPanEvent>
{
    private readonly IEventAggregator _events;
    public int Height { get; set; }
    public Guid Id { get; }
    public int Width { get; set; }

    public string Title;
    public int GridSize = 100;

    private double _x;

    public double X
    {
        get => _x;
        set
        {
            _x = value;
            NotifyOfPropertyChange(nameof(X));
            Events.PublishOnBackgroundThreadAsync(new TerminalMovedEvent(this));
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
            Events.PublishOnBackgroundThreadAsync(new TerminalMovedEvent(this));
        }
    }

    public List<IWire> Wires { get; set; } = new List<IWire>();
    public IEventAggregator Events { get; }

    public BindableCollection<ISocket> InputSockets { get; set; } = new BindableCollection<ISocket>();
    public BindableCollection<ISocket> OutputSockets { get; set; } = new BindableCollection<ISocket>();
    public List<ISocket> InputSocket { get; set; } 
    public List<ISocket> OutputSocket { get; set; }
    public List<ITerminal> Connectors { get; set; } = new List<ITerminal>();

    public TerminalViewModel(IEventAggregator events)
    {
        Events = events;
        Events.SubscribeOnBackgroundThread(this);
        Id = Guid.NewGuid();

  
        TestInit();
    }

    private void TestInit()
    {
        Height = 80;
        Width = 50;
       
        InputSockets.Add(new SocketViewModel( this, Events, SocketTypeEnum.Input){Label = "Input 1", Slate = this});
        InputSockets.Add(new SocketViewModel(this, Events, SocketTypeEnum.Input){ Label = "Input 2", Slate = this });
        InputSockets.Add(new SocketViewModel(this, Events, SocketTypeEnum.Input){Label = "Input 3", Slate = this });
        OutputSockets.Add( new SocketViewModel(this, Events, SocketTypeEnum.Output){Label = "Output 1", Slate = this });
        OutputSockets.Add( new SocketViewModel(this, Events, SocketTypeEnum.Output){Label = "Output 2", Slate = this });
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

        X += dx; Y += dy;

        Trace.WriteLine("X: " +X + " Y: " + Y);
        return Task.CompletedTask;

    }
}