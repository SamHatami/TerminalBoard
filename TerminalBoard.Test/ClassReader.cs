using System.Reflection;
using TerminalBoard.Core;
using TerminalBoard.Core.Graph.Terminals;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Test;

public class ClassReaderTest
{
    public ClassReaderTest()
    {
    }

    [Fact]
    public void VaultServiceMethodTestRead()
    {
        var terminals = new List<ITerminal>();

        // Create ONE instance of the provider class
        var vaultService = new VaultService();
        var vaultServiceType = vaultService.GetType();
        var vaultServiceAttributes = vaultServiceType.GetCustomAttributes<TerminalProviderAttribute>();
        // Create terminals for each method
        foreach (var method in vaultServiceType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
        {
            // Create a terminal that wraps this method
            var terminal = ProviderOperationTerminal.Create(vaultService, method, "");
            terminals.Add(terminal);
        }
    }
}