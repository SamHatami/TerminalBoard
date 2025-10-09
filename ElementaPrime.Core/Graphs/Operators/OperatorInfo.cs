namespace ElementaPrime.Core.Graphs.Operators
{
    public record OperatorInfo(string Provider, string OperatorSignature, OperatorId OperatorId)
    {
        // For UI display
        public string DisplayName => $"{Provider}.{OperatorSignature}";
    }

    public sealed record OperatorId(string Value)
    {
        public override string ToString() => Value;
    }
}