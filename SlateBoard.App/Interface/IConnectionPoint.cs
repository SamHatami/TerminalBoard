using System.Drawing;

namespace SlateBoard.App.Interface;

public interface IConnectionPoint
{
    Point Position { get; set; }

    HashCode Id { get; set; }

    IMoveableItem Parent { get; set; }
}