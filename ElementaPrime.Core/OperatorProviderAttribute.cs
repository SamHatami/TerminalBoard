namespace ElementaPrime.Core;

[AttributeUsage(AttributeTargets.Class)]
public class ProviderAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}

[AttributeUsage(AttributeTargets.Method)]
public class ProviderNameAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}

[AttributeUsage(AttributeTargets.Method)]
public class ProviderDescriptionAttribute(string description) : Attribute
{
    public string Description { get; } = description;
}