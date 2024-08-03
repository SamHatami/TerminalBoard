using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class TerminalFactory : ITerminalFactory
{
    private static Dictionary<TerminalType, Func<ITerminal>> _terminals = [];

    public ITerminal Instantiate(TerminalType terminalType) //switch name to some enum
    {
        if (_terminals.TryGetValue(terminalType, out var terminal))
            return terminal();

        return null;
    }

    public static void RegisterTerminal(TerminalType terminalType, Func<ITerminal> terminal)
    {
        _terminals[terminalType] = terminal;
    }
}