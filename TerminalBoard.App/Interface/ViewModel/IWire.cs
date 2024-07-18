using System.DirectoryServices;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using TerminalBoard.App.Enum;

namespace TerminalBoard.App.Interface.ViewModel;

/// <summary>
/// Connecting slates by wire
/// </summary>
public interface IWire : ISelectable
{
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }
    ISocket StartSocket { get; set; }
    ISocket EndSocket { get; set; }

    ITerminal InputTerminal { get; set; }
    ITerminal OutputTerminal { get; set; }
    WireTypeEnum WireType { get; set; }

    void UpdatePosition(ISocket socket);
    Guid Id { get; }
}