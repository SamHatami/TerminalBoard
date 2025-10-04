using TerminalBoard.Core.Graphs.Excecution;
using TerminalBoard.Core.Interfaces.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Graphs.Wires;

namespace TerminalBoard.Core.Interfaces.Graphs.Terminals;

/// <summary>
/// Base interface for a Terminal which contains inputs and outputs.
/// </summary>
public interface ITerminal 
{
    Guid Id { get; }
    string Label { get; }
    string TerminalDefinitionId { get; }
    List<ISocket> InputSockets { get; }
    List<ISocket> OutputSockets { get; }
    List<IWire> Connections { get; set; }
    
    TerminalExcecutionResult Execute();
    

}