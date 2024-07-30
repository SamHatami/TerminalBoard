using Caliburn.Micro;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public static class TerminalCreator
{
    public static ITerminal New(string name) //switch name to some enum
    {
        var function = FunctionFactory.CreateFunction(name);

        var terminal = new FloatValueTerminal();

        return terminal;
    }
}