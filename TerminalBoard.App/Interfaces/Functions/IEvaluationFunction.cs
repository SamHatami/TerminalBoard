using TerminalBoard.App.Functions;

namespace TerminalBoard.App.Interfaces.Functions;

public interface IEvaluationFunction<T> : IFunction
{
    List<Input<T>> Inputs { get; }
    List<Output<T>> Outputs { get; }

    void SetInputValues(Input<T>[] inputs);

    void Evaluate();
}