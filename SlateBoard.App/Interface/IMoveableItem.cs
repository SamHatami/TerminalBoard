using Caliburn.Micro;

namespace SlateBoard.App.Interface;

public interface IMoveableItem
{
    #region Properties

    IConnectionPoint[] ConnectionPoints { get; set; }
    IMoveableItem[] Connectors { get; set; }
    public IEventAggregator Events { get; }
    int Height { get; set; }
    HashCode Id { get; set; }
    int Width { get; set; }
    double X { get; set; }
    double Y { get; set; }

    #endregion Properties

    #region Methods

    void Connect();

    void Dropped();

    void Moved();

    #endregion Methods

    //TODO: Future wire connections
}