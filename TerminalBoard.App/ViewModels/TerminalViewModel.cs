using Caliburn.Micro;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interfaces;
using TerminalBoard.App.Interfaces.Functions;
using TerminalBoard.App.Interfaces.ViewModels;

namespace TerminalBoard.App.ViewModels;

public class TerminalViewModel : PropertyChangedBase, ITerminalViewModel, IHandle<CanvasZoomPanEvent>,
    IHandle<SelectItemEvent>
{
    #region Fields

    private readonly IEventAggregator _events;
    private bool _selected;
    private double _canvasPositionX;
    private double _canvasPositionY;

    #endregion Fields

    #region Properties

    public ITerminal Terminal { get; }
    public List<ITerminal> Connectors { get; set; } = [];
    public int Height { get; set; }
    public Guid Id { get; }
    public BindableCollection<ISocketViewModel> InputSockets { get; set; } = [];
    public BindableCollection<ISocketViewModel> OutputSockets { get; set; } = [];

    public bool GetInputValue { get; private set; }

    private string _inputValue;
    public string InputValue
    {
        get => _inputValue;
        set
        {
            _inputValue = value;
            NotifyOfPropertyChange(nameof(InputValue));
            SetInputValue(InputValue);
        }
    }

    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            NotifyOfPropertyChange(nameof(Selected));
        }
    }

    public int Width { get; set; }
    public List<IWire> Wires { get; set; } = new();

    public double CanvasPositionX //kind of anti-pattern.
    {
        get => _canvasPositionX;
        set
        {
            _canvasPositionX = value;
            NotifyOfPropertyChange(nameof(CanvasPositionX));
            NotifyWiresOfNewPosition();
        }
    }

    public double CanvasPositionY
    {
        get => _canvasPositionY;
        set
        {
            _canvasPositionY = value;
            NotifyOfPropertyChange(nameof(CanvasPositionY));
            NotifyWiresOfNewPosition();
        }
    }

    #endregion Properties

    #region Constructors

    public TerminalViewModel(IEventAggregator events, ITerminal terminal)
    {
        _events = events;
        Terminal = terminal;
        _events.SubscribeOnBackgroundThread(this);
        Id = Guid.NewGuid();

        Initialize();
    }

    #endregion Constructors

    #region Methods

    private void Initialize()
    {
        Height = 80;
        Width = 50;

        if(Terminal.InputSockets != null)
        {
            foreach (var input in Terminal.InputSockets)
            {
                var socket = new SocketViewModel(this, _events, SocketTypeEnum.Input)
                {
                    Label = input.Name
                };
                InputSockets.Add(socket);
            }
        }

        if(Terminal.OutputSockets != null)
        {
            foreach (var output in Terminal.OutputSockets)
            {
                var socket = new SocketViewModel(this, _events, SocketTypeEnum.Output)
                {
                    Label = output.Name
                };

                OutputSockets.Add(socket);
            }
        }

        if (Terminal.RequireInputValue && Terminal is IValueTerminal<float> floatValueTerminal)
        {
            GetInputValue = true;
            InputValue = floatValueTerminal.Function.Output.ToString();
        }


    }

    public void SetInputValue(string value)
    {
        if(Terminal is IValueTerminal<float> floatValueTerminal && float.TryParse(value, out float floatValue))
            floatValueTerminal.Function.SetValue(floatValue);
        NotifyConnectors();

    }

    private void NotifyConnectors()
    {
        foreach (var connectedTerminal in Connectors)
        {
            //TODO: Notify all connectors that something was changed here.
        }
    }

    public void AddWire(IWire wire)
    {
        Wires.Add(wire);
    }

    public void Connect()
    {
        throw new NotImplementedException();
    }

    public void Dropped()
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(CanvasZoomPanEvent message, CancellationToken cancellationToken)
    {
        var dx = message.dX;
        var dy = message.dY;

        CanvasPositionX += dx;
        CanvasPositionY += dy;

        //TODO: Scale up everything in here instead of canvas?

        return Task.CompletedTask;
    }

    public Task HandleAsync(SelectItemEvent message, CancellationToken cancellationToken)
    {
        if (message.Item != this)
            Selected = false;
        return Task.CompletedTask;
    }

    public void Moved()
    {

    }

 


    private void NotifyWiresOfNewPosition()
    {
        foreach (var socket in InputSockets)
            socket.UpdatePosition();

        foreach (var socket in OutputSockets)
            socket.UpdatePosition();
    }

    #endregion Methods
}