using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Functions;

public static class FunctionFactory
{
    public static IFunction CreateFunction(string name)
    {
        switch (name)
        {
            case "Float": return new TypedValueOutputFunction<float>();
            case "Int": return new TypedValueOutputFunction<int>();
        }

        return null;
    }
}