using Caliburn.Micro;

namespace SlateBoard.App.Interface;
/// <summary>
/// Interface for a dragable item. 
/// </summary>
public interface IMoveableItem
{
    #region Properties
    
    List<INode> ConnectionPoints { get; set; }
    List<IMoveableItem> Connectors { get; set; }
    List<IWire> Wires { get; set; }
    public IEventAggregator Events { get; }
    int Height { get; set; }
    HashCode Id { get; set; }
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