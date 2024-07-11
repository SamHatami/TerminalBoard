using System.Windows;
using Caliburn.Micro;
using SlateBoard.App.Enum;

namespace SlateBoard.App.Interface;

/// <summary>
/// Connecting slates by wire
/// </summary>
public interface IWire
{
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }
    ISocket StartSocket { get; set; }
    ISocket EndSocket { get; set; }

    ITerminal InputTerminal { get; set; }
    ITerminal OutputTerminal { get; set; }
    WireTypeEnum WireType { get; set; }

    Guid Id { get; }
}