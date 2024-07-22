using Caliburn.Micro;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TerminalBoard.App.Events;
using GridTypeEnum = TerminalBoard.App.Enum.GridTypeEnum;

namespace TerminalBoard.App.BackgroundGrid;

public class GridVisual : DrawingVisual, IHandle<GridChangeEvent>
{
    private int _gridSize = 25;
    private GridTypeEnum _gridType;
    private double _dotRadius;
    private Canvas _canvas;

    public GridVisual(Canvas canvas)
    {
        _canvas = canvas;
        DrawGrid(500, 500);
    }

    public void DrawGrid(double canvasHeight, double canvasWidth)
    {
        var dottedPen = new Pen(Brushes.Red, 0.5);
        dottedPen.DashStyle = new DashStyle(new double[] { 25, 25 }, 0);
        dottedPen.Freeze();

        using (var dc = RenderOpen())
        {
            for (double x = 0; x < 800; x += _gridSize)
                dc.DrawLine(dottedPen, new Point(x, 0), new Point(x, canvasHeight));
            for (double y = 0; y < 450; y += _gridSize)
                dc.DrawLine(dottedPen, new Point(0, y), new Point(canvasWidth, y));
        }
    }

    public Task HandleAsync(GridChangeEvent message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}