using System.Drawing;
using Caliburn.Micro;
using SlateBoard.App.Interface;

namespace SlateBoard.App.SlateItems;

public class ConnectionPoint(double x, double y, IMoveableItem parent, IEventAggregator events) : INode
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;
    public HashCode Id { get; set; }

    public IMoveableItem Parent { get; set; } = parent;
    public IEventAggregator Events { get; set; } = events;
}