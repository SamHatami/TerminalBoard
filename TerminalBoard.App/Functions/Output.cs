namespace TerminalBoard.App.Functions;

public class Output<T>(T value, string name, Type type)
{
    public Type Type { get; set; } = type;
    public T Value { get; set; } = value;
    public string Name { get; } = name;
}