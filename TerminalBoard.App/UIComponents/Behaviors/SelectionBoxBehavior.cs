using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace TerminalBoard.App.UIComponents.Behaviors;

public class SelectionBoxBehavior : Behavior<UIElement>
{
    private Point _startPoint;
    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.PreviewMouseDown+= OnLeftMouseButtonDown;
        AssociatedObject.PreviewMouseMove+= OnMouseMove;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.PreviewMouseDown -= OnLeftMouseButtonDown;
        AssociatedObject.PreviewMouseMove -= OnMouseMove;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured)
        {
            var currentPosition = e.GetPosition(AssociatedObject);
        }

        e.Handled = false;
    }

    private void OnLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
    {
        _startPoint = e.GetPosition(AssociatedObject);

        AssociatedObject.CaptureMouse();

        e.Handled = false;
    }
}