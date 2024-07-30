using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;
using TerminalBoard.Core.Wires;

namespace TerminalBoard.Core.Services;

public class TerminalService
{
    
    public TerminalService()
    {
    }

    private ITerminal CreateTerminal(TerminalType terminalType)
    {
        return null;
    }

    public void UpdateTerminal()
    {
    }

    public void DeleteTerminal()
    {
    }

    public void ConnectTerminals(ITerminal inputTerminal, ITerminal outputTerminal)
    {
    }

    public void RemoveConnection(ITerminal terminal, WireConnection wire)
    {
    }
}