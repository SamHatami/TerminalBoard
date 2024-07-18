using System.Windows;
using System.Windows.Controls;
using TerminalBoard.App.Enum;

namespace TerminalBoard.App.Extensions;

internal class StackPanelExtensions
{
    /// <summary>
    /// Reorders into a mirroring order
    /// </summary>
    public static readonly DependencyProperty ReverseChildrenProperty =
        DependencyProperty.RegisterAttached(
            "ReverseChildren",
            typeof(SocketTypeEnum),
            typeof(StackPanelExtensions),
            new PropertyMetadata(SocketTypeEnum.Input, OnReverseChildrenChanged));

    public static SocketTypeEnum GetReverseChildren(DependencyObject obj)
    {
        return (SocketTypeEnum)obj.GetValue(ReverseChildrenProperty);
    }

    public static void SetReverseChildren(DependencyObject obj, SocketTypeEnum value)
    {
        obj.SetValue(ReverseChildrenProperty, value);
    }

    private static void OnReverseChildrenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is StackPanel stackPanel && e.NewValue is SocketTypeEnum type) ReverseChildren(stackPanel, type);
    }

    private static void ReverseChildren(StackPanel stackPanel, SocketTypeEnum type)
    {
        if (type == SocketTypeEnum.Output) //Reverse if its an output socket
        {
            var children = stackPanel.Children.Cast<UIElement>().ToList();
            children.Reverse();
            stackPanel.Children.Clear();

            foreach (var child in children)
                stackPanel.Children.Add(child);
        }
    }
}