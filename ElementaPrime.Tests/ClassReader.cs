using System.Reflection;
using ElementaPrime.Core;
using ElementaPrime.Core.Graphs.Helpers;
using ElementaPrime.Core.Graphs.Operators;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;


namespace ElementaPrime.Tests;

public class ClassReaderTest
{
    public ClassReaderTest()
    {
    }

    [Fact]
    public void VaultServiceMethodTestRead()
    {
        var operators = new List<IOperator>();

        // Create ONE instance of the provider class
        var vaultService = new VaultService();
        var vaultServiceType = vaultService.GetType();
        var vaultServiceAttributes = vaultServiceType.GetCustomAttributes<ProviderAttribute>();
        // Create operator for each method
        foreach (var method in vaultServiceType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
        {
            // Create a terminal that wraps this method
            var operatorId = OperatorIdFactory.GetOperatorId(method, vaultService);
            var @operator = ProviderOperator.Create(vaultService, method,"", operatorId);
            operators.Add(@operator);
        }
    }
}