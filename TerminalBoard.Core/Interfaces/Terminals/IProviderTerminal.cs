using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Interfaces.Terminals;

/// <summary>
/// Base interface for a Terminal which contains inputs and outputs.
/// </summary>
public interface IProviderTerminal : ITerminal 
{
    protected object ProviderInstance { get; set; }  // Non-generic
    public string ProviderCategory { get; protected set; }
    public string TerminalDefinitionId { get; protected set; }
    

}