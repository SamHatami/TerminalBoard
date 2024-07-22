namespace TerminalBoard.App.Functions;

public class Input
{
    public Type Type { get; set; }
    public object Value { get; }
    public string Name { get; }

    public Input(object value, string name, Type type)
    {
        Value = value;
        Name = name;
        Type = type;
    }
}