using System.Diagnostics;
using Caliburn.Micro;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class EvaluationTerminal : IEvaluationTerminal
{
    private Dictionary<ISocket, IValue> _values = [];

    public string Label { get; }
    public List<ISocket> InputSockets { get; private set; } = [];
    public List<ISocket> OutputSockets { get; } = [];

    private List<IWire> _connections;

    public List<IWire> Connections
    {
        get => _connections;
        set
        {
            _connections = value;
        }
    }

    public Guid Id { get; }
    public IEventAggregator Events { get; }

    public IEvaluationFunction EvaluationFunction { get; }

    public EvaluationTerminal(IEvaluationFunction evaluationFunction)
    {
        EvaluationFunction = evaluationFunction;
        Connections = [];
        Id = Guid.NewGuid();
        Label = EvaluationFunction.Label;
        InitializeSockets();
    }

    private void InitializeSockets()
    {
        foreach (var input in EvaluationFunction.Inputs)
        {
            var newSocket = new Socket(SocketTypeEnum.Input, input.Name, this) { ValueType = input.Value.GetType() };
            InputSockets.Add(newSocket); //TODO: cast?
            _values.Add(newSocket, input);
        }

        foreach (var output in EvaluationFunction.Outputs)
            OutputSockets.Add(new Socket(SocketTypeEnum.Output, output.Name, this)
                { ValueType = output.Value.GetType() });
    }

    public void NotifyConnectors()
    {
        foreach (var wire in Connections)
            if (wire.StartSocket.ParentTerminal.Id == Id) //Only notify outbound connections
                wire.Value = EvaluationFunction.Outputs[0];
    }

    public void UpdateInput(ISocket socket, IValue newValue)
    {
        if (!_values.TryGetValue(socket, out var definedValue)) return;

        EvaluationFunction.SetInputValue(newValue, definedValue);
        NotifyConnectors();
    }
}