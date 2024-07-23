using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Functions;

public class FloatValue(float value, string name, Guid guid) : IValue
{
    public float Value { get; set; } = value;

    public object ValueObject
    {
        get => Value;
        set
        {
            if (value is float floatValue)
                Value = floatValue;
        }
    }

    public string Name { get; } = name;
    public Guid Id { get; } = guid; //Should come from the socket
}