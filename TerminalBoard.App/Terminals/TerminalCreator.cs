using Caliburn.Micro;
using FunctionFactory = TerminalBoard.App.Functions.FunctionFactory;
using ITerminal = TerminalBoard.App.Interfaces.ITerminal;

namespace TerminalBoard.App.Terminals;

public static class TerminalCreator
{
    public static ITerminal New(string name, IEventAggregator eventAggregator) //switch name to some enum
    {
        var function = FunctionFactory.CreateFunction(name);

        var terminal = new FloatOutputTerminal();

        return terminal;
    }
}