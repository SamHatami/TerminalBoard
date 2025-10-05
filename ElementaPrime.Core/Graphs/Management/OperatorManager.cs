using System.Reflection;
using ElementaPrime.Core.Graphs.Operators;
using ElementaPrime.Core.Interfaces;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;

namespace ElementaPrime.Core.Graphs.Management
{
    //Creates and manages terminals
    public class OperatorManager
    {
        private Dictionary<string, Func<IOperator>> _terminalRegistry = new();

        public OperatorManager( /*ILogger logger*/)
        {
            //_logger = logger;
        }

        public void RegisterProviderMethods<T>(T providerInstance) where T : class, IProvider
        {
            var providerAttributes = typeof(T).GetCustomAttributes<ProviderAttribute>();


            var methods = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly |
                                               BindingFlags.Static);

            //TODO: Handle multiple attributes
            foreach (var method in typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static))
            {
                // Create a terminal that wraps this method

                var terminalId = ProviderOperatorBase.GetMethodTerminalId(method.Name, providerInstance);

                if (_terminalRegistry.ContainsKey(terminalId))
                {
                    //Nah... if there is an method with several overloads then this will only take the first one
                    //we should build another way of identifying methods?
                    //Seperate class that holds original methods infos along side the overloads?

                    //TODO log warning
                    continue;
                }

                _terminalRegistry[terminalId] = () => ProviderOperator.Create(providerInstance, method, "");
            }
        }

        public IOperator? GetTerminal(string terminalId)
        {
            if (_terminalRegistry.TryGetValue(terminalId, out var terminal))
                return terminal();

            return null;
        }
    }
}