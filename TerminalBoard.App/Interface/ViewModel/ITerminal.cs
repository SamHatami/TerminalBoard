using Caliburn.Micro;

namespace TerminalBoard.App.Interface.ViewModel;
/// <summary>
/// Interface for a dragable item. 
/// </summary>
public interface ITerminal : ISelectable
{
    #region Properties

    List<ISocket> InputSocket { get; set; }
    List<ISocket> OutputSocket { get; set; }
    List<ITerminal> Connectors { get; set; }
    List<IWire> Wires { get; set; }
    public IEventAggregator Events { get; }
    int Height { get; set; }
    Guid Id { get; }
    int Width { get; set; }
    double X { get; set; }
    double Y { get; set; }

    #endregion Properties

    #region Methods

    void AddWire(IWire wire);
    void Connect();

    void Dropped();

    void Moved();

    #endregion Methods

    //TODO: Future wire connections
}