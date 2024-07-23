using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Interfaces.Terminals;

public interface IOutputTerminal : ITerminal
{
    bool ShowFinalOutputValue { get; }
    IValue Output { get; set; }
}