namespace TerminalBoard.Core.Interfaces.Functions;

public interface IEvaluationFunction : IFunction
{
    List<IValue> Inputs { get; }
    List<IValue> Outputs { get; }

    void SetInputValue(IValue newValue, IValue oldValue);

    void Evaluate();
}