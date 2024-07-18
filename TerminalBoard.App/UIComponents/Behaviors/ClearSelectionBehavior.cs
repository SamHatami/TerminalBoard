using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using TerminalBoard.App.Events;
using TerminalBoard.App.Interface.ViewModel;
using TerminalBoard.App.UIComponents.Helpers;
using TerminalBoard.App.ViewModels;

namespace TerminalBoard.App.UIComponents.Behaviors
{
    public class ClearSelectionBehavior : Behavior<UIElement>
    {
        private MainViewModel? _mainViewModel;
        private Canvas? _mainCanvas;
        private IEventAggregator? _events;
        protected override void OnAttached()
        {
            base.OnAttached();

            _events = BehaviorHelper.EventsAggregator;

            if (AssociatedObject is Canvas canvas)
            {
                _mainCanvas = canvas;
                _mainCanvas.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            _mainCanvas.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;

        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPosition = e.GetPosition(_mainCanvas);

            VisualTreeHelper.HitTest(_mainCanvas, null, CheckType,
                new GeometryHitTestParameters(new EllipseGeometry(e.GetPosition(_mainCanvas), 1, 1)));

            AssociatedObject.ReleaseMouseCapture();

            e.Handled = false;
        }

        private HitTestResultBehavior CheckType(HitTestResult hit)
        {
            if (hit.VisualHit is Canvas)
            {
                _events.PublishOnBackgroundThreadAsync(new ClearSelectionEvent());
            }

            return HitTestResultBehavior.Continue;
        }
    }
}