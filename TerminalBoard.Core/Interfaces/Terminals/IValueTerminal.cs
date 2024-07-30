using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Interfaces.Terminals;

public interface IValueTerminal : ITerminal
{
    IValueFunction Function { get; }
    bool RequireInputValue { get; } //For UI stuff, but could probably be replaced by some type-check in the viewmodel.


}