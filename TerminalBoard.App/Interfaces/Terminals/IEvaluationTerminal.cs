using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Interfaces;

public interface IEvaluationTerminal<T> : ITerminal
{
    IEvaluationFunction<T> EvaluationFunction { get; }

}