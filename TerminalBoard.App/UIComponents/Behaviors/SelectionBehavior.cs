using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TerminalBoard.App.Events;
using TerminalBoard.App.UIComponents.Helpers;
using ISelectable = TerminalBoard.App.Interfaces.ViewModels.ISelectable;
using ITerminal = TerminalBoard.App.Interfaces.Terminals.ITerminal;
using IWireViewModel = TerminalBoard.App.Interfaces.ViewModels.IWireViewModel;

namespace TerminalBoard.App.UIComponents.Behaviors;

public class SelectionBehavior : Behavior<UIElement>
{
    private IWireViewModel? _wireViewModel;
    private ITerminal _terminalViewModel;
    private IEventAggregator? _events;
    private ISelectable? _item;

    protected override void OnAttached()
    {
        base.OnAttached();
        _events = BehaviorHelper.EventsAggregator;
        AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
        AssociatedObject.PreviewMouseLeftButtonUp += OnMouseButtonUp;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
        AssociatedObject.PreviewMouseLeftButtonUp -= OnMouseButtonUp;
    }

    private void OnMouseButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_item == null) return;
        _events.PublishOnBackgroundThreadAsync(new SelectItemEvent(_item));
        e.Handled = false;
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var clickPosition = e.GetPosition(AssociatedObject);

        VisualTreeHelper.HitTest(AssociatedObject, null, CheckType,
            new GeometryHitTestParameters(new EllipseGeometry(e.GetPosition(AssociatedObject), 1, 1)));

        AssociatedObject.ReleaseMouseCapture();

        e.Handled = false;
    }

    private HitTestResultBehavior CheckType(HitTestResult hit)
    {
        if (hit.VisualHit is FrameworkElement { DataContext: ISelectable selectable })
        {
            selectable.Selected = true;
            _item = selectable;
            _events.PublishOnBackgroundThreadAsync(new SelectItemEvent(selectable));
        }

        return HitTestResultBehavior.Continue;
    }
}