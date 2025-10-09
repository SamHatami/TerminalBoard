using ElementaPrime.Core.Graphs.Conduits;
using ElementaPrime.Core.Graphs.Operators;
using ElementaPrime.Core.Interfaces;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Graphs.Management
{
    public class GraphManager
    {
        private readonly IConduitManager _conduitManager;
        private readonly IOperatorFactory _operatorFactory;
        public List<IOperator> Operators { get; } = [];
        public List<IConduit> Connections { get; } = [];

        public GraphManager(IConduitManager conduitManager, IOperatorFactory operatorFactory)
        {
            _conduitManager = conduitManager;
            _operatorFactory = operatorFactory;
        }

        public void AddTerminal(IOperator @operator)
        {
            if (Operators.Contains(@operator)) return;

            Operators.Add(@operator);
        }

        public void RemoveTerminal(IOperator @operator)
        {
            if (Operators == null || !Operators.Contains(@operator)) return;

            Operators.Remove(@operator);
        }

        public IOperator? GetTerminalById(string terminalDefintionId)
        {
            return Operators.FirstOrDefault(t => string.Equals(t.TerminalDefinitionId, terminalDefintionId));
        }

        public void Clear()
        {
            Operators.Clear();
        }

        public void ExecuteGraph() //this runs every possible scenario, needs to be more controlled
        {
            foreach (var terminal in Operators)
            {
                terminal.Execute();
            }
        }

        public OperatorInfo[] GetAllProviderOperatorsInfos()
        {
            return _operatorFactory.GetAllProviderOperatorInfos();
        }

        public IOperator? GetProviderOperator(OperatorInfo operatorInfo)
        {
            return _operatorFactory.GetProviderOperator(operatorInfo.OperatorId);
        }

        public void ConnectSockets(IDataPort startPort, IDataPort endPort)
        {
            if(!ConduitValidator.Validate(startPort, endPort))
                return;
            _conduitManager.ConnectSockets(startPort, endPort);
        }

        public void ConnectOperators(IConduit valueConduit, ValueOperator<long> valueOperator, IOperator getFileOperator)
        {
            //hmm
            if (!Operators.Contains(valueOperator))
                Operators.Add(valueOperator);
            if (!Operators.Contains(getFileOperator))
                Operators.Add(getFileOperator);
            Connections.Add(valueConduit);
            valueOperator.Connections.Add(valueConduit);
            getFileOperator.Connections.Add(valueConduit);
        }
    }

    public static class TerminalGraphExtensions
    {
        /// <summary>
        /// Deep clone a GraphManager.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static GraphManager Clone(GraphManager original)
        {
            //var newGraph = new GraphManager();
            //foreach (var terminal in original.Operators)
            //{
            //}
            //return newGraph;

            return null;
        }

        public static void TopographicalSort(this GraphManager graphManager)
        {
            // Kahn's algorithm
            //var sorted = new List<ITerminal>();
            //var terminalsWithNoIncomingEdges = new Queue<ITerminal>(graphManager.Operators.Where(t => t.Connections.Count == 0));
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
            //if (sorted.Count != graphManager.Operators.Count)
            //{
            //    throw new InvalidOperationException("Graph has at least one cycle.");
            //}
            //graphManager.Operators.Clear();
            //graphManager.Operators.AddRange(sorted);
        }
    }
}