using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Interfaces.Terminals;

public interface IEvaluationTerminal : ITerminal
{
    IEvaluationFunction EvaluationFunction { get; }
    void NotifyConnectors();
}