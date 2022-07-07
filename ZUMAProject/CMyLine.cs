using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BezierCurve
{
   public class CMyLine
    {
        public float xs, ys, xe, ye, dx, dy, m, invM , currX, currY;
        public int Speed = 15;
        public int Color;
        public int aim_mode = 0;
       public int counter = 0;
        
        public void SetVals(float a, float b, float c, float d)
        {
            xs = a;
            ys = b;
            xe = c;
            ye = d;
            //////////////////
            dx = xe - xs;
            dy = ye - ys;
            m = dy / dx;
            invM = dx / dy;
            /////////////////
            currX = xs;
            currY = ys;
        }

      
        public void MoveStep()
        {
            
           
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    if (xs < xe)
                    {
                        currX += Speed;
                        currY += m * Speed;

                    }
                    else
                    {
                        currX -= Speed;
                        currY -= m * Speed;

                    }
                }
                else
                {
                    if (ys < ye)
                    {
                        currY += Speed;
                        currX += invM * Speed;

                    }
                    else
                    {
                        currY -= Speed;
                        currX -= invM * Speed;

                    }
                }
            
        }
               
        public void DrawYourCurrPos(Graphics g)
        {
            if (Color == 0)
            {
                g.FillEllipse(Brushes.Blue, currX - 5, currY - 5, 50, 50);
            }
            if (Color == 1)
            {
                g.FillEllipse(Brushes.Green, currX - 5, currY - 5, 50 , 50);
            }
            if (Color == 2)
            {
                g.FillEllipse(Brushes.Crimson, currX - 5, currY - 5, 50, 50);
            }
        }
    }
}
