using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Interfaces;

public interface ITerminalCreator
{
    public ITerminal Instantiate(TerminalType terminalType);
}