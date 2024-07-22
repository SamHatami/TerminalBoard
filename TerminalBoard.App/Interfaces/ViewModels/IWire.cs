using System.Windows;
using TerminalBoard.App.Enum;

namespace TerminalBoard.App.Interfaces.ViewModels;

/// <summary>
/// Connecting slates by wire
/// </summary>
public interface IWire : ISelectable
{
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }
    ISocketViewModel StartSocketViewModel { get; set; }
    ISocketViewModel EndSocketViewModel { get; set; }

    ITerminal InputTerminal { get; set; }
    ITerminal OutputTerminal { get; set; }
    WireTypeEnum WireType { get; set; }

    void UpdatePosition(ISocketViewModel socketViewModel);

    Guid Id { get; }
}