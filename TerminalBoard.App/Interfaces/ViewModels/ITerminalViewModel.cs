using Caliburn.Micro;

namespace TerminalBoard.App.Interfaces.ViewModels;

public interface ITerminalViewModel : ISelectable
{
    #region Properties

    ITerminal Terminal { get; }
    List<ITerminal> Connectors { get; set; }
    BindableCollection<ISocketViewModel> InputSockets { get; set; }
    BindableCollection<ISocketViewModel> OutputSockets { get; set; }
    List<IWire> Wires { get; set; }
    int Height { get; set; }
    int Width { get; set; }
    double CanvasPositionX { get; set; }
    double CanvasPositionY { get; set; }

    public string InputValue { get; set; }
    #endregion Properties

    #region Methods

    void AddWire(IWire wire);

    void Connect();

    void Dropped();

    void Moved();

    void SetInputValue(string value); //if terminal is an IValueTerminal

    #endregion Methods

    //TODO: Future wire connections
}