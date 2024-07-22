using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Interfaces;

public interface IValueTerminal<T> : ITerminal
{
    IValueFunction<T> Function { get; }
    bool RequireInputValue { get; }


}