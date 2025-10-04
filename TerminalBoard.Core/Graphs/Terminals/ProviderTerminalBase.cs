using System.Reflection;
using TerminalBoard.Core.Graphs.Excecution;
using TerminalBoard.Core.Interfaces.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;
using TerminalBoard.Core.Interfaces.Graphs.Wires;

namespace TerminalBoard.Core.Graphs.Terminals;

//Used along side reflection to build up the terminals based on a class's methods
public abstract class ProviderTerminalBase : IProviderTerminal 
{
    private readonly MethodInfo _methodInfo;
    public string Label { get; set; }
    public object ProviderInstance { get; set; }
    public string ProviderCategory { get; set; }
    public string TerminalDefinitionId { get; set; }
    public List<ISocket> InputSockets { get; } = [];
    public List<ISocket> OutputSockets { get; } = [];
    public List<IWire> Connections { get; set; } = [];

    public static string GetMethodTerminalId(string methodName, object providerInstance) 
    {
        var type = providerInstance.GetType();
        return $"{type.Name}.{methodName}";
    }

    public Guid Id { get; }
    public abstract TerminalExcecutionResult Execute();

    private protected abstract bool ValidateInput(ISocket socket, object? value);
    private protected abstract void CreateInputs();
    private protected abstract void CreateOutputs();
}