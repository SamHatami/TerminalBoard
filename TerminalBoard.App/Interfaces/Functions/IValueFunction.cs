namespace TerminalBoard.App.Interfaces.Functions;

public interface IValueFunction<T> : IFunction
{
    void SetValue(T? value);
    public T? Output { get; set; }
}