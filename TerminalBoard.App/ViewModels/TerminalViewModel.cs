using System.Globalization;
using Caliburn.Micro;
using TerminalBoard.App.Events.UIEvents;
using TerminalBoard.App.Interfaces.ViewModels;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Events.TerminalEvents;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Terminals;


namespace TerminalBoard.App.ViewModels;

public class TerminalViewModel : PropertyChangedBase, ITerminalViewModel, IHandle<CanvasZoomPanEvent>,
    IHandle<SelectItemEvent>, IHandle<OutputUpdateEvent>
{
    #region Fields

    private readonly IEventAggregator _events;
    private bool _selected;
    private double _canvasPositionX;
    private double _canvasPositionY;

    #endregion Fields

    #region Properties

    public ITerminal Terminal { get; }
    public int Height { get; set; }
    public Guid Id { get; }
    public BindableCollection<ISocketViewModel> InputSocketsViewModels { get; set; } = [];
    public BindableCollection<ISocketViewModel> OutputSocketViewModels { get; set; } = [];
    public List<IWireViewModel> WireViewModels { get; set; } = [];
    
    public bool GetInputValue { get; private set; }
    public bool ShowFinalOutput { get; private set; }

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

    private string _outputValue;
    public string OutputValue
    {
        get => _outputValue;
        set
        {
            _outputValue = value;
            NotifyOfPropertyChange(nameof(OutputValue));
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
        InputValue = String.Empty;

        //TODO: Clean the crazyness below up 

        if(Terminal.InputSockets != null)
        {
            foreach (var inputSocket in Terminal.InputSockets)
            {
                var socket = new SocketViewModel(this, _events, SocketTypeEnum.Input, inputSocket)
                {
                    Label = inputSocket.Name
                };
                InputSocketsViewModels.Add(socket);
            }
        }

        if(Terminal.OutputSockets != null)
        {
            foreach (var outputSocket in Terminal.OutputSockets)
            {
                var socket = new SocketViewModel(this, _events, SocketTypeEnum.Output, outputSocket)
                {
                    Label = outputSocket.Name
                };

                OutputSocketViewModels.Add(socket);
            }
        }

        if (Terminal is IValueTerminal floatValueTerminal)
        {
            GetInputValue = true;
            if(floatValueTerminal.Function.Output is FloatValue floatValue)
                InputValue = floatValue.Value.ToString("0.0", CultureInfo.CurrentCulture);
        }


        if (Terminal is IOutputTerminal outputTerminal)
        {
            ShowFinalOutput = true;
        }


    }

    public void AdjustNumeric(string no)
    {
        
        switch (no)
        {
            case "+":
                
                break;
            case "-":
                
                break;

        }

    }

    public void SetInputValue(string value) //TODO... usch
    {
        if (Terminal is IValueTerminal floatValueTerminal && float.TryParse(value, out float floatValue)) 
        {
            foreach (var wire in Terminal.Connections)
            {
                wire.Value = new FloatValue(Convert.ToSingle(value), "", Guid.NewGuid());
            }
        }
    }

    public void somethign()
    {

    }

    public void AddWireViewModel(IWireViewModel wire)
    {
        WireViewModels.Add(wire);
    }


    public Task HandleAsync(CanvasZoomPanEvent message, CancellationToken cancellationToken)
    {
        var dx = message.Dx;
        var dy = message.Dy;

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
        foreach (var socket in InputSocketsViewModels)
            socket.UpdatePosition();

        foreach (var socket in OutputSocketViewModels)
            socket.UpdatePosition();
    }

    #endregion Methods

    public Task HandleAsync(OutputUpdateEvent message, CancellationToken cancellationToken)
    {
        if (Terminal != message.Terminal)
            return Task.CompletedTask;

        OutputValue = message.Output.ValueObject.ToString();
        return Task.CompletedTask;
    }
}