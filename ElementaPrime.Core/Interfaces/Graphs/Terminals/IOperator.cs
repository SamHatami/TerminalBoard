using ElementaPrime.Core.Graphs.Excecution;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Interfaces.Graphs.Terminals;

/// <summary>
/// Base interface for a Terminal which contains inputs and outputs.
/// </summary>
public interface IOperator 
{
    Guid Id { get; }
    string Label { get; }
    string TerminalDefinitionId { get; }
    List<IDataPort> InputSockets { get; }
    List<IDataPort> OutputSockets { get; }
    List<IConduit> Connections { get; set; }
    
    OperationResult Execute();
    

}