using Caliburn.Micro;
using ElementaPrime.Core.Enum;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;


namespace ElementaPrime.TestApp.Interfaces.ViewModels;

public interface ISocketViewModel
{
    bool IsConnected { get; }
    string Label { get; set; }
    double X { get; set; }
    double Y { get; set; }
    Guid Id { get; }

    void AddWire(IWireViewModel wire);
    List<IWireViewModel> Wires { get; }
    ITerminalViewModel ParentViewModel { get; }
    DataPortDirection Type { get; }
    
    IDataPort DataPort { get; }
    IEventAggregator Events { get; }

    void UpdatePosition();
}