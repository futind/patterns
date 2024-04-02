using Geometry;
using System.Windows.Media.Imaging;
using Visual;

namespace LinesAndCurves
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            last_clicked = new Geometry.Point(Width / 2, Height / 2);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if (last_clicked == null)
            {
                last_clicked = new Geometry.Point(e.X, e.Y);
            }
            else
            {
                last_clicked.setX(e.X);
                last_clicked.setY(e.Y);
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            IPoint a = new Geometry.Point(Width / 2, Height / 2);
            IPoint b = new Geometry.Point(last_clicked.getX(), last_clicked.getY());
            IPoint c = new Geometry.Point(Width / 2, last_clicked.getY());
            IPoint d = new Geometry.Point(last_clicked.getX(), Height / 2);
            VisualCurve L = new VisualLine(a, b);
            VisualCurve B = new VisualBezier(a, b, c, d);

            L.Draw(e.Graphics);
            B.Draw(e.Graphics);
        }

        private IPoint last_clicked = null;
        private System.Numerics.Vector<VisualCurve> curves;
    }
}
