using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Interfaces.Terminals;

public interface IOutputTerminal : ITerminal
{
    bool ShowFinalOutputValue { get; }
    IValue Output { get; set; }
}