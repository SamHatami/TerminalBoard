using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ElementaPrime.Core.Extensions
{
    public static class MethodInfoExtensions
    {
        public static string GetArgumentString(this MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();

            var argumentstring = new StringBuilder(parameters.Length);
            argumentstring.Append("(");
            for (var i = 0; i < parameters.Length; i++)
            {
                argumentstring.Append(parameters[i].ParameterType.GetAliasName());

                if(i != parameters.Length-1)
                    argumentstring.Append(",");
            }

            argumentstring.Append(")");
            return argumentstring.ToString();
        }

        public static Type[] GetParametersTypes(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
                return [];
            
            var types = new Type[methodInfo.GetParameters().Length];

            for (var i = 0; i < methodInfo.GetParameters().Length; i++)
            {
                types[i]=methodInfo.GetParameters()[i].ParameterType;
            }

            return types;
        }

        public static string GetFullMethodSignature(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
                return string.Empty;

            var methodName = methodInfo.Name;
            var arguments = methodInfo.GetArgumentString();

            return $"{methodName}{arguments}";
        }
    }
}
