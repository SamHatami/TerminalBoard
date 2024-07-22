using TerminalBoard.App.Enum;
using TerminalBoard.App.Interfaces;
using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Terminals;

public class EvaluationTerminal<T> : IEvaluationTerminal<T>
{
    public EvaluationTerminal(IEvaluationFunction<T> evaluationFunction)
    {
        EvaluationFunction = evaluationFunction;
        Id = Guid.NewGuid();
        Label = EvaluationFunction.Label;
        InitializeSockets();
    }

    public string Label { get; }
    public List<ISocket> InputSockets { get; } = [];
    public List<ISocket> OutputSockets { get; } = [];
    public List<ITerminal> Connectors { get; set; } = [];
    public bool RequireInputValue { get; } = false;
    public bool ShowFinalOutputValue { get; } = false;
    public Guid Id { get; }
    public IEvaluationFunction<T> EvaluationFunction { get; }

    private void InitializeSockets()
    {
        foreach (var input in EvaluationFunction.Inputs) InputSockets.Add(new Socket(SocketTypeEnum.Input, input.Name));

        foreach (var output in EvaluationFunction.Outputs)
            InputSockets.Add(new Socket(SocketTypeEnum.Output, output.Name));
    }
}