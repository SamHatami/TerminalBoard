using Caliburn.Micro;
using SlateBoard.App.Interface;

namespace SlateBoard.App.ViewModels;

public class NodeViewModel: INode
{
    public NodeViewModel(double x, double y, IMoveableItem parent, IEventAggregator events)
    {
        X = x;
        Y = y;
        Parent = parent;
        Events = events;
    }

    public double X { get; set; }
    public double Y { get; set; }
    public HashCode Id { get; set; }
    public IMoveableItem Parent { get; set; }
    public IEventAggregator Events { get; set; }
}