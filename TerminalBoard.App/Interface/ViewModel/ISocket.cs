using System.Windows;
using Caliburn.Micro;
using TerminalBoard.App.Enum;

namespace TerminalBoard.App.Interface.ViewModel;

public interface ISocket
{
    string Label { get; set; }
    double X { get; set; }
    double Y { get; set; }
    Guid Id { get; }
    ITerminal ParentTerminal { get; set; }
    SocketTypeEnum Type { get; set; }
    IEventAggregator Events { get; set; }
}