using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using SlateBoard.App.Enum;
using SlateBoard.App.Events;
using SlateBoard.App.Extensions;
using SlateBoard.App.Interface;
using SlateBoard.App.ViewModels;

namespace SlateBoard.App.Behaviours
{
    internal class WireConnectionBehavior : Behavior<UIElement>
    {
        private IEventAggregator _events;

        private Point _centerPoint;
        private Canvas _mainCanvas;

        private Point _linePoint1;
        private ISocket _startSocket;
        private ISocket _endSocket;
        private ITerminal _terminal;

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

            if(_terminal == null)
                SetDataContextAndEvents();
           //Create a wire with a starting point as this point and end point as eventual connection point

           Point relativeLocation = AssociatedObject.TranslatePoint(default, _mainCanvas);
           var mouseCurrentPosition = e.GetPosition(_mainCanvas);

            
            _currentLine = new Line
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = relativeLocation.X,
                Y1 = relativeLocation.Y,
                X2 = mouseCurrentPosition.X,
                Y2 = mouseCurrentPosition.Y,
            };

            _mainCanvas.Children.Add(_currentLine);
            
            AssociatedObject.CaptureMouse();
            e.Handled = true;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (AssociatedObject.IsMouseCaptured)
            {
                var currentPosition = e.GetPosition(_mainCanvas);

                _currentLine.X2 = currentPosition.X;
                _currentLine.Y2 = currentPosition.Y;

                var direction = currentPosition - _centerPoint;

                var newLeft = _centerPoint.X + direction.X; //Consider width and height of contentcontrol
                var newTop = _centerPoint.Y + direction.Y;


                //var element = Mouse.DirectlyOver;

                //if(element != null) HandleMouseOverElement(element);

                HitTestResult mouseTraceHit = VisualTreeHelper.HitTest(AssociatedObject, currentPosition); //TODO: Probably enough to do this on check on mouse up? But how to highlight input socket ?

                VisualTreeHelper.HitTest(_mainCanvas, null, CheckIfOverSocket, new GeometryHitTestParameters(new EllipseGeometry(currentPosition, 1, 1)));

          
                if (double.IsNaN(newLeft) || double.IsNaN(newTop))
                    return;

         
            }
        }

        private HitTestResultBehavior CheckIfOverSocket(HitTestResult hit)
        {
            if (hit.VisualHit is FrameworkElement element)
            {
                if (element.DataContext is ISocket { Type: SocketTypeEnum.Input } socket)
                {
                        _endSocket = socket;

                }
            }

            return HitTestResultBehavior.Continue;
        }


        //private void HandleMouseOverElement(IInputElement element)
        //{
        //    if (element is UIElement socketView)
        //    {

        //    }
        //}

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (AssociatedObject.IsMouseCaptured) AssociatedObject.ReleaseMouseCapture();

            if(_endSocket == null)
            {
                _mainCanvas.Children.Remove(_currentLine);
                return;
            }

            IWire wire = new WireViewModel(_startSocket,_endSocket,_startSocket.Events); //TODO meh 
            var mainViewModel = _mainCanvas.DataContext as MainViewModel;
            mainViewModel.Wires.Add(wire); //replace with a WireModel
            _mainCanvas.Children.Remove(_currentLine);


        }

        private void SetDataContextAndEvents()
        {
            if (AssociatedObject is FrameworkElement { DataContext: ISocket item })
            {
                _startSocket = item;
                _terminal = _startSocket.Slate;
                _events = _startSocket.Events;
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

        private static T FindParent<T>(DependencyObject child) where T : class
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }
    }
}
