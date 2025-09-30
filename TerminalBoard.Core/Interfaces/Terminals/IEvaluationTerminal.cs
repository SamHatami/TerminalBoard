using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Interfaces.Terminals;

[Obsolete("replaced by MethodTerminal")]
public interface IEvaluationTerminal : ITerminal
{
    IEvaluationFunction EvaluationFunction { get; }
    void NotifyConnectors();
}