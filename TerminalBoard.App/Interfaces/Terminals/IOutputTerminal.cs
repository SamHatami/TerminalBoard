namespace TerminalBoard.App.Interfaces.Terminals;

public interface IOutputTerminal : ITerminal
{
    bool ShowFinalOutputValue { get; }
}