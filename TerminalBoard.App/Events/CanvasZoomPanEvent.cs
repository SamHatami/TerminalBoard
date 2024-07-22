namespace TerminalBoard.App.Events;

public class CanvasZoomPanEvent(double x, double y)
{
    public double Dx { get; } = x;
    public double Dy { get; } = y;
}