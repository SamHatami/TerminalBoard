using System.Reflection;
using TerminalBoard.Core.Interfaces;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

//Used along side reflection to build up the terminals based on a class's methods
public abstract class ClassTerminalBase<T> : ITerminal
{
    private readonly MethodInfo _methodInfo;
    public T Provider { get; }
    public string Label { get; set; }
    public string ProviderCategory { get; protected init; }
    public string TerminalDefinitionId { get; protected init; }
    public List<ISocket> InputSockets { get; } = [];
    public List<ISocket> OutputSockets { get; } = [];
    public List<IWire> Connections { get; set; } = [];

    public void UpdateInput(ISocket socket, IValue newValue)
    {
    }

    public static string GetMethodTerminalId(string methodName) 
    {
        return $"{typeof(T).Name}.{methodName}";
    }

    public Guid Id { get; }
    public abstract void Execute();

    private protected abstract bool ValidateInput(ISocket socket, object? value);
    private protected abstract void CreateInputs();
    private protected abstract void CreateOutputs();
}