using TerminalBoard.App.Functions;

namespace TerminalBoard.App.Interfaces.Functions;

public interface IEvaluationFunction : IFunction
{
    List<Input> Inputs { get; }
    List<Output> Outputs { get; }

    void SetInputValues(Input[] inputs);

    void Evaluate();
}