namespace TerminalBoard.Core;

[AttributeUsage(AttributeTargets.Class)]
sealed class TerminalProviderAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}

[AttributeUsage(AttributeTargets.Method)]
sealed class TerminalNameAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}