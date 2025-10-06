using System.Reflection;
using ElementaPrime.Core.Graphs.Excecution;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Graphs.Operators;

//Used along side reflection to build up the terminals based on a class's methods
public abstract class ProviderOperatorBase : IProviderOperator 
{
    private readonly MethodInfo _methodInfo;
    public string Label { get; set; }
    public object ProviderInstance { get; set; }
    public string ProviderCategory { get; set; }
    public string TerminalDefinitionId { get; set; }
    public List<IDataPort> InputSockets { get; } = [];
    public List<IDataPort> Outputs { get; } = [];
    public List<IConduit> Connections { get; set; } = [];

    public static string GetMethodTerminalId(string methodName, object providerInstance) 
    {
        var type = providerInstance.GetType();
        return $"{type.Name}.{methodName}";
    }

    public Guid Id { get; }
    public abstract OperationResult Execute();

    private protected abstract bool ValidateInput(IDataPort dataPort, object? value);
    private protected abstract void CreateInputs();
    private protected abstract void CreateOutputs();
}