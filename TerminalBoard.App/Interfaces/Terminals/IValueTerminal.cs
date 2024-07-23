using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Interfaces.Terminals;

public interface IValueTerminal : ITerminal
{
    IValueFunction Function { get; }
    bool RequireInputValue { get; }


}