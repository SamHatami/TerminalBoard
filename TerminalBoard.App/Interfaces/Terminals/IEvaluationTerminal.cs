using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Interfaces.Terminals;

public interface IEvaluationTerminal : ITerminal
{
    IEvaluationFunction EvaluationFunction { get; }
    void NotifyConnectors();
    void SetFunctionInputs(List<IValue> inputs);


}