using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SlateBoard.App.Events
{
    public class CanvasZoomPanEvent
    {
        public CanvasZoomPanEvent(double x, double y)
        {
            X = x;
            Y = y;
        }

        public TransformGroup TransformGroup { get; }
        public double X { get; }
        public double Y { get; }
    }
}
