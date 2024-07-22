using System.Numerics;

namespace TerminalBoard.App.Functions;

public class NumericValue<TValue>(TValue value, string label)
    where TValue : INumber<TValue>
{
    public string Label { get; set; } = label;
    public TValue Value { get; set; } = value;
}