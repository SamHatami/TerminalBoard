﻿using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TerminalBoard.App.Events.UIEvents;
using TerminalBoard.App.Interfaces.ViewModels;
using TerminalBoard.App.UIComponents.Helpers;
using TerminalBoard.Core.Interfaces.Terminals;

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
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var clickPosition = e.GetPosition(AssociatedObject);

        VisualTreeHelper.HitTest(AssociatedObject, null, CheckType,
            new GeometryHitTestParameters(new EllipseGeometry(e.GetPosition(AssociatedObject), 1, 1)));

        _events.PublishOnBackgroundThreadAsync(new SelectItemEvent(_item));
        AssociatedObject.ReleaseMouseCapture();

        e.Handled = false;
    }

    private HitTestFilterBehavior SelectionFilter(DependencyObject potentialHitTestTarget)
    {
        if (potentialHitTestTarget is ContentControl { DataContext: ISelectable selectable })
            return HitTestFilterBehavior.ContinueSkipChildren;

        return HitTestFilterBehavior.Continue;
    }

    private HitTestResultBehavior CheckType(HitTestResult hit)
    {
        if (hit.VisualHit is FrameworkElement { DataContext: ISelectable selectable })
        {
            selectable.Selected = true;
            _item = selectable;
            _events.PublishOnBackgroundThreadAsync(new SelectItemEvent(selectable));
            AssociatedObject.Focus();

            return HitTestResultBehavior.Stop;
        }

        return HitTestResultBehavior.Continue;
    }
}