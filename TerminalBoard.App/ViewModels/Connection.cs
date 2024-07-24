using TerminalBoard.App.Interfaces.ViewModels;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.App.ViewModels;

public class Connection
{
    public ISocketViewModel? InputSocketViewModel { get; set; } 
    public ISocketViewModel? OutputSocketViewModel { get; set; }

    public ITerminal? InputTerminal { get; set; }
    public ITerminal? OutputTerminal { get; set; }
}