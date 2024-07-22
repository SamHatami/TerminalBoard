using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Functions;

public static class FunctionFactory
{
    public static IFunction CreateFunction(string name)
    {
        switch (name)
        {
            case "Float": return new ValueOutputFunction<float>();
            case "Int": return new ValueOutputFunction<int>();
        }

        return null;
    }
}