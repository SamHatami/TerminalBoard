using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Functions;

public static class FunctionFactory
{
    public static IFunction CreateFunction(string name)
    {
        switch (name)
        {
            case "Float": return new ValueOutputFunction();
            case "Int": return new ValueOutputFunction();
        }

        return null;
    }
}