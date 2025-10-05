namespace ElementaPrime.Core.Interfaces.Graphs.Terminals;

/// <summary>
/// Base interface for a Terminal which contains inputs and outputs.
/// </summary>
public interface IProviderOperator : IOperator 
{
    protected object ProviderInstance { get; set; }  // Non-generic
    public string ProviderCategory { get; protected set; }
    public string TerminalDefinitionId { get; protected set; }
    

}