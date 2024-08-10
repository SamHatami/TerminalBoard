using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TerminalBoard.App.UIComponents
{
    public class GridCanvas : Canvas
    {
        private readonly Brush _lineBrush = Brushes.LightGray;
        private readonly Pen _linePen;
        private DrawingContext _drawingContext;

        public static readonly DependencyProperty ShowGridProperty =
            DependencyProperty.Register(
                nameof(ShowGrid), typeof(bool), typeof(GridCanvas),
                new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.AffectsRender, OnShowGridChanged));

        public bool ShowGrid
        {
            get => (bool)GetValue(ShowGridProperty);
            set => SetValue(ShowGridProperty, value);
        }

        public static readonly DependencyProperty GridSpacingProperty =
            DependencyProperty.Register(
                nameof(GridSpacing), typeof(double), typeof(GridCanvas),
                new FrameworkPropertyMetadata((double)15f, FrameworkPropertyMetadataOptions.AffectsRender, OnGridSettingsChanged));

        public double GridSpacing
        {
            get => (double)GetValue(GridSpacingProperty);
            set => SetValue(GridSpacingProperty, value);
        }

        public static readonly DependencyProperty GridColorProperty =
            DependencyProperty.Register(
                nameof(GridColor), typeof(Color), typeof(GridCanvas),
                new FrameworkPropertyMetadata(Colors.LightSkyBlue, FrameworkPropertyMetadataOptions.AffectsRender, OnGridSettingsChanged));

        public Color GridColor
        {
            get => (Color)GetValue(GridColorProperty);
            set => SetValue(GridColorProperty, value);
        }

        private Image _gridImage;

        public GridCanvas()
        {
            _gridImage = new Image
            {
                Stretch = Stretch.None,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            RenderOptions.SetBitmapScalingMode(_gridImage, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(_gridImage, EdgeMode.Aliased);

            Children.Add(_gridImage);
        }

        private static void OnShowGridChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GridCanvas gridCanvas )
            {
                if(gridCanvas.ShowGrid)
                    gridCanvas.CreateGridImage();
                else
                {
                    gridCanvas._gridImage.Source = null;
                }
            }
        }

        private static void OnGridSettingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GridCanvas gridCanvas)
            {
                gridCanvas.CreateGridImage();
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            _drawingContext = dc;
            if (ShowGrid)
            {
                CreateGridImage();
            }
        }


        private void CreateGridImage() //Inte riktigt min egen kod :)
        {
            int width = 2600;
            int height = 2600;

            if (width <= 0 || height <= 0 || GridSpacing<=0 ) return;

            WriteableBitmap wb = new WriteableBitmap(
                width,
                height,
                96, 96,
                PixelFormats.Bgra32,
                null
            );

            _gridImage.Source = wb;

            Int32Rect rect = new Int32Rect(0, 0, 2, 2);
            int size = rect.Width * rect.Height * 4;
            byte[] pixels = new byte[size];

            // Setup the pixel array
            for (int i = 0; i < rect.Height * rect.Width; ++i)
            {
                pixels[i * 4 + 0] = GridColor.B;   // Blue
                pixels[i * 4 + 1] = GridColor.G;   // Green
                pixels[i * 4 + 2] = GridColor.R;   // Red
                pixels[i * 4 + 3] = GridColor.A;   // Alpha
            }

            int step = (int)GridSpacing;
            for (int r = 0; r < height; r += step)
            {
                for (int c = 0; c < width; c += step)
                {
                    rect.X = c;
                    rect.Y = r;
                    wb.WritePixels(rect, pixels, rect.Width * 4, 0);
                }
            }
        }
    }
}
