using SlateBoard.App.Enum;

namespace SlateBoard.App.Interface;

/// <summary>
/// Connecting slates by wire
/// </summary>
public interface IWire
{
    INode Start { get; set; }
    INode End { get; set; }
    WireTypeEnum WireType { get; set; }
    Guid Id { get; set; }
}