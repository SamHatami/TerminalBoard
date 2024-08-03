using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Functions;

public static class FunctionFactory
{
    private static Dictionary<string, Func<IFunction>?> _registeredFunctions = [];


    /// <summary>
    /// Returns new instance of function if it is registered
    /// </summary>
    /// <param name="functionName"></param>
    /// <returns></returns>
    public static IFunction? GetFunction(string functionName)
    {
        if (_registeredFunctions.TryGetValue(functionName, out var registeredFunction)
            && registeredFunction != null)
            return registeredFunction();

        return null;
    }
    
    public static void RegisterFunction<T>(string functionName) where T : IFunction, new()
    {
        if (_registeredFunctions.TryGetValue(functionName, out var registeredFunction))
            return;

        _registeredFunctions.Add(functionName, () => new T());
    }   
    public static void RegisterFunction(string functionName, Func<IFunction> function) 
    {
        if (_registeredFunctions.TryGetValue(functionName, out var registeredFunction))
            return;

        _registeredFunctions.Add(functionName, function);
    }
}