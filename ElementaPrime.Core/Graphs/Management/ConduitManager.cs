using Caliburn.Micro;
using ElementaPrime.Core.Events.TerminalEvents;
using ElementaPrime.Core.Functions;
using ElementaPrime.Core.Graphs.Conduits;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;

namespace ElementaPrime.Core.Graphs.Management;

public class ConduitManager(): IHandle<TerminalRemovedEvent>
{
    public void ConnectSockets(IDataPort inputDataPort, IDataPort outputDataPort)
    {
      
        ConduitConnection newConnection = new ConduitConnection(inputDataPort,
            outputDataPort, new TypedValue<object>("", Guid.NewGuid())); 

        inputDataPort.ParentOperator.Connections.Add(newConnection);
        outputDataPort.ParentOperator.Connections.Add(newConnection);

        //TODO: Set connected inputSocket to connected
    }

    public void TerminalRemoved(IOperator @operator)
    {
        foreach (var connection in @operator.Connections)
        {
            connection.End.ParentOperator.Connections.Remove(connection);
        }
    }

    public Task HandleAsync(TerminalRemovedEvent message, CancellationToken cancellationToken)
    {
        TerminalRemoved(message.Operator);

        return Task.CompletedTask;
    }
}