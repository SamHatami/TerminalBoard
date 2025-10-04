namespace TerminalBoard.Core.Interfaces.Graphs.Terminals;

public interface IOutputTerminal : ITerminal
{
    bool ShowFinalOutputValue { get; }
}