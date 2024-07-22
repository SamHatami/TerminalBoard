using TerminalBoard.App.Interfaces;
using TerminalBoard.App.Interfaces.ViewModels;

namespace TerminalBoard.App.ViewModels;

public class Connection
{
    public ISocketViewModel InputSocketViewModel { get; set; }
    public ISocketViewModel OutputSocketViewModel { get; set; }

    public ITerminal InputTerminal { get; set; }
    public ITerminal OutputTerminal { get; set; }
}