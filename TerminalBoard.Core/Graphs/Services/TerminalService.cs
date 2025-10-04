using System.Reflection;
using TerminalBoard.Core.Graphs.Terminals;
using TerminalBoard.Core.Interfaces;
using TerminalBoard.Core.Interfaces.Graphs.Terminals;

namespace TerminalBoard.Core.Graphs.Services;

public class  TerminalService
{
    //private readonly ILogger _logger;

    private Dictionary<string, Func<ITerminal>> _terminalRegistry = new();

    public TerminalService( /*ILogger logger*/)
    {
        //_logger = logger;
    }

    public void RegisterProviderMethods<T>(T providerInstance) where T : class, IProvider
    {
        var providerAttributes = typeof(T).GetCustomAttributes<TerminalProviderAttribute>();


        var methods = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly |
                                           BindingFlags.Static);

        //TODO: Handle multiple attributes
        foreach (var method in typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static))
        {
            // Create a terminal that wraps this method

            var terminalId = ProviderTerminalBase.GetMethodTerminalId(method.Name, providerInstance);

            if (_terminalRegistry.ContainsKey(terminalId))
            {
                //Nah... if there is an method with several overloads then this will only take the first one
                //we should build another way of identifying methods?
                //Seperate class that holds original methods infos along side the overloads?

                //TODO log warning
                continue;
            }

            _terminalRegistry[terminalId] = () => ProviderOperationTerminal.Create(providerInstance, method, "");
        }
    }

    public ITerminal? GetTerminal(string terminalId)
    {
        if (_terminalRegistry.TryGetValue(terminalId, out var terminal))
            return terminal();

        return null;
    }

}