using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using SlateBoard.App.Events;
using SlateBoard.App.Extensions;
using SlateBoard.App.Interface;

namespace SlateBoard.App.Behaviours
{
    internal class WireConnectionBehavior : Behavior<UIElement>
    {
        private IEventAggregator _events;

        private Point _centerPoint;
        private Canvas _mainCanvas;

        private Point _linePoint1;
        private INode _startPoint;
        private INode _endPoint;
        private IMoveableItem _moveableItem;

        private Line _currentLine;
        private bool _isDragging;

        private IWire _wire;


        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
            
            
            _mainCanvas = GetMainCanvas(AssociatedObject);
            SetDataContextAndEvents();

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           //Create a wire with a starting point as this point and end point as eventual connection point
 
            Task.WhenAll(_events.PublishOnBackgroundThreadAsync(new CreateWireEvent(_startPoint)));
            if(_moveableItem.Wires.Count == 0) return;
            _wire = _moveableItem.Wires[0]; //TODO fix this

            var mouseCurrentPosition = e.GetPosition(_mainCanvas);
            _currentLine = new Line
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = mouseCurrentPosition.X,
                Y1 = mouseCurrentPosition.Y,
                X2 = mouseCurrentPosition.X,
                Y2 = mouseCurrentPosition.Y,
            };

            LineExtension.SetWireId(_currentLine, _wire.Id);
            _mainCanvas.Children.Add(_currentLine);
            
            AssociatedObject.CaptureMouse();
            e.Handled = true;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (AssociatedObject.IsMouseCaptured)
            {
                var mouseCurrentPosition = e.GetPosition(_mainCanvas);

                _currentLine.X2 = mouseCurrentPosition.X;
                _currentLine.Y2 = mouseCurrentPosition.Y;

                var direction = mouseCurrentPosition - _centerPoint;

                var newLeft = _centerPoint.X + direction.X; //Consider width and height of contentcontrol
                var newTop = _centerPoint.Y + direction.Y;

   
                if (double.IsNaN(newLeft) || double.IsNaN(newTop))
                    return;

                //_wire.End.X = newLeft;
                //_wire.End.Y = newTop;
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (AssociatedObject.IsMouseCaptured) AssociatedObject.ReleaseMouseCapture();

        }

        private void SetDataContextAndEvents()
        {
            if (AssociatedObject is FrameworkElement { DataContext: INode item })
            {
                _startPoint = item;
                _moveableItem = _startPoint.Parent;
                _events = _startPoint.Events;
                _events.SubscribeOnBackgroundThread(this);
            }
        }

        private Canvas GetMainCanvas(DependencyObject? element)
        {
            while (element != null)
            {
                if (element is Canvas { Name: "MainCanvas" } canvas) return canvas;
                element = VisualTreeHelper.GetParent(element);
            }

            return null;
        }
    }
}
