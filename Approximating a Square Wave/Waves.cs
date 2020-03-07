using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Approximating_a_Square_Wave
{
    public partial class Waves : Form
    {

        public Waves()
        {
            InitializeComponent();
            InitializeComponent();
            CenterToScreen();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void drawSine(int iteration)
        {
            //create a graphics object from the form
            Graphics g = this.CreateGraphics();
            // Create font and brush.
            Pen ghost = new Pen(Color.FromArgb(255, 70, 70, 70));
            Pen bold = new Pen(Color.Black, 2);
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            g.FillRectangle(whiteBrush, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            List<double[]> miniSinePoints = new List<double[]>();
            double[] mainSine = new double[ClientRectangle.Width];
            double[] miniSine;

            for (int m = 1; m < iteration; m+=2)
            {
                miniSine = new double[ClientRectangle.Width];

                for (int x = 0; x < ClientRectangle.Width; x++)
                {
                    double yScale = (ClientRectangle.Height / 2);
                    double xScale = ClientRectangle.Width / 10;
                    double y = yScale * Math.Sin(m*(x/xScale))/m;
                    miniSine[x] = y;
                    mainSine[x] += y;
                }
                miniSinePoints.Add(miniSine);
            }
            g.TranslateTransform(0, ClientRectangle.Height / 2);

            Point[] sine = new Point[mainSine.Length];

            foreach (double[] pointList in miniSinePoints)
            {
                sine = new Point[pointList.Length];
                for(int i = 0; i < pointList.Length; i++)
                {
                    sine[i] = new Point(i, (int)(pointList[i]));
                }
                g.DrawLines(ghost, sine);
            }

            sine = new Point[mainSine.Length];
            for (int i = 0; i < mainSine.Length; i++)
            {
                sine[i] = new Point(i, (int)(mainSine[i]));
            }
            g.DrawLines(bold, sine);
        }

        private void MainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            drawSine((int)(numericUpDown1.Value));
        }

        void Waves_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            drawSine((int)(numericUpDown1.Value));
        }
    }
}
