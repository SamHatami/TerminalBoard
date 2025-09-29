namespace TerminalBoard.Core;

[AttributeUsage(AttributeTargets.Class)]
public class TerminalProviderAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}

[AttributeUsage(AttributeTargets.Method)]
public class TerminalNameAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}

[AttributeUsage(AttributeTargets.Method)]
public class TerminalDescriptionAttribute(string description) : Attribute
{
    public string Description { get; } = description;
}