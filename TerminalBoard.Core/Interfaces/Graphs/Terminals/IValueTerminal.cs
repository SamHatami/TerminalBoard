namespace TerminalBoard.Core.Interfaces.Graphs.Terminals;

public interface IValueTerminal<T> : ITerminal
{
    T Value { get; set; }

}