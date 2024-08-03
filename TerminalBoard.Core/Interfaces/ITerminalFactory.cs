using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Interfaces;

public interface ITerminalFactory
{
    public ITerminal Instantiate(TerminalType terminalType);
}