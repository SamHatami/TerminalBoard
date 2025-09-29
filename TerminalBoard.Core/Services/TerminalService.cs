using System.Reflection;
using TerminalBoard.Core.Interfaces;
using TerminalBoard.Core.Interfaces.Terminals;
using TerminalBoard.Core.Terminals;

namespace TerminalBoard.Core.Services;

public class TerminalService
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

        //TODO: Handle multiple attributes
        foreach (var method in typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static))
        {
            // Create a terminal that wraps this method
            var terminalId = ClassTerminalBase<T>.GetMethodTerminalId(method.Name);

            if (_terminalRegistry.ContainsKey(terminalId))
            {
                //TODO log warning
                continue;
            }

            _terminalRegistry[terminalId] = () => MethodTerminal<T>.Create(providerInstance, method, "");
        }
    }

    public ITerminal? GetTerminal(string terminalId)
    {
        if (_terminalRegistry.TryGetValue(terminalId, out var terminal))
            return terminal();

        return null;
    }
}