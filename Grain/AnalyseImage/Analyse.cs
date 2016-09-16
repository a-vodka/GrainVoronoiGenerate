using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.Drawing;
using System.Windows;
using System.Runtime.InteropServices;

namespace Grain.AnalyseImage
{
    class Analyse
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();
        public Analyse()
        { }       
        private Image<Bgr, Byte> DrowLinesSimpleX(UserData data)
        {       
            var img = new Image<Bgr, Int32>(data.Source.UriSource.LocalPath);
            var imageGray = new Image<Gray, byte>(img.Bitmap);
            var imageColor = new Image<Bgr, byte>(imageGray.Bitmap);
            var size = 0;
            var colorCircle = new Bgr(data.CirclesColor);
            var colorLine = new Bgr(data.LinesColor);
            try
            {
                for (var i = 0; i < data.Source.PixelHeight - 1; i += data.Source.PixelHeight / data.Lines)
                {
                    for (var j = 0; j < data.Source.PixelWidth - 1; j++)
                    {
                        if (imageColor[i, j].Red > data.ColorGridSensitive) 
                        {
                            size++;
                        }
                        else if (size != 0 && size >= data.MinSizeGrainPerc * data.Source.PixelWidth/100 && size <= data.MaxSizeGrainPerc * data.Source.PixelWidth/100)
                        {
                            var point = new PointF(j, i);
                            imageColor.Draw(new CircleF(point, data.CircleRad), colorCircle, data.LineThickness);
                            size = 0;
                        }
                        imageColor[i, j] = colorLine;
                        //var point2 = new PointF(j, i);
                        //var cross2df = new Cross2DF(point2, data.Source.PixelWidth / (20 * data.Lines), data.Source.PixelWidth / (10 * data.Lines));
                        //imageColor.Draw(cross2df, colorLine, 1);
                    }
                }
                //ImageX = imageColor;
                return imageColor;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private Image<Bgr, Byte> DrowLinesSimpleY(UserData data)
        {
            var img = new Image<Bgr, Int32>(data.Source.UriSource.LocalPath);
            var imageGray = new Image<Gray, byte>(img.Bitmap);
            var imageColor = new Image<Bgr, byte>(imageGray.Bitmap);
            var size = 0;
            var colorCircle = new Bgr(data.CirclesColor);
            var colorLine = new Bgr(data.LinesColor);
            try
            {
                for (var i = 0; i < data.Source.PixelWidth - 1; i += data.Source.PixelWidth / data.Lines)
                {
                    for (var j = 0; j < data.Source.PixelHeight - 1; j++)
                    {
                        if (imageColor[j, i].Red > data.ColorGridSensitive)
                        {
                            size++;
                        }

                        else if (size != 0 && size >= data.MinSizeGrainPerc *  data.Source.PixelWidth/100 && size <= data.MaxSizeGrainPerc * data.Source.PixelWidth/100)
                        {
                            var point = new PointF(i, j);
                            imageColor.Draw(new CircleF(point, data.CircleRad), colorCircle, data.LineThickness);
                            size = 0;
                        }
                        //var point2 = new PointF(i, j);
                        //var cross2df = new Cross2DF(point2, data.Source.PixelWidth / (20 * data.Lines), data.Source.PixelWidth / (20 * data.Lines));
                        //imageColor.Draw(cross2df, colorLine, 1);
                        imageColor[j, i] = colorLine;
                    }
                }
              
                
                return imageColor;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public BitmapSource DrowGrid(UserData data)
        {
            var imageX = DrowLinesSimpleX(data);
            var imageY = DrowLinesSimpleY(data);
            var imageGrid = imageX.And(imageY);
            return BitmapSourceConvert.ToBitmapSource(imageGrid);
        }

        private ResultAnalyse AnalyseXDirection(UserData data)
        {
            var resultAnalyse = new ResultAnalyse();
            var img = new Image<Bgr, Int32>(data.Source.UriSource.LocalPath);
            var imageGray = new Image<Gray, byte>(img.Bitmap);
            var imageColor = new Image<Bgr, byte>(imageGray.Bitmap);
            var size = 0;
            var colorCircle = new Bgr(data.CirclesColor);
            var colorLine = new Bgr(data.LinesColor);
           
            try
            {
                for (var i = 0; i < data.Source.PixelHeight - 1; i += data.Source.PixelHeight / data.Lines)
                {
                    for (var j = 0; j < data.Source.PixelWidth - 1; j++)
                    {
                        if (imageColor[i, j].Red > data.ColorGridSensitive)
                        {
                            size++;
                        }
                        else if (size != 0 && size >= data.MinSizeGrainPerc * data.Source.PixelWidth / 100 && size <= data.MaxSizeGrainPerc * data.Source.PixelWidth / 100)
                        {
                            var point = new PointF(j, i);
                            imageColor.Draw(new CircleF(point, data.CircleRad), colorCircle, data.LineThickness);
                            resultAnalyse.SizesOfXElements.Add(size);
                            
                            size = 0;
                        }
                        imageColor[i, j] = colorLine;
                        //var point2 = new PointF(j, i);
                        //var cross2df = new Cross2DF(point2, data.Source.PixelWidth / (20 * data.Lines), data.Source.PixelWidth / (10 * data.Lines));
                        //imageColor.Draw(cross2df, colorLine, 1);
                    }
                }
                return resultAnalyse;         
            }           
            catch (Exception e)
            {              
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private ResultAnalyse AnalyseYDirection(UserData data)
        {
            var resultAnalyse = new ResultAnalyse();
            var img = new Image<Bgr, Int32>(data.Source.UriSource.LocalPath);
            var imageGray = new Image<Gray, byte>(img.Bitmap);
            var imageColor = new Image<Bgr, byte>(imageGray.Bitmap);
            var size = 0;
            var colorCircle = new Bgr(data.CirclesColor);
            var colorLine = new Bgr(data.LinesColor);
            
            try
            {
                for (var i = 0; i < data.Source.PixelWidth - 1; i += data.Source.PixelWidth / data.Lines)
                {
                    for (var j = 0; j < data.Source.PixelHeight - 1; j++)
                    {
                        if (imageColor[j, i].Red > data.ColorGridSensitive)
                        {
                            size++;
                        }

                        else if (size != 0 && size >= data.MinSizeGrainPerc * data.Source.PixelWidth / 100 && size <= data.MaxSizeGrainPerc * data.Source.PixelWidth / 100)
                        {
                            var point = new PointF(i, j);
                            imageColor.Draw(new CircleF(point, data.CircleRad), colorCircle, data.LineThickness);
                            resultAnalyse.SizesOfXElements.Add(size);
                            
                            size = 0;
                        }
                        //var point2 = new PointF(i, j);
                        //var cross2df = new Cross2DF(point2, data.Source.PixelWidth / (20 * data.Lines), data.Source.PixelWidth / (20 * data.Lines));
                        //imageColor.Draw(cross2df, colorLine, 1);
                        imageColor[j, i] = colorLine;
                    }
                }
                return resultAnalyse;    
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return resultAnalyse;
            }
        }

        public ResultAnalyse AnalyseGrid(UserData data)
        {
            var result1 = AnalyseXDirection(data);
            var result2 = AnalyseYDirection(data);
            var resultFinal = new ResultAnalyse();
            resultFinal.GrainsCountX = result1.SizesOfXElements.Count/(data.Lines/2);
            resultFinal.GrainsCountY = result2.SizesOfYElements.Count/(data.Lines/2);
            return resultFinal;

        }

        public void ShowResult(ResultAnalyse resultAnalyse)
        {
            AllocConsole();
            foreach (var size in resultAnalyse.SizesOfXElements)
            {
                Console.WriteLine(size.ToString());
            }

            AllocConsole();
            foreach (var size in resultAnalyse.SizesOfYElements)
            {
                Console.WriteLine(size.ToString());
            }
        }
    }
 }


