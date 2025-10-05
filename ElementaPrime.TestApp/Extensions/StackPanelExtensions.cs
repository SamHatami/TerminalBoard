using System.Windows;
using System.Windows.Controls;
using ElementaPrime.Core.Enum;

namespace ElementaPrime.TestApp.Extensions;

internal class StackPanelExtensions
{
    /// <summary>
    /// Reorders into a mirroring order
    /// </summary>
    public static readonly DependencyProperty ReverseChildrenProperty =
        DependencyProperty.RegisterAttached(
            "ReverseChildren",
            typeof(DataPortDirection),
            typeof(StackPanelExtensions),
            new PropertyMetadata(DataPortDirection.Input, OnReverseChildrenChanged));

    public static DataPortDirection GetReverseChildren(DependencyObject obj)
    {
        return (DataPortDirection)obj.GetValue(ReverseChildrenProperty);
    }

    public static void SetReverseChildren(DependencyObject obj, DataPortDirection value)
    {
        obj.SetValue(ReverseChildrenProperty, value);
    }

    private static void OnReverseChildrenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is StackPanel stackPanel && e.NewValue is DataPortDirection type) ReverseChildren(stackPanel, type);
    }

    private static void ReverseChildren(StackPanel stackPanel, DataPortDirection type)
    {
        if (type == DataPortDirection.Output) //Reverse if it's an output socketViewModel
        {
            var children = stackPanel.Children.Cast<UIElement>().ToList();
            children.Reverse();
            stackPanel.Children.Clear();

            foreach (var child in children)
                stackPanel.Children.Add(child);
        }
    }
}