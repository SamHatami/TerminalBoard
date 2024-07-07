using SlateBoard.App.Enum;
using SlateBoard.App.Interface;

namespace SlateBoard.App.SlateItems;

public class Wire : IWire
{
    public INode Start { get; set; }
    public INode End { get; set; } 
    public WireTypeEnum WireType { get; set; }
    public HashCode Id { get; set; }

    public Wire()
    {
        
    }
}