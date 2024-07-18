using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interface.ViewModel;
using TerminalBoard.App.UIComponents.Helpers;

namespace TerminalBoard.App.UIComponents.Behaviors
{
    public class SelectionBehavior : Behavior<UIElement>
    {
        private IWire? _wireViewModel;
        private ITerminal _terminalViewModel;
        private IEventAggregator? _events;
        private readonly ISelectable? _item;

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

            e.Handled = false;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPosition = e.GetPosition(AssociatedObject);

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
                _events.PublishOnBackgroundThreadAsync(new SelectItemEvent(selectable));
            }

            return HitTestResultBehavior.Continue;
        }
    }
}