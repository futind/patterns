using Geometry;
using System.Windows.Media.Imaging;
using Visual;

namespace LinesAndCurves
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
            last_clicked = new Geometry.Point(Width / 2, Height / 2);
        }

        private void Main_Form_Mouse_Down(object sender, MouseEventArgs e)
        {
            last_clicked.setX(e.X);
            last_clicked.setY(e.Y);
            Refresh();
        }

        private void Main_Form_Paint(object sender, PaintEventArgs e)
        {
            //IPoint a = new Geometry.Point(Width / 2, Height / 2);
            //IPoint b = new Geometry.Point(last_clicked.getX(), last_clicked.getY());
            //IPoint c = new Geometry.Point(Width / 2, last_clicked.getY());
            //IPoint d = new Geometry.Point(last_clicked.getX(), Height / 2);
            //VisualCurve L = new VisualCurve(new Line(a, b));
            //VisualCurve B = new VisualCurve(new Bezier(a, b, c, d));

            //IDrawer black = new BlackDrawer(e.Graphics);
            //IDrawer green = new GreenDrawer(e.Graphics);

            //L.Draw(green);
            //B.Draw(black);
        }

        private IPoint last_clicked = null;
        private System.Numerics.Vector<VisualCurve> curves;

        private void Main_Form_Generate_Button_Clicked(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void Save_Button_Clicked(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Red, 0, 0, 5, 5);
            e.Graphics.DrawRectangle(Pens.Green, 10, 10, 5, 5);
            e.Graphics.DrawRectangle(Pens.Blue, -10, -10, 5, 5);
            e.Graphics.DrawRectangle(Pens.Yellow, -10, 10, 5, 5);
            e.Graphics.DrawRectangle(Pens.Purple, 10, -10, 5, 5);

            Random rnd = new Random();
            IPoint a = new Geometry.Point((float)rnd.Next(200, 500), (float)rnd.Next(100, 300));
            IPoint b = new Geometry.Point((float)rnd.Next(200, 800), (float)rnd.Next(100, 300));
            IPoint c = new Geometry.Point((float)rnd.Next(200, 800), (float)rnd.Next(100, 300));
            IPoint d = new Geometry.Point((float)rnd.Next(200, 800), (float)rnd.Next(100, 300));

            //IPoint a = new Geometry.Point(300, 200);
            //IPoint b = new Geometry.Point(600, 50);
            //IPoint c = new Geometry.Point(100, 100);
            //IPoint d = new Geometry.Point(250, 250);
            VisualCurve L = new VisualCurve(new Line(a, b));
            VisualCurve B = new VisualCurve(new Bezier(a, b, c, d));

            IDrawer black = new BlackDrawer(e.Graphics);
            IDrawer green = new GreenDrawer(e.Graphics);
            IDrawer chiralgreen = new ChiralDrawer(green);
            IDrawer chiralblack = new ChiralDrawer(black);

            L.Draw(green);
            L.Draw(chiralgreen);
            B.Draw(black);
            B.Draw(chiralblack);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
