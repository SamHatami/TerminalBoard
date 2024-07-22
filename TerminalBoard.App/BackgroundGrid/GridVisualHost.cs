﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TerminalBoard.App.BackgroundGrid;

public class GridVisualHost : FrameworkElement
{
    private GridVisual _gridVisual;

    public static readonly DependencyProperty CanvasWidthProperty =
        DependencyProperty.Register("CanvasWidth", typeof(double), typeof(GridVisualHost), new PropertyMetadata(800.0));

    public static readonly DependencyProperty CanvasHeightProperty =
        DependencyProperty.Register("CanvasHeight", typeof(double), typeof(GridVisualHost),
            new PropertyMetadata(450.0));

    public static readonly DependencyProperty MouseLeftButtonDownHandlerProperty =
        DependencyProperty.Register("MouseLeftButtonDownHandler", typeof(Action<object, MouseButtonEventArgs>),
            typeof(GridVisualHost));

    public double CanvasWidth
    {
        get => (double)GetValue(CanvasWidthProperty);
        set => SetValue(CanvasWidthProperty, value);
    }

    public double CanvasHeight
    {
        get => (double)GetValue(CanvasHeightProperty);
        set => SetValue(CanvasHeightProperty, value);
    }

    public GridVisualHost(Canvas canvas)
    {
        _gridVisual = new GridVisual(canvas);
    }

    private void UpdateGrid()
    {
        _gridVisual.DrawGrid(CanvasHeight, CanvasWidth);
    }

    protected override int VisualChildrenCount => 1;

    protected override Visual GetVisualChild(int index)
    {
        return _gridVisual;
    }

    private static void CanvasWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var element = (GridVisualHost)d;
        element.UpdateGrid();
    }

    private static void CanvasHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var element = (GridVisualHost)d;
        element.UpdateGrid();
    }
}