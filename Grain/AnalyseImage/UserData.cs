using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Grain.AnalyseImage
{
    class UserData
    {
        protected internal  BitmapImage Source { get; set; }
        protected internal int Lines { get; set; }
        protected internal System.Drawing.Color LinesColor { get; set; }
        protected internal System.Drawing.Color CirclesColor { get; set; }
        protected internal int LineThickness { get; set; }
        protected internal int CircleRad { get; set; }
        protected internal double MinSizeGrainPerc { get; set; }
        protected internal double MaxSizeGrainPerc { get; set; }
        protected internal  byte ColorGridSensitive { get; set; }

        protected internal UserData(BitmapImage source,
                        int lines,
                        System.Drawing.Color linesColor,
                        System.Drawing.Color circlesColor,
                        int lineThickness,
                        int circleRadius,
                        double minSizeGrainPerc,
                        double maxSizeGrainPerc,
                        byte colorGridSensitive)
        {
            Source = source;
            Lines = lines;
            LinesColor = linesColor;
            CirclesColor = circlesColor;
            LineThickness = lineThickness;
            CircleRad = circleRadius;
            MinSizeGrainPerc = minSizeGrainPerc;
            MaxSizeGrainPerc = maxSizeGrainPerc;
            ColorGridSensitive = colorGridSensitive;
        }
    }
}
