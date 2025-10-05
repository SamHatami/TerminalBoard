using System.Windows;
using ElementaPrime.Core.Enum;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;


namespace ElementaPrime.TestApp.Interfaces.ViewModels;

/// <summary>
/// Connecting slates by wire
/// </summary>
public interface IWireViewModel : ISelectable, IDisposable
{
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }
    ISocketViewModel StartSocketViewModel { get; set; }
    ISocketViewModel EndSocketViewModel { get; set; }

    IOperator InputOperator { get; set; }
    IOperator OutputOperator { get; set; }
    ConduitTypeEnum ConduitType { get; set; }

    IConduit ConduitConnection { get; set; }

    void UpdatePosition(ISocketViewModel socketViewModel);

    Guid Id { get; }
}