using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Grain.AnalyseImage;
using Microsoft.Win32;
using Emgu.CV.Structure;
using Emgu.CV;
using Grain.GenerateGrain;

namespace Grain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var source = new BitmapImage(new Uri(op.FileName));
                AnalyseSource.Source = source;                                       
                imgPhoto.Source = source;                                         
            }                  
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var userData = new UserData(AnalyseSource.Source,
                                        Convert.ToInt16(textBox4.Text),
                                        System.Drawing.Color.Black,
                                        System.Drawing.Color.Red,
                                        2,
                                        3,
                                        Convert.ToDouble(textBox.Text),
                                        Convert.ToDouble(textBox3.Text),
                                        (byte)slider.Value);
            var analyse = new Analyse();
            var result = analyse.AnalyseGrid(userData);
            //analyse.ShowResult(result);
            
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            var userData = new UserData(AnalyseSource.Source,
                                       Convert.ToInt16(textBox4.Text),
                                       System.Drawing.Color.Black,
                                       System.Drawing.Color.Red,
                                       2,
                                       3,
                                       Convert.ToDouble(textBox.Text),
                                       Convert.ToDouble(textBox3.Text),
                                       (byte)slider.Value);
            var analyse = new Analyse();
            var result = analyse.AnalyseGrid(userData);
            //analyse.ShowResult(result);

            var generate = new GenerateVoronoi(result.GrainsCountX,
                                               result.GrainsCountY,
                                               AnalyseSource.Source.PixelWidth,
                                               AnalyseSource.Source.PixelHeight);
            Voronoi.Children.Clear();
            generate.GenerateVoronoiPoints(Voronoi);
            generate.ExportToPng(new Uri("d:/ToUse.png"), Voronoi);
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            var userData = new UserData(AnalyseSource.Source,
                                       Convert.ToInt16(textBox4.Text),
                                       System.Drawing.Color.Black,
                                       System.Drawing.Color.Red,
                                       2,
                                       3,
                                       Convert.ToDouble(textBox.Text),
                                       Convert.ToDouble(textBox3.Text),
                                       (byte)slider.Value);
            var analyse = new Analyse();
            imgPhoto.Source = analyse.DrowGrid(userData);
            MessageBox.Show("Done!");
        }
    }
}
