using TerminalBoard.Core.Graph;
using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Interfaces.Terminals;

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