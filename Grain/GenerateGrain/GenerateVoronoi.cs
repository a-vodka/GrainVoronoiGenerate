using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Voronoi2;

namespace Grain.GenerateGrain
{
    class GenerateVoronoi
    {
        private int CentresX { get; set; }
        private int CentresY { get; set; }
        private System.Windows.Point[] Points { get; set; }
        private int ResX { get; }
        private int ResY { get; }

        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        public GenerateVoronoi(int centresX, int centresY, int resX, int resY)
        {
            CentresX = centresX;
            CentresY = centresY;
            ResX = resX;
            ResY = resY;
        }

        public void GenerateVoronoiPoints(Canvas canvas)
        {
            var xVal = new double[CentresX];
            var yVal = new double[CentresY];
            var voronoiObject = new Voronoi(0.1);
            var random = new Random();    
            var VoronoiCounts = 0;
            var xValVoronoi = new double[CentresX * CentresY];
            var yValVoronoi = new double[CentresY * CentresX];

            var strideX = 0;
            var strideY = 0;
            //for (var i = 0; i < CentresX; i++)
            //{
            //    for (var j = 0; j < CentresY; j++)
            //    {

            //    }
            //}
            for (int i = 0; i < CentresX; i++)
            {
                xVal[i] = random.Next(strideX, strideX + ResX / CentresX);
                strideX += ResX / CentresX;   
                      
                for (var j = 0; j < CentresY; j++) 
                {                
                    yVal[j] = random.Next(strideY, strideY + ResY / CentresY);
                    strideY += ResY / CentresY;            

                    xValVoronoi[VoronoiCounts] = xVal[i];
                    yValVoronoi[VoronoiCounts] = yVal[j];
                    VoronoiCounts++;
                }
                strideY = 0;              
             }

            //AllocConsole();
            //foreach (var e in yValVoronoi)
            //{
            //    Console.WriteLine(e);
            //}

            var ge = voronoiObject.generateVoronoi(xValVoronoi, yValVoronoi, 0.1, ResX, 0.1, ResY);

            Points = new System.Windows.Point[2];
            canvas.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            for (var i = 0; i < ge.Count; i++)
            {
                Points[0] = new System.Windows.Point(ge[i].x1, ge[i].y1);
                Points[1] = new System.Windows.Point(ge[i].x2, ge[i].y2);
                VoronoiEdges(canvas);
            }
            canvas.Width = ResX;
            canvas.Height = ResY;
            ExportToPng(new Uri("d:\\ToUse.png"), canvas);
            ExportToPng(new Uri("d:\\Voronoi.png"), canvas);
        }

        public void VoronoiEdges(Canvas canvas)
        {
            Polyline line = new Polyline();
            PointCollection collection = new PointCollection();
            foreach (System.Windows.Point p in Points)
            {
                collection.Add(p);
            }
            line.Points = collection;
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.StrokeThickness = 4;
            line.Fill = new SolidColorBrush(Colors.White);

            //canvas.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            canvas.Children.Add(line);
        }

        public void ExportToPng(Uri path, Canvas surface)
        {
            if (path == null) return;

            // Save current canvas transform
            Transform transform = surface.LayoutTransform;
            // reset current transform (in case it is scaled or rotated)
            surface.LayoutTransform = null;

            // Get the size of canvas
            Size size = new Size(surface.Width, surface.Height);
            // Measure and arrange the surface
            // VERY IMPORTANT
            surface.Measure(size);
            surface.Arrange(new Rect(size));

            // Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(surface);

            // Create a file stream for saving image
            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                // Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
                
            }

            // Restore previously saved layout
            surface.LayoutTransform = transform;
        }
    }
}
