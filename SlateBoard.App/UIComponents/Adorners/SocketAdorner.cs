using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace SlateBoard.App.UIComponents.Adorners
{
    public class Socketadorner : Adorner
    {
        public Socketadorner(UIElement adornedElement) : base(adornedElement)
        {
            
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
    }
}