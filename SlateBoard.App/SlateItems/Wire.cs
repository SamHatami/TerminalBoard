using System.Windows.Documents;
using SlateBoard.App.Enum;
using SlateBoard.App.Interface;

namespace SlateBoard.App.SlateItems;

public class Wire : IWire
{
    public INode Start { get; set; }
    public INode End { get; set; }  
    public WireTypeEnum WireType { get; set; }
    public Guid Id { get; set; }

    public Wire()
    {
        Id = Guid.NewGuid();
    }
}