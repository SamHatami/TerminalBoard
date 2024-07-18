using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TerminalBoard.App.Events
{
    public class CanvasZoomPanEvent
    {
        public CanvasZoomPanEvent(double x, double y)
        {
            dX = x;
            dY = y;
        }

        public TransformGroup TransformGroup { get; }
        public double dX { get; }
        public double dY { get; }
    }
}
