using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Graphs.Management
{
    public class TerminalGraph
    {
        public List<IOperator> Terminals { get; } = [];
        public List<IConduit> Wires { get; } = [];

        public TerminalGraph(IOperator[] terminals)
        {
            Terminals.AddRange(terminals);
        }

        public void AddTerminal(IOperator @operator)
        {
            if (Terminals.Contains(@operator)) return;
            
            Terminals.Add(@operator);
        }

        public void RemoveTerminal(IOperator @operator)
        {
            if (Terminals == null || !Terminals.Contains(@operator)) return;
            
            Terminals.Remove(@operator);
        }

        public IOperator? GetTerminalById(string terminalDefintionId)
        {
            return Terminals.FirstOrDefault(t => string.Equals(t.TerminalDefinitionId, terminalDefintionId));
        }

        public void Clear()
        {
            Terminals.Clear();
        }

        public void ExecuteGraph() //this runs every possible scenario, needs to be more controlled
        {
            foreach (var terminal in Terminals)
            {

                terminal.Execute();

            }
        }


    }

    public static class TerminalGraphExtensions
    {
        /// <summary>
        /// Deep clone a TerminalGraph.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static TerminalGraph Clone(TerminalGraph original)
        {
            //var newGraph = new TerminalGraph();
            //foreach (var terminal in original.Terminals)
            //{

            //}
            //return newGraph;

            return null;
        }

        public static void TopographicalSort(this TerminalGraph graph)
        {

            // Kahn's algorithm
            //var sorted = new List<ITerminal>();
            //var terminalsWithNoIncomingEdges = new Queue<ITerminal>(graph.Terminals.Where(t => t.Connections.Count == 0));
            //while (terminalsWithNoIncomingEdges.Count > 0)
            //{
            //    var terminal = terminalsWithNoIncomingEdges.Dequeue();
            //    sorted.Add(terminal);
            //    foreach (var outputSocket in terminal.Outputs)
            //    {
            //        foreach (var wire in outputSocket.Connections.ToList())
            //        {
            //            var targetTerminal = wire.InputSocket.ParentTerminal;
            //            wire.InputSocket.Connections.Remove(wire);
            //            outputSocket.Connections.Remove(wire);
            //            if (targetTerminal.InputSockets.All(s => s.Connections.Count == 0))
            //            {
            //                terminalsWithNoIncomingEdges.Enqueue(targetTerminal);
            //            }
            //        }
            //    }
            //}
            //if (sorted.Count != graph.Terminals.Count)
            //{
            //    throw new InvalidOperationException("Graph has at least one cycle.");
            //}
            //graph.Terminals.Clear();
            //graph.Terminals.AddRange(sorted);

        }
    }
}