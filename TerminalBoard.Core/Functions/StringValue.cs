using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Functions;

public class StringValue(string value, string name, Guid guid) : IValue
{
    public string Value { get; set; } = value;

    public object ValueObject
    {
        get => Value;
        set
        {
            if (value is string floatValue)
                Value = floatValue;
        }
    }

    public string Name { get; } = name;
    public Guid Id { get; } = guid; //Should come from the socket
}