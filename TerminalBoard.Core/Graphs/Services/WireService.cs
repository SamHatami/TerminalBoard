using Caliburn.Micro;
using TerminalBoard.Core.Events.TerminalEvents;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Graphs.Wires;
using TerminalBoard.Core.Interfaces.Graphs.Sockets;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;

namespace TerminalBoard.Core.Graphs.Services;

public class WireService(): IHandle<TerminalRemovedEvent>
{
    public void ConnectSockets(ISocket inputSocket, ISocket outputSocket)
    {
      
        WireConnection newConnection = new WireConnection(inputSocket,
            outputSocket, new TypedValue<object>("", Guid.NewGuid())); 

        inputSocket.ParentTerminal.Connections.Add(newConnection);
        outputSocket.ParentTerminal.Connections.Add(newConnection);

        //TODO: Set connected inputSocket to connected
    }

    public void TerminalRemoved(ITerminal terminal)
    {
        foreach (var connection in terminal.Connections)
        {
            connection.EndSocket.ParentTerminal.Connections.Remove(connection);
        }
    }

    public Task HandleAsync(TerminalRemovedEvent message, CancellationToken cancellationToken)
    {
        TerminalRemoved(message.Terminal);

        return Task.CompletedTask;
    }
}