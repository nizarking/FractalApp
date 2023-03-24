using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas canvas;
        Vector originPoint;
        double lineAngle;
        static double branchLengthMultiplayer = 0.75;
        int colorVar = 0;


        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("hellow world");
            canvas = (Canvas)FindName("myCanvas");
            canvas.Background = Brushes.Gray;
            originPoint = new Vector(canvas.Width / 2, canvas.Height);

        }
        void slider_valueChanged(Object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (canvas == null) return;
            Console.WriteLine("angle" + Math.Floor(((Slider)sender).Value));
            int lineLength = 100;
            lineAngle = e.NewValue;
            canvas.Children.Clear();
            colorVar = 0;
            Console.WriteLine("line angle" + lineAngle);
            Vector lineEndPoint = drawMainLine(originPoint.X, originPoint.Y, 0, lineLength, 5);
            drawLine(lineEndPoint.X, lineEndPoint.Y, lineAngle, lineLength * branchLengthMultiplayer, 5);
            drawLine(lineEndPoint.X, lineEndPoint.Y, -lineAngle, lineLength * branchLengthMultiplayer, 5);

        }
        private Vector drawMainLine(double x, double y, double angle, double length, double strock)
        {
            double radianAngle = DegreesToRadians(angle);
            double x2 = length * Math.Sin(radianAngle);
            double y2 = length * Math.Cos(radianAngle);
            x2 = x2 + originPoint.X;
            y2 = -y2 + originPoint.Y;

            drawLine2Points(x, y, x2, y2, strock);
            return new Vector(x2, y2);
        }
        private void drawLine(double x, double y, double angle, double length, double strock)
        {
            double radianAngle = DegreesToRadians(angle);
            double x2 = length * Math.Sin(radianAngle);
            double y2 = length * Math.Cos(radianAngle);
            x2 = x2 + x;
            y2 = y - y2;

            drawLine2Points(x, y, x2, y2, strock);
            if (length > 6)
            {
                drawLine(x2, y2, angle + lineAngle, length * branchLengthMultiplayer, strock * 0.7);
                drawLine(x2, y2, angle - lineAngle, length * branchLengthMultiplayer, strock * 0.7);
            }

        }


        double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
        private void drawLine2Points(double x1, double y1, double x2, double y2, double strock)
        {
            colorVar++;
            Line l = new Line();

            if (colorVar < 5) l.Stroke = new SolidColorBrush(Color.FromRgb(0, 255, 00));
            else if (colorVar < 100) l.Stroke = new SolidColorBrush(Color.FromRgb(0, 00, 255));
            else if (colorVar < 200) l.Stroke = new SolidColorBrush(Color.FromRgb(255,00,00));
            else if (colorVar < 400) l.Stroke = new SolidColorBrush(Color.FromRgb(200, 00, 00));
            else if (colorVar < 800) l.Stroke = new SolidColorBrush(Color.FromRgb(200, 200, 00));
            else if (colorVar < 1600) l.Stroke = new SolidColorBrush(Color.FromRgb(200, 00, 200));
            else if (colorVar < 1600*2) l.Stroke = Brushes.Red;
            else if (colorVar < 1600*2*2) l.Stroke = Brushes.Red;

            l.StrokeThickness = strock;
            l.X1 = x1;
            l.Y1 = y1;
            l.X2 = x2;
            l.Y2 = y2;

            canvas.Children.Add(l);
        }
    }
}
