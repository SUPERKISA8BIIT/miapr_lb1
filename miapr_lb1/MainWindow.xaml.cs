using miapr_lb1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static miapr_lb1.Pixelcolor;
using Point = System.Windows.Point;

namespace paint
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Point> pixels;
        List<Point> ancers;
        Dictionary<int, List<Point>> classifiedPoints;

        private WriteableBitmap _bitmap;
        public MainWindow()
        {
            InitializeComponent();

            ClearAll();
        }      

        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            Generate();
        }

        private void Algorythm_Button_Click(object sender, RoutedEventArgs e)
        {
            KMeans();
        }

        private void Generate()
        {
            Random rnd = new Random();
            for (int i = 0; i < pixels.Capacity; i++)
            {
                int x = rnd.Next(0, 799);
                int y = rnd.Next(0, 449);

                _bitmap.AddPixel(PixelColors.Pink, x, y);
                _bitmap.AddPixel(PixelColors.Pink, x + 1, y);
                _bitmap.AddPixel(PixelColors.Pink, x, y + 1);
                _bitmap.AddPixel(PixelColors.Pink, x + 1, y + 1);

                pixels.Add(new Point(x, y));
            }

            for (int i = 0; i < ancers.Capacity; i++)
            {
                int x = rnd.Next(0, 799);
                int y = rnd.Next(0, 449);
                ancers.Add(new Point(x, y));
                _bitmap.AddRect(PixelColors.Empty[i], x, y, 4);
                classifiedPoints[i] = new List<Point>();
            }
        }

        private void KMeans()
        {
            foreach (var pixel in pixels)
            {
                var distance = ancers.Select(x => DistanceBeetweenPoints(x, pixel)).ToList();
                int idOfMinDistance = distance.IndexOf(distance.Min());
                classifiedPoints[idOfMinDistance].Add(pixel);

                _bitmap.AddPixel(PixelColors.Empty[idOfMinDistance], (int)pixel.X, (int)pixel.Y);
                _bitmap.AddPixel(PixelColors.Empty[idOfMinDistance], -~(int)pixel.X, (int)pixel.Y);
                _bitmap.AddPixel(PixelColors.Empty[idOfMinDistance], (int)pixel.X, -~(int)pixel.Y);
                _bitmap.AddPixel(PixelColors.Empty[idOfMinDistance], -~(int)pixel.X, -~(int)pixel.Y);
            }

            foreach (var ancer in ancers)
            {
                _bitmap.AddRect(PixelColors.Black, (int)ancer.X, (int)ancer.Y, 4);
            }
        }

        private void ClearAll()
        {
            pixels = new List<Point>(int.Parse(shapes.Text));
            ancers = new List<Point>(int.Parse(algorythm.Text));
            classifiedPoints = new Dictionary<int, List<Point>>();

            _bitmap = new WriteableBitmap((int)image.Width, (int)image.Height, 96, 96, PixelFormats.Bgr32, null);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    _bitmap.AddPixel(PixelColors.White, i, j);
                }
            }
            image.Source = _bitmap;
        }

        private static double DistanceBeetweenPoints(Point a, Point b)
        {
            var delta = a - b;
            return delta.X * delta.X + delta.Y * delta.Y;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Point> prevAncers;
            do
            {
                prevAncers = new List<Point>(ancers); //Запоминаем прошлые ядра

                for (int i = 0; i < ancers.Capacity; i++) //Очищаем прошлую классификацию
                    classifiedPoints[i] = new List<Point>();

                foreach (var pixel in pixels)
                {
                    var distance = ancers.Select(x => DistanceBeetweenPoints(x, pixel)).ToList();
                    int idOfMinDistance = distance.IndexOf(distance.Min());
                    classifiedPoints[idOfMinDistance].Add(pixel);
                }

                foreach (var pair in classifiedPoints)
                {
                    //Находим центр массы точек класса, путём нахождения среднего арифметического (сумма точек/кол-во точек)
                    var mean = pair.Value.Aggregate(new Point(), (a, b) => new Point(a.X + b.X, a.Y + b.Y));
                    mean = new Point((int)mean.X / pair.Value.Count, (int)mean.Y / pair.Value.Count);
                    ancers[pair.Key] = mean;
                }

            } while (!prevAncers.SequenceEqual(ancers)); //Проверяем совпали ли прошлые точки с новыми


            //Отрисовка результат
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    _bitmap.AddPixel(PixelColors.White, i, j);
                }
            }

            foreach (var pair in classifiedPoints)
            {
                foreach (var pixel in pair.Value)
                {
                    _bitmap.AddPixel(PixelColors.Empty[pair.Key], (int)pixel.X, (int)pixel.Y);
                    _bitmap.AddPixel(PixelColors.Empty[pair.Key], -~(int)pixel.X, (int)pixel.Y);
                    _bitmap.AddPixel(PixelColors.Empty[pair.Key], (int)pixel.X, -~(int)pixel.Y);
                    _bitmap.AddPixel(PixelColors.Empty[pair.Key], -~(int)pixel.X, -~(int)pixel.Y);
                }
            }

            foreach (var ancer in ancers)
            {
                _bitmap.AddRect(PixelColors.Black, (int)ancer.X, (int)ancer.Y, 4);
            }
            //
        }
    }
}
