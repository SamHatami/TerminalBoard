using System.Reflection;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core;

public abstract class ClassTerminalBase<T>: ITerminal 
{
    private readonly MethodInfo _methodInfo;
    public T Provider { get; }
    public string Label { get; }
    public List<ISocket> InputSockets { get; }
    public List<ISocket> OutputSockets { get; }
    public List<IWire> Connections { get; set; }
    public void UpdateInput(ISocket socket, IValue newValue)
    {
    }

    public Guid Id { get; }
    public abstract void Execute();

    private protected abstract bool ValidateInput(ISocket socket, object? value);
    private protected abstract void CreateInputs();
    private protected abstract void CreateOutputs();
}