using Caliburn.Micro;
using TerminalBoard.App.Enum;

namespace TerminalBoard.App.Interfaces.ViewModels;

public interface ISocketViewModel
{
    bool IsConnected { get; }
    string Label { get; set; }
    double X { get; set; }
    double Y { get; set; }
    Guid Id { get; }

    List<IWire> Wires { get; set; }
    ITerminalViewModel ParentViewModel { get; set; }
    SocketTypeEnum Type { get; set; }
    IEventAggregator Events { get; set; }

    void UpdatePosition();
}