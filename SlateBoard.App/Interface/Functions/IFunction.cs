namespace SlateBoard.App.Interface.Functions
{
    internal interface IFunction
    {
        object[] Inputs { get; }
        object[] Outputs { get; }
    }
}
