using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BezierCurve
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>

 
    public class Bullet{
        public int X;
        public int Y;
           public int color;
    }
    public class Line
    {
        public float start_x;
        public float start_y;
        public float end_x;
        public float end_y;
    }

    public class Ball_Target {
        public float X;
     public float Y;
     public int Color;
     public float move_t = 0f;
     public void Increase_T(float val , BezierCurve obj)
     {
         move_t += val;
         PointF carPoint = obj.CalcCurvePointAtTime(move_t);
         this.X = carPoint.X;
         this.Y = carPoint.Y;
     }
     public int order;
     public int move_backwards = 0;
    }
    public class Frog
    {
        public int X;
        public int Y;
        public Bitmap im;
        public List<CMyLine> bl = new List<CMyLine>();
    }
	public class Form1 : System.Windows.Forms.Form
	{
		enum Modes{CTRL_POINTS, DRAG , SHOOT};
		BezierCurve	obj = new BezierCurve();
        float my_t_inForm = 0.5f;
        PointF carPoint;
        List<Ball_Target> BL = new List<Ball_Target>();
		int			numOfCtrlPoints=0;
        Modes CurrentMouseMode = Modes.CTRL_POINTS;
		int			indexCurrDragNode = -1;
		Bitmap		off;
        int t = 0;
        Frog fr = new Frog();
        int or = 0;
        private System.Windows.Forms.MainMenu mainMenu1;

        int count = 0;
        private IContainer components;
        Timer tt = new Timer();
        public Random rnd = new Random();
        public Random rnd_bullet = new Random();
        CMyLine lne = new CMyLine();
        Circle circ = new Circle();
        public float angle = 0;
        int flag_nope = 0;
        CMyLine l = new CMyLine();
        CMyLine ball_aim = new CMyLine();
        public Random Aim_Color = new Random();
        int randomized_aim;
        int curr_K = -5;
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
            this.Paint +=new PaintEventHandler(Form1_Paint);
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.Load += new EventHandler(Form1_Load);
            this.MouseDown +=new MouseEventHandler(Form1_MouseDown);
            this.MouseMove +=new MouseEventHandler(Form1_MouseMove);
            this.MouseUp +=Form1_MouseUp;
            tt.Tick += new EventHandler(tt_Tick);
            tt.Start();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
        int ct = 0;
      
        void tt_Tick(object sender, EventArgs e)
        {
            if (t% 4 == 0 && obj.ControlPoints.Count > 2)
            {




                  CreatenewBall();
                
                ct++;
            }
            MoveBalls();
            for (int i = 0; i < fr.bl.Count; i++)
            {
              
                    fr.bl[i].MoveStep();
                
            }
            CheckCollidingShoot();
                t++;
            DrawDubb(CreateGraphics());
        }

 

        void CheckCollidingShoot()
        {
            for (int i = 0; i < fr.bl.Count; i++)
            {
                float dx = fr.bl[i].dx;
                float dy = fr.bl[i].dy;
                float currX = fr.bl[i].currX;
                float currY = fr.bl[i].currY;
                float xs = fr.bl[i].xs;
                float xe = fr.bl[i].xe;
                float Speed = fr.bl[i].Speed;
                float m = fr.bl[i].m;
                float ys = fr.bl[i].ys;
                float ye = fr.bl[i].ye;
                float invM = fr.bl[i].invM;
                flag_nope = 0;

                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    if (xs < xe)
                    {

                        for (int j = 0; j < BL.Count; j++)
                        {
                            if (currX > BL[j].X &&
                               currX + 10 < BL[j].X + 50 &&
                                currY + 50 > BL[j].Y &&
                                currY < BL[j].Y + 10
                                )
                            {

                                int ct = fr.bl[i].Color;

                                Explosion(j, ct , 0);
                                if (flag_nope == 0)
                                {
                                    Shifting(j, ct);
                                }
                                fr.bl.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < BL.Count; j++)
                        {
                            if (currX > BL[j].X &&
                               currX + 10 < BL[j].X + 50 &&
                              currY < BL[j].Y + 50 &&
                               currY + 50 > BL[j].Y)
                            {

                                int ct = fr.bl[i].Color;
                               
                                Explosion(j, ct , 0);
                                if (flag_nope == 0)
                                {
                                    Shifting(j, ct);
                                }
                                fr.bl.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (ys < ye)
                    {
                        for (int j = 0; j < BL.Count; j++)
                        {
                            if (currX > BL[j].X &&
                               currX  < BL[j].X + 50 &&
                                currY + 50 > BL[j].Y && 
                                currY < BL[j].Y + 50)
                            {

                                int ct = fr.bl[i].Color;
                                Explosion(j, ct , 0);
                                if (flag_nope == 0)
                                {
                                    Shifting(j, ct);
                                }
                                fr.bl.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < BL.Count; j++)
                        {
                            if (currX > BL[j].X &&
                               currX < BL[j].X + 50 &&
                               currY < BL[j].Y + 50 &&
                               currY + 50 > BL[j].Y)
                            {
                                String v = " ";
                                if (BL[j].Color == 0)
                                {
                                    v = "Blue";
                                }
                                if (BL[j].Color == 1)
                                {
                                    v = "Green";
                                }
                                if (BL[j].Color == 2)
                                {
                                    v = "Crimson";
                                }

                                int ct = fr.bl[i].Color;

                                Explosion(j, ct , 0);
                                if (flag_nope == 0)
                                {
                                    Shifting(j, ct);
                                }
                                
                                fr.bl.RemoveAt(i);
                                
                                Console.WriteLine("HELLO" + v);

                                break;
                            }
                        }

                    }
                }
            }
        }
        void MoveBalls()
        {

           
            for (int i = 0; i < BL.Count; i++)
            {


                if (BL[i].move_backwards == 0)
                {
                    BL[i].Increase_T(0.002f, obj);
                }
                if (BL[i].move_backwards == 2)
                {
                    BL[i].Increase_T(-0.002f, obj);
                }
              
                    if (BL[i].move_t >= 1f)
                    {
                        BL.RemoveAt(i);
                    }
            }

            for (int j = 0; j < BL.Count - 1; j++)
            {
                if (BL[j].move_backwards == 0 && BL[j + 1].move_backwards == -1 && BL[j].move_t + 0.008f > BL[j + 1].move_t)
                {
                    BL[j + 1].move_backwards = 0;
                    BL[j + 1].move_t += 0.001f;
                }

            }
      
           
            for (int j = 0; j < BL.Count - 1; j++)
            {
                if (BL[j].move_backwards == 0 && BL[j + 1].move_backwards == 2 && BL[j].move_t + 0.008f > BL[j + 1].move_t)
                {

                    if (BL[j + 1].Color == BL[j + 2].Color)
                    {
                        Explosion(j + 1, BL[j + 1].Color, 0);
                    }
                    else
                    {
                        Explosion(j, BL[j].Color, 0);

                    }
                    break;



                }


            }

           
            


         
        }

      
        void CreatenewBall() {
           
            Ball_Target pnn = new Ball_Target();
            carPoint = obj.CalcCurvePointAtTime(pnn.move_t);
            pnn.X = carPoint.X;
            pnn.Y = carPoint.Y;
            pnn.Color = rnd.Next(3);
            pnn.order = or;
            or++;
            BL.Insert(0 , pnn);
        }
        void SaveCoordinates()
        {

            StreamWriter File = new StreamWriter("C://Users//20100//Desktop//ZUMA//Coordinates.txt");
            Random rnd = new Random();
            for (int i = 0; i < obj.ControlPoints.Count; i++)
            {
               
                int num = rnd.Next(4);
                File.Write(obj.ControlPoints[i].X + "," + obj.ControlPoints[i].Y + "\n");

            }
            File.Close();
        }

        void Shifting(int k , int ct)
        {
           

         
            Ball_Target pnn;


            pnn = new Ball_Target();
            pnn.X = BL[k].X;
            pnn.Y = BL[k].Y;
            pnn.move_t = BL[k].move_t;
            pnn.order = 9000;
            pnn.Color = ct;
            BL.Insert(k, pnn);

            int curr = BL.Count - 1;
            for (int i = curr; i > k; i--)
            {
                
                BL[i].move_t = BL[i].move_t + 0.008f;
               
            }

            

        }
        void Explosion(int k , int ct , int counter)
        {
            int count = counter;
            int end = k;
            int beg = k - 1;
            int count2 = 0;
            int cur_b = -1;
            int cur_e = -1;
            int starting;
            int ending;
            int flagger = 0;
            while (end < BL.Count)
            {

                if (BL[end].Color == ct)
                {
                    count++;
                    end++;
                }
                else if(BL[end].Color != ct)
                {
                    cur_e = end;
                    break;
                }
               
            }
            while (beg >= 0)
            {

                if (BL[beg].Color == ct)
                {
                    count++;
                    if (beg == 0)
                    {
                        cur_b = beg;
                        break;
                    }
                     beg--; 
                }
                else if (BL[beg].Color != ct)
                {
                    cur_b = beg;
                    break;
                }

            }



            if (cur_b != -1 && cur_e != -1)
            {
                int color = BL[cur_e].Color;
                for (int c = cur_b; c > 0; c--)
                {
                    if (BL[c].Color == color)
                    {
                        count2++;
                    }
                    else
                    {
                        break;
                    }
                }

            }
            if (cur_e != -1)
            {
                int color = BL[cur_e].Color;
                for (int f = cur_e; f < BL.Count; f++)
                {
                    if (BL[f].Color == color)
                    {
                        count2++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (cur_b != -1)
            {
                int color = BL[cur_b].Color;
                for (int c = cur_b; c > 0; c--)
                {
                    if (BL[c].Color == color)
                    {
                        count2++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

               

            
            
                if (count >= 2 && count2 <= 2)
                {
                    flag_nope = 1;

                    if (cur_b != 0 && BL[cur_b].Color != t)
                    {
                        for (int i = end; i < BL.Count; i++)
                        {
                            BL[i].move_backwards = -1;
                        }
                    }
                    else
                    {
                 
                            for (int i = end; i < BL.Count; i++)
                            {
                                BL[i].move_backwards = 0;
                            }
                        
                    }
                    starting = beg + count;
                    ending = beg + 1;
                    if (cur_b == 0 && BL[cur_b].Color == t) { starting--; ending = beg; }
                    for (int j =  starting; j >= ending; j--)
                    {
                        BL.RemoveAt(j);
                    }

                }
                else if (count >= 2 && count2 >= 2)
                {
                    flag_nope = 1;

                    if (cur_b != 0 && BL[cur_b].Color != t)
                    {
                        for (int i = end; i < BL.Count; i++)
                        {
                            BL[i].move_backwards = 2;
                        }
                    }
                    else
                    {
                        for (int i = end; i < BL.Count; i++)
                        {
                            BL[i].move_backwards = 2;
                        }
                    }
                    starting = beg + count;
                    ending = beg + 1;
                    if (cur_b == 0 && BL[cur_b].Color == t) { starting--; ending = beg; }
                    
                    for (int j = starting; j >= ending; j--)
                    {
                        BL.RemoveAt(j);
                    }
                }
           
                else
                {


                    for (int i = end; i < BL.Count; i++)
                    {
                        BL[i].move_backwards = 0;
                    }


                }
           
        }
        void CheckCollision()
        {

            for (int i = 0; i < BL.Count - 1; i++)
            {


                


               
                if (BL[i].X + 50 >= BL[i + 1].X &&
                    BL[i].X <= BL[i + 1].X + 50
                                    )
                {
                    BL[i+1].move_t += 0.001f;
                }
                if (BL[i + 1].Y >= BL[i].Y &&
                    BL[i + 1].Y <= BL[i].Y + 50)
                {
                    BL[i + 1].move_t += 0.001f;
                }
              if (BL[i].X  >= BL[i + 1].X &&
                   BL[i].X <= BL[i + 1].X + 50 &&
                    BL[i].X + 50 >= BL[i + 1].X + 50
                                   )
                {
                    BL[i + 1].move_t += 0.001f;
                }
               
               
         
                /*

                if (
                     BL[i].Y  > BL[i + 1].Y &&
                     BL[i].Y  < BL[i + 1].Y + 50)
                {
                    BL[i + 1].move_t += 0.001f;
                }
                  
                else        if (
                           BL[i].X  > BL[i + 1].X &&
                           BL[i].X  < BL[i + 1].X + 50)
                    {
                        BL[i + 1].move_t += 0.001f;
                    }
                else    if (
                       BL[i].Y + 50 > BL[i + 1].Y &&
                       BL[i].Y + 50 < BL[i + 1].Y + 50)
                    {
                        BL[i + 1].move_t += 0.001f;
                    }
                  
                else      if (BL[i + 1].X > BL[i].X &&
                        BL[i + 1].X < BL[i].X + 50)
                    {
                        BL[i + 1].move_t += 0.001f;
                    }
                 
                 */

                    carPoint = obj.CalcCurvePointAtTime(BL[i + 1].move_t);
                    BL[i + 1].X = carPoint.X;
                    BL[i + 1].Y = carPoint.Y;


                
            }
            
        }
        void Form1_Load(object sender, EventArgs e)
        {
       
            if (off == null)
            {
                off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            }
            CreateFrog();
            randomized_aim = Aim_Color.Next(3);
            ball_aim.Color = randomized_aim;
            FillCoordinates();
            CurrentMouseMode = Modes.SHOOT;
        }

      
        void CreateFrog()
        {
            fr.X = 500 + 250;
            fr.Y = 500 - 200;
            fr.im = new Bitmap("C://Users//20100//Desktop//ZUMA//frog.png");
            fr.im.MakeTransparent(fr.im.GetPixel(0, 0));
        }
        public Bitmap Rotate(Bitmap b, float angle)
        {
            int maxside = (int)(Math.Sqrt(b.Width * b.Width + b.Height * b.Height));

            //create a new empty bitmap to hold rotated image

            Bitmap returnBitmap = new Bitmap(maxside, maxside);

            //make a graphics object from the empty bitmap

            Graphics g = Graphics.FromImage(returnBitmap);





            //move rotation point to center of image

            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);

            //rotate

            g.RotateTransform(angle);

            //move image back

            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);

            //draw passed in image onto graphics object

            g.DrawImage(b, new Point(0, 0));



            return returnBitmap;
        }
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            { 
                case Keys.Space:
                   // SaveCoordinates();
                    break;
                case Keys.B:
                   // FillCoordinates();
                    break;
                case Keys.S:
                   // CurrentMouseMode = Modes.SHOOT;
                    break;
                

            }
            DrawDubb(this.CreateGraphics());

        }
        void FillCoordinates()
        {
            string fileName = "C://Users//20100//Desktop//ZUMA//Coordinates.txt";

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string sub = line;

                   string[] values =line.Split(',');
                    Point pnn = new Point();
                    pnn.X = Int16.Parse(values[0]);
                    pnn.Y = Int16.Parse(values[1]);
                    obj.ControlPoints.Add(pnn);
                }
            }
        }

        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            switch (CurrentMouseMode)
            {
                case Modes.CTRL_POINTS:
                    //if (count < 5)
                    {
                        obj.SetControlPoint(new Point(e.X, e.Y));
                        numOfCtrlPoints++;
                    }
                    //else
                    {
                        //obj2.SetControlPoint(new Point(e.X, e.Y));
                    }
                    //count++;
                    break;

                case Modes.DRAG:
                    indexCurrDragNode = obj.isCtrlPoint(e.X, e.Y);
                    break;
                case Modes.SHOOT:
                    l = new CMyLine();
                    int X_F = (fr.X + (fr.X + fr.im.Width)) / 2;
                    int Y_F = (fr.Y + (fr.Y + fr.im.Height)) / 2 ;
                    l.Color = ball_aim.Color;
                    l.SetVals(X_F , Y_F , e.X , e.Y);
                    fr.bl.Add(l);
                    randomized_aim = Aim_Color.Next(3);
                    ball_aim.Color = randomized_aim;
                    

                    break;
            }

            //Invalidate();
           
            	DrawDubb( this.CreateGraphics());

		}
			
			
		
       

		private void DrawScene(Graphics g)
		{
			g.Clear(Color.White);
            
			obj.DrawCurve(g);
            
            for (int i = 0; i < BL.Count; i++)
            {
                carPoint = obj.CalcCurvePointAtTime(BL[i].move_t);
                
                if (BL[i].Color == 0)
                {
                    g.FillEllipse(Brushes.Blue, carPoint.X, carPoint.Y, 50, 50);
                }
                if (BL[i].Color == 1)
                {
                    g.FillEllipse(Brushes.Green, carPoint.X, carPoint.Y, 50, 50);
                }
                if (BL[i].Color == 2)
                {
                    g.FillEllipse(Brushes.Crimson, carPoint.X, carPoint.Y, 50, 50);
                }
            
                BL[i].X = carPoint.X;
                BL[i].Y = carPoint.Y;
            }
            for (int i = 0; i < fr.bl.Count; i++)
            {
                fr.bl[i].DrawYourCurrPos(g);
               
            }
            ball_aim.DrawYourCurrPos(g);
            Bitmap f;
            f = Rotate(fr.im, angle);
            g.DrawImage(f, fr.X, fr.Y);
                
                //The image you want to rotate
              
		}

		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
         
            DrawDubb(e.Graphics);
		}


		private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (CurrentMouseMode == Modes.DRAG && indexCurrDragNode != -1)
			{
				obj.ModifyCtrlPoint(indexCurrDragNode, e.X, e.Y);
				DrawDubb( this.CreateGraphics());
			}
             
                circ.XC = (fr.X  + (fr.im.Width / 2)); 
                circ.YC = (fr.Y +  (fr.im.Height / 2)); 
                double x_dis =  circ.XC - e.X;
                double y_dis =   circ.YC - e.Y;

                double rad = Math.Atan2(y_dis , x_dis);
                double deg = rad * 180 / Math.PI + 180;
                angle = Convert.ToSingle(deg) ;

                float tempx2 = e.X;
                float tempy2 = e.Y;
                double dx = e.X - circ.XC;
                double dy = e.Y - circ.YC;
                double m = dy / dx;
            double a = circ.XC;
            double b = circ.YC;
           double c = 0; double d = 0;
           int speed = 20;
                if(Math.Abs(dy) > Math.Abs(dx))
                { 
                if(a < e.X)
                {
                    c = a + speed;
                    d = b + (m * speed);
                }
                else
                {

                    c = a - speed;
                    d = b - (m * speed);
                }
                }
                if(Math.Abs(dx) > Math.Abs(dy))
                {
                    {
                        if(b < e.Y)
                        {
                            c = a + (1 / m * speed);
                            d = b + speed;
                        }
                        else
                        {

                            c = a - (1 / m * speed);
                            d = b - speed;
                        }
                    }
                }
                ball_aim.aim_mode = 1;
                ball_aim.counter = 0;
                ball_aim.Speed = 3;
                ball_aim.SetVals((float)a, (float)b, (float)c, (float)d);
                while (true)
                {
                    ball_aim.MoveStep();
                    if (ball_aim.currX > fr.X + fr.im.Width ||
                       ball_aim.currY > fr.Y + fr.im.Height ||
                        ball_aim.currX < fr.X ||
                        ball_aim.currY < fr.Y)
                    {
                        break;
                    }
                }
                
             
		}

		private void Form1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (CurrentMouseMode == Modes.DRAG)
			{
				indexCurrDragNode = -1;
				DrawDubb(this.CreateGraphics());
			}
          
		}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
	}
}
