using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BezierCurve
{
    class Circle
    {

        public double XC, YC, Rad;
        public int inC = 1;
        public int StartTh = 0;
        public int EndTh = 360;
        public Line L1;
        public Line L2;
        public void DrawYourSelf(Graphics g)
        {
            float x, y , xOuter=0 , yOuter=0;
            float thRadian, thRadianOuter;
            int i=0;
            for (int th = StartTh; th < EndTh; th += inC, i++)
            {
                thRadian = (float)(th * Math.PI / 180);
                x = (float)(Rad * Math.Cos(thRadian) + XC);
                y = (float)(Rad * Math.Sin(thRadian) + YC);
                g.FillEllipse(Brushes.Black, x - 5, y - 5, 10, 10);
            }
        }
        
    }
}
