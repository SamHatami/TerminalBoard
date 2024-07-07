using System.Drawing;
using Caliburn.Micro;
using SlateBoard.App.Interface;
using SlateBoard.App.SlateItems;

namespace SlateBoard.App.ViewModels;

public class SlateViewModel : PropertyChangedBase, IMoveableItem
{
    private readonly IEventAggregator _events;
    public int Height { get; set; }
    public HashCode Id { get; set; }
    public int Width { get; set; }

    public string Title;
    public int GridSize = 100;

    private double _x;

    public double X
    {
        get => _x;
        set
        {
            _x = value;
            NotifyOfPropertyChange(nameof(X));
        }
    }

    private double _y;

    public double Y
    {
        get => _y;
        set
        {
            _y = value;
            NotifyOfPropertyChange(nameof(Y));
        }
    }

    public List<IWire> Wires { get; set; } = new List<IWire>();
    public IEventAggregator Events { get; }

    public List<INode> ConnectionPoints { get; set; } = new List<INode>();
    public List<IMoveableItem> Connectors { get; set; } = new List<IMoveableItem>();

    public SlateViewModel(IEventAggregator events)
    {
        Events = events;
        Events.Subscribe(this);

        TestInit();
    }

    private void TestInit()
    {
        Height = 80;
        Width = 50;
       
        ConnectionPoints.Add( new NodeViewModel(0, 0, this, Events));
    }

    public void Moved()
    {
    }

    public void Dropped()
    {
        throw new NotImplementedException();
    }

    public void AddWire(IWire wire)
    {
        Wires.Add(wire);
    }

    public void Connect()
    {
        throw new NotImplementedException();
    }
}