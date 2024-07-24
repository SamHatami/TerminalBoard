using TerminalBoard.Core.Interfaces.Terminals;
using TerminalBoard.Core.Terminals;

namespace TerminalBoard.Core.Services;

public class TerminalService
{
    
    public TerminalService()
    {
    }

    private ITerminal CreateTerminal()
    {
        TerminalCreator.New()
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