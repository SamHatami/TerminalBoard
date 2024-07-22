namespace TerminalBoard.App.Functions;

public class Output
{
    public Type Type { get; set; }
    public object Value { get; }
    public string Name { get; }

    public Output(object value, string name, Type type)
    {
        Value = value;
        Name = name;
        Type = type;
    }
}