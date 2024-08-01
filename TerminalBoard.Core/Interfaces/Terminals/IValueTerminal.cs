using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Interfaces.Terminals;

public interface IValueTerminal<T> : ITerminal
{
    ITypedValueFunction<T> Function { get; }
    bool RequireInputValue { get; } //For UI stuff, but could probably be replaced by some type-check in the viewmodel.


}