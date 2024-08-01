using TerminalBoard.Core.Functions;

namespace TerminalBoard.Core.Interfaces.Functions;

public interface ITypedValueFunction<T> : IFunction
{
    void SetValue(TypedValue<T> value);
    public TypedValue<T> Output { get; set; }
}
