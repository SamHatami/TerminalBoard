using Caliburn.Micro;
using TerminalBoard.App.Functions;
using TerminalBoard.App.Interfaces;

namespace TerminalBoard.App.Terminals;

public static class TerminalCreator
{
    public static ITerminal New(string name, IEventAggregator eventAggregator) //switch name to some enum
    {
        var function = FunctionFactory.CreateFunction(name);

        var terminal = new FloatValueTerminal();

        return terminal;
    }
}