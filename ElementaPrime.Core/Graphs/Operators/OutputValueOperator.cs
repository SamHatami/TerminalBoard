using ElementaPrime.Core.Enum;
using ElementaPrime.Core.Graphs.DataPorts;
using ElementaPrime.Core.Graphs.Excecution;
using ElementaPrime.Core.Interfaces.Functions;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Graphs.Operators;


//Not sure of this class yet, this terminal should be able to take any value type and show it. But not all outputs will be able to show a value.
//it just might aswell not be used for antying at all.
public class OutputValueOperator : IOutputOperator
{
    public Guid Id { get; }
    public string Label { get; } = "Result";
    public string TerminalDefinitionId { get; }
    public List<IDataPort> InputSockets { get; } = [];
    public List<IDataPort> Outputs { get; } = [];
    public OperationResult Execute() => OperationResult.NotApplicable;
    public List<IConduit> Connections { get; set; } = [];
    public bool ShowFinalOutputValue { get; } = true;

    public IValue Value { get; set; } 
    public OutputValueOperator()
    {
        Id = Guid.NewGuid();
        TerminalDefinitionId = $"SimpleOutputTerminal";
        Initialize();
    }

    private void Initialize()
    {
        InputSockets.Add(new ValueDataPort(DataPortDirection.Input, "", this, typeof(string))); //PLaceholder
    }
}