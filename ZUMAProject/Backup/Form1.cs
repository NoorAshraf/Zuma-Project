using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace BezierCurve
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		enum Modes{CTRL_POINTS, DRAG};
		BezierCurve	obj = new BezierCurve();
		int			numOfCtrlPoints=0;
		Modes 		CurrentMouseMode = Modes.CTRL_POINTS;
		int			indexCurrDragNode = -1;
		Bitmap		_backBuffer;

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3});
			this.menuItem1.Text = "Bezier Curve";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Control Points";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Drag & Drop";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(672, 429);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);

		}
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
			switch(CurrentMouseMode)
			{
				case Modes.CTRL_POINTS:
					if (numOfCtrlPoints < obj.NumControlPoints)
					{
						obj.SetControlPoint(new Point(e.X,e.Y), numOfCtrlPoints);
						numOfCtrlPoints++;
					}
					break;

				case Modes.DRAG:
					indexCurrDragNode = obj.ChangeCtrlPoint(e.X, e.Y);
					break;
			}
			
			//Invalidate();
			PaintStuff();
		}

		private void PaintStuff()
		{
			if(_backBuffer==null)
			{ 
				_backBuffer=new Bitmap(this.ClientSize.Width,this.ClientSize.Height);
			}

			Graphics g=null;
			g=Graphics.FromImage(_backBuffer);
			g.Clear(Color.White);

			obj.DrawCurve(g);
  
			Graphics gForm = this.CreateGraphics();
			//g.Dispose();
			gForm.DrawImageUnscaled(_backBuffer,0,0); 
		}

		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			obj.DrawCurve(e.Graphics);
		}


		private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (CurrentMouseMode == Modes.DRAG && indexCurrDragNode != -1)
			{
				obj.ModifyCtrlPoint(indexCurrDragNode, e.X, e.Y);
				PaintStuff();
				//Invalidate();
				//Update();
			}
		}

		private void Form1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (CurrentMouseMode == Modes.DRAG)
			{
				indexCurrDragNode = -1;
				PaintStuff();
				//Invalidate();
				//Update();
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			CurrentMouseMode = Modes.CTRL_POINTS;
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			CurrentMouseMode = Modes.DRAG;
		}
	}
}
