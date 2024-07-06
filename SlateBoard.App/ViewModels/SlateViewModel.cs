using Caliburn.Micro;
using SlateBoard.App.Interface;

namespace SlateBoard.App.ViewModels;

public class SlateViewModel : PropertyChangedBase, IMoveableItem
{
    private readonly IEventAggregator _events;
    public int Height { get; set; }
    public HashCode Id { get; set; }
    public int Width { get; set; }

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

    public IEventAggregator Events { get; }
    public IConnectionPoint[] ConnectionPoints { get; set; }
    public IMoveableItem[]? Connectors { get; set; }

    public SlateViewModel(IEventAggregator events)
    {
        Events = events;
        Events.Subscribe(this);
    }

    
    public void Moved()
    {
    }

    public void Dropped()
    {
        throw new NotImplementedException();
    }

    public void Connect()
    {
        throw new NotImplementedException();
    }
}