using System;
using System.Drawing;

namespace BezierCurve
{
	/// <summary>
	/// 
	/// </summary>
	public class BezierCurve
	{
		public int		NumControlPoints = 6;
		Point	[]ControlPoints;

		float	t_inc = 0.001f;

		public BezierCurve()
		{
			ControlPoints = new Point[NumControlPoints];
		}

		public void SetControlPoint(Point pt, int i)
		{
			ControlPoints[i] = pt;
		}

		private float Factorial(int n)
		{
			float res = 1.0f;
			for (int i=2; i<=n; i++)
				res *= i;

			return res;
		}

		private float C(int n,int i)
		{
			float res = Factorial(n) / (Factorial(i) * Factorial(n-i));
			return res;
		}

		private double Calc_B(float t,int i)
		{
			int n = ControlPoints.Length-1;
			double res = C(n,i) * Math.Pow((1-t),(n-i)) * Math.Pow(t, i);
			return res;
		}

		private PointF CalcCurvePointAtTime(float t)
		{
			PointF pt = new PointF();
			for (int i=0; i<ControlPoints.Length; i++)
			{
				double B = Calc_B(t,i);
				pt.X += (float)(B * ControlPoints[i].X);
				pt.Y += (float)(B * ControlPoints[i].Y);
			}
			
			return pt;
		}

		private void DrawControlPoints(Graphics g)
		{
			for (int i=0; i<ControlPoints.Length; i++)
			{
				g.DrawEllipse(new Pen(Color.Blue), ControlPoints[i].X, ControlPoints[i].Y, 5,5);
			}
		}

		public int ChangeCtrlPoint(int XMouse, int YMouse)
		{
			Rectangle rc;
			for (int i=0; i<ControlPoints.Length; i++)
			{
				rc = new Rectangle(ControlPoints[i].X, ControlPoints[i].Y, 5,5);
				if (XMouse >= rc.Left && XMouse <= rc.Right && YMouse >= rc.Top && YMouse <= rc.Bottom)
				{
					return i;
				}
			}
			return -1;
		}

		public void ModifyCtrlPoint(int i , int XMouse, int YMouse)
		{
			ControlPoints[i].X =  XMouse;
			ControlPoints[i].Y =  YMouse;
		}
		
		private void DrawCurvePoints(Graphics g)
		{
			PointF	curvePoint;
			for (float t=0.0f; t<=1.0; t+=t_inc)
			{
				curvePoint = CalcCurvePointAtTime(t);
				g.DrawLine(new Pen(Color.Red), curvePoint.X, curvePoint.Y, curvePoint.X+1, curvePoint.Y);
			}
		}

		public void DrawCurve(Graphics g)
		{
			DrawControlPoints(g);
			DrawCurvePoints(g);
		}
	}
}
