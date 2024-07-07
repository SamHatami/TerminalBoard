using System.Windows;

namespace SlateBoard.App.Extensions;

public static class LineExtension
{
    public static readonly DependencyProperty WireIdProperty =
        DependencyProperty.RegisterAttached(
            "WireId",
            typeof(Guid),
            typeof(LineExtension),
            new PropertyMetadata(Guid.Empty));

    public static void SetWireId(UIElement element, Guid value)
    {
        element.SetValue(WireIdProperty, value);
    }

    public static Guid GetWireId(UIElement element)
    {
        return (Guid)element.GetValue(WireIdProperty);
    }
}