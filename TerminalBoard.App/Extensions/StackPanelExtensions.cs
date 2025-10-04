using System.Windows;
using System.Windows.Controls;
using TerminalBoard.Core.Enum;

namespace TerminalBoard.App.Extensions;

internal class StackPanelExtensions
{
    /// <summary>
    /// Reorders into a mirroring order
    /// </summary>
    public static readonly DependencyProperty ReverseChildrenProperty =
        DependencyProperty.RegisterAttached(
            "ReverseChildren",
            typeof(SocketDirection),
            typeof(StackPanelExtensions),
            new PropertyMetadata(SocketDirection.Input, OnReverseChildrenChanged));

    public static SocketDirection GetReverseChildren(DependencyObject obj)
    {
        return (SocketDirection)obj.GetValue(ReverseChildrenProperty);
    }

    public static void SetReverseChildren(DependencyObject obj, SocketDirection value)
    {
        obj.SetValue(ReverseChildrenProperty, value);
    }

    private static void OnReverseChildrenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is StackPanel stackPanel && e.NewValue is SocketDirection type) ReverseChildren(stackPanel, type);
    }

    private static void ReverseChildren(StackPanel stackPanel, SocketDirection type)
    {
        if (type == SocketDirection.Output) //Reverse if it's an output socketViewModel
        {
            var children = stackPanel.Children.Cast<UIElement>().ToList();
            children.Reverse();
            stackPanel.Children.Clear();

            foreach (var child in children)
                stackPanel.Children.Add(child);
        }
    }
}