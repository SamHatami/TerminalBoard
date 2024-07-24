namespace TerminalBoard.Core.Interfaces.Functions;

public interface IEvaluationFunction : IFunction
{
    List<IValue> Inputs { get; set; }
    List<IValue> Outputs { get; }

    void SetInputValue(IValue value, Guid socketId);

    void Evaluate();
}