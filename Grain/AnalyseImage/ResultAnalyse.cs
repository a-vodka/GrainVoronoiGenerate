using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.AnalyseImage
{
       class ResultAnalyse
    {
        public  int WhitePixels { get; set; } // Светлые пиксели на картинке
        public  int BlackPixels { get; set; } // Темные пиксели на картинке
        public  List<int> SizesOfXElements { get; set; } // Размеры зерен по линиям вдоль оси Х
        public  List<int> SizesOfYElements { get; set; } // Размеры зерен по линиям вдоль оси У
        public int GrainsCountX { get; set; } // Кол-во зерен
        public int GrainsCountY { get; set; } // Кол-во зерен
        public  double AveSize { get; set; }  // Средний размер зерна      

        public ResultAnalyse()
        {
            WhitePixels = new int();
            BlackPixels = new int();
            SizesOfXElements = new List<int>();
            SizesOfYElements = new List<int>();
            GrainsCountX = new int();
            GrainsCountY = new int();
            AveSize = new double();
        } 
    }
}
