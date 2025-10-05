using ElementaPrime.TestApp.Interfaces.ViewModels;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;

namespace ElementaPrime.TestApp.ViewModels;

public class Connection
{
    public ISocketViewModel? InputSocketViewModel { get; set; } 
    public ISocketViewModel? OutputSocketViewModel { get; set; }

    public IOperator? InputTerminal { get; set; }
    public IOperator? OutputTerminal { get; set; }
}