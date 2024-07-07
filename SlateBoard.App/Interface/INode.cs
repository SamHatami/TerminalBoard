using System.Drawing;
using Caliburn.Micro;

namespace SlateBoard.App.Interface;

public interface INode
{
    double X { get; set; }
    double Y { get; set; }
    HashCode Id { get; set; }
    IMoveableItem Parent { get; set; }

    IEventAggregator Events { get; set; }
}