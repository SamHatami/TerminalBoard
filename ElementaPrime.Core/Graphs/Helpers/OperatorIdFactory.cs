using ElementaPrime.Core.Graphs.Operators;
using System.Reflection;
using ElementaPrime.Core.Extensions;

namespace ElementaPrime.Core.Graphs.Helpers
{
    internal static class OperatorIdFactory
    {
        public static OperatorId GetOperatorId(MethodInfo methodInfo, object providerInstance)
        {
            var type = providerInstance.GetType();
            var inputsAliases = methodInfo.GetArgumentString();

            return new OperatorId($"{type.FullName}.{methodInfo.Name}{inputsAliases}:{methodInfo.ReturnType.GetAliasName()}");
        }

    }
}