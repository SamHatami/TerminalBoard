using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Functions;

public class TypedValue<T> : IValue
{
    public T Value { get; set; }
    public string Name { get; }
    public Guid Id { get; }

    object IValue.Value
    {
        get => Value;
        set => Value = (T)value;
    }

    public TypedValue(string name, Guid id)
    {
        Name = name;
        Id = id;
    }
}