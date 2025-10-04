namespace TerminalBoard.Core.Interfaces.Terminals;

public interface IValueTerminal<T> : ITerminal
{
    T Value { get; set; }

}