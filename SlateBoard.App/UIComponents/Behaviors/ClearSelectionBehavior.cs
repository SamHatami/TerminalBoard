using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using SlateBoard.App.Events;
using SlateBoard.App.Interface.ViewModel;
using SlateBoard.App.UIComponents.Helpers;
using SlateBoard.App.ViewModels;

namespace SlateBoard.App.UIComponents.Behaviors
{
    public class ClearSelectionBehavior : Behavior<UIElement>
    {
        private MainViewModel? _mainViewModel;
        private Canvas? _mainCanvas;
        private IEventAggregator? _events;
        private ISelectable _item;

        protected override void OnAttached()
        {
            base.OnAttached();

            _events = BehaviorHelper.EventsAggregator;

            if (AssociatedObject is Canvas canvas)
            {
                _mainCanvas = canvas;
                _mainCanvas.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
                _mainCanvas.PreviewMouseLeftButtonUp += OnMouseButtonUp;
                _mainCanvas.KeyUp += OnKeyUp;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                _events.PublishOnBackgroundThreadAsync(new SelectItemEvent(_item));

            e.Handled = false;

        }

        private void OnMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_item == null) return;

            _events.PublishOnBackgroundThreadAsync(new SelectItemEvent(_item));

            e.Handled = false;
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