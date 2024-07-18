using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TerminalBoard.App.UIComponents
{
    public class GridCanvas : Canvas
    {
        public static readonly DependencyProperty ShowGridProperty =
            DependencyProperty.Register(
            nameof(ShowGrid), typeof(bool), typeof(GridCanvas),
            new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.AffectsRender));

        public bool ShowGrid
        {
            get => (bool)GetValue(ShowGridProperty);
            set => SetValue(ShowGridProperty, value);
        }

        public static readonly DependencyProperty GridSpacingProperty =
            DependencyProperty.Register(
            nameof(GridSpacing), typeof(double), typeof(GridCanvas),
            new FrameworkPropertyMetadata(15, FrameworkPropertyMetadataOptions.AffectsRender));

        public double GridSpacing
        {
            get => (double)GetValue(GridSpacingProperty);
            set => SetValue(GridSpacingProperty, value);
        }

        public static readonly DependencyProperty GridColorProperty =
            DependencyProperty.Register(
            nameof(GridColor), typeof(Color), typeof(GridCanvas),
            new FrameworkPropertyMetadata(Colors.LightSkyBlue, FrameworkPropertyMetadataOptions.AffectsRender));

        public Color GridColor
        {
            get => (Color)GetValue(GridColorProperty);
            set => SetValue(GridColorProperty, value);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (ShowGrid)
            {

            }

        }
    }
}