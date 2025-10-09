using ElementaPrime.Core.Graphs.Operators;
using ElementaPrime.Core.Interfaces;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using System.Reflection;
using ElementaPrime.Core.Extensions;
using ElementaPrime.Core.Graphs.Helpers;

namespace ElementaPrime.Core.Graphs.Management
{
    //Creates and manages terminals, should only be accessed by the GraphManager
    internal class OperatorFactory : IOperatorFactory
    {
        private record Registration(Func<IOperator> Creator, MethodInfo ProviderMethod, Type ProviderType);

        private readonly Dictionary<OperatorId, Registration> _operatorRegistry = new();

        public OperatorFactory( /*ILogger logger*/)
        {
            //_logger = logger;
        }

        public void RegisterProviderMethods<T>(T providerInstance) where T : class, IProvider
        {
            var providerAttributes = typeof(T).GetCustomAttributes<ProviderAttribute>();

            var methods = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly |
                                               BindingFlags.Static);

            foreach (var method in methods)
            {

                var operatorId = OperatorIdFactory.GetOperatorId(method, providerInstance); //terminal id is based on the method signature and its parameters, so it should be unique even if there are overloads

                if (_operatorRegistry.ContainsKey(operatorId))
                {
                    //TODO log warning
                    continue;
                }

                _operatorRegistry[operatorId] = new Registration(
                    Creator: () => ProviderOperator.Create(providerInstance, method,"", operatorId),
                    ProviderMethod: method,
                    ProviderType: providerInstance.GetType() // använd runtime-typen om T kan vara ett interface/proxy
                );
            }
        }

        public IOperator? GetProviderOperator(OperatorId id)
        {
            if (_operatorRegistry.TryGetValue(id, out var registration))
                return registration.Creator();

            return null;
        }


        public OperatorInfo[] GetAllProviderOperatorInfos()

        {
            List<OperatorInfo> infos = [];
            foreach (var kvp in _operatorRegistry)
            {
                var reg = kvp.Value;
                var operatorSignature = reg.ProviderMethod.GetFullMethodSignature();
                var providerName = reg.ProviderType.FullName ?? reg.ProviderType.Name;
                infos.Add(new OperatorInfo(providerName, operatorSignature,kvp.Key));
            }

            return infos.ToArray();
        }

    }

    public interface IOperatorFactory
    {
        void RegisterProviderMethods<T>(T providerInstance) where T : class, IProvider;
        IOperator? GetProviderOperator(OperatorId id);
        OperatorInfo[] GetAllProviderOperatorInfos();

    }

}