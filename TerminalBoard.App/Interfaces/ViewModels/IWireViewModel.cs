using System.Windows;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;


namespace TerminalBoard.App.Interfaces.ViewModels;

/// <summary>
/// Connecting slates by wire
/// </summary>
public interface IWireViewModel : ISelectable, IDisposable
{
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }
    ISocketViewModel StartSocketViewModel { get; set; }
    ISocketViewModel EndSocketViewModel { get; set; }

    ITerminal InputTerminal { get; set; }
    ITerminal OutputTerminal { get; set; }
    WireTypeEnum WireType { get; set; }

    IWire WireConnection { get; set; }

    void UpdatePosition(ISocketViewModel socketViewModel);

    Guid Id { get; }
}