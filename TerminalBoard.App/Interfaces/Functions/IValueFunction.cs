namespace TerminalBoard.App.Interfaces.Functions;

public interface IValueFunction : IFunction
{
    void SetValue(IValue value);
    public IValue Output { get; set; }
}