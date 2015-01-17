using Common.Logging;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyApp.Wpf.GameOfLife.Converter
{
    public class BitmapConverter : IValueConverter
    {
        private static readonly ILog logger = Common.Logging.LogManager.GetLogger<BitmapConverter>();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            logger.Debug("Start creating bitmap.");

            var gameBoard = (GameBoard.GameBoard)value;

            if (gameBoard.GridSize.IsEmpty)
                return null;

            var bitmap = GenerateBitmap(gameBoard);

            logger.Debug("Done in creating bitmap.");

            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private RenderTargetBitmap GenerateBitmap(GameBoard.GameBoard gameBoard)
        {
            // 100 pixel for each unit
            var unitSize = new System.Drawing.Size(8, 8);
            var bitmapWidth = gameBoard.GridSize.Width * unitSize.Width;
            var bitmapHeight = gameBoard.GridSize.Height * unitSize.Height;

            var brush = new SolidColorBrush(Colors.Green);
            var pen = new System.Windows.Media.Pen(brush, 0);

            var drawingVisual = new DrawingVisual();

            logger.Debug("Start Drawing");

            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(
                    new SolidColorBrush(Colors.White),
                    null,
                    new Rect(0, 0, bitmapWidth, bitmapHeight));

                logger.Debug("Start the loop");

                for (var i = 0; i < gameBoard.Units.Count; i++)
                {
                    if (!gameBoard.Units[i].IsAlive)
                        continue;

                    var startPoint = GetStartPoint(gameBoard, i, unitSize);
                    drawingContext.DrawRectangle(
                        brush,
                        pen,
                        new Rect(startPoint.X, startPoint.Y, unitSize.Width+1, unitSize.Height+1));
                }

                logger.Debug("done with the loop");

                drawingContext.Close();

                logger.Debug("closing context");

                var bitmap = new RenderTargetBitmap(bitmapWidth, bitmapHeight, 120, 96, PixelFormats.Pbgra32);

                logger.Debug("creating RenderTargetBitmap instance");
                
                bitmap.Render(drawingVisual);

                logger.Debug("Rendering bitmap");

                return bitmap;
            }
        }

        private System.Drawing.Point GetStartPoint(GameBoard.GameBoard gameBoard, int unitIndex, System.Drawing.Size unitSize)
        {
            var unit = gameBoard.Units[unitIndex];
            var x = (unitIndex % gameBoard.GridSize.Width) * unitSize.Width;
            var y = System.Convert.ToInt32(Math.Truncate(System.Convert.ToDouble(unitIndex / gameBoard.GridSize.Width))) * unitSize.Height;
            return new System.Drawing.Point(x, y);
        }
    }
}
