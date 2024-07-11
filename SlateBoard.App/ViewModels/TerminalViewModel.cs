using System.Drawing;
using Caliburn.Micro;
using SlateBoard.App.Enum;
using SlateBoard.App.Events;
using SlateBoard.App.Interface;

namespace SlateBoard.App.ViewModels;

public class TerminalViewModel : PropertyChangedBase, ITerminal
{
    private readonly IEventAggregator _events;
    public int Height { get; set; }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public int Width { get; set; }

    public string Title;
    public int GridSize = 100;

    private int _x;

    public int X
    {
        get => _x;
        set
        {
            _x = value;
            NotifyOfPropertyChange(nameof(X));
            Events.PublishOnBackgroundThreadAsync(new TerminalMovedEvent(this));
        }
    }

    private int _y;

    public int Y
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

  
        TestInit();
    }

    private void TestInit()
    {
        Height = 80;
        Width = 50;
       
        InputSockets.Add(new SocketViewModel(0, 0, this, Events, SocketTypeEnum.Input){Label = "Input 1", Slate = this});
        InputSockets.Add(new SocketViewModel(0, 0, this, Events, SocketTypeEnum.Input){ Label = "Input 2", Slate = this });
        InputSockets.Add(new SocketViewModel(0, 0, this, Events, SocketTypeEnum.Input){Label = "Input 3", Slate = this });
        OutputSockets.Add( new SocketViewModel(0, 0, this, Events, SocketTypeEnum.Output){Label = "Output 1", Slate = this });
        OutputSockets.Add( new SocketViewModel(0, 0, this, Events, SocketTypeEnum.Output){Label = "Output 2", Slate = this });
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


}