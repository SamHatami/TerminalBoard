using SlateBoard.App.Enum;

namespace SlateBoard.App.Interface;

public interface IWire
{
    IConnectionPoint Start { get; set; }
    IConnectionPoint End { get; set; }
    HashCode Id { get; set; }

    WireTypeEnum WireType { get; set; }
}