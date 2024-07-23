using Caliburn.Micro;
using TerminalBoard.App.Interfaces.Terminals;

namespace TerminalBoard.App.Interfaces.ViewModels;

public interface ITerminalViewModel : ISelectable
{
    #region Properties

    ITerminal Terminal { get; }
    BindableCollection<ISocketViewModel> InputSocketsViewModels { get; set; }
    BindableCollection<ISocketViewModel> OutputSocketViewModels { get; set; }
    List<IWireViewModel> Wires { get; set; }
    int Height { get; set; }
    int Width { get; set; }
    double CanvasPositionX { get; set; }
    double CanvasPositionY { get; set; }
    public string InputValue { get; set; }
    public string OutputValue { get; set; }
    #endregion Properties

    #region Methods

    void AddWire(IWireViewModel wire);
    void SetInputValue(string value); //if terminal is an IValueTerminal

    #endregion Methods

    //TODO: Future wire connections
}