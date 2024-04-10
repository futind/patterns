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
            IPoint a = new Geometry.Point(Width / 2, Height / 2);
            IPoint b = new Geometry.Point(last_clicked.getX(), last_clicked.getY());
            IPoint c = new Geometry.Point(Width / 2, last_clicked.getY());
            IPoint d = new Geometry.Point(last_clicked.getX(), Height / 2);
            VisualCurve L = new VisualCurve(new Line(a, b));
            VisualCurve B = new VisualCurve(new Bezier(a, b, c, d));

            IDrawer black = new BlackDrawer(e.Graphics);
            IDrawer green = new GreenDrawer(e.Graphics);

            L.Draw(green);
            B.Draw(black);
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
            Random rnd = new Random();
            IPoint a = new Geometry.Point((float) rnd.Next(0, 800), (float) rnd.Next(0, 400));
            IPoint b = new Geometry.Point((float) rnd.Next(0, 800), (float) rnd.Next(0, 400));
            IPoint c = new Geometry.Point((float) rnd.Next(0, 800), (float) rnd.Next(0, 400));
            IPoint d = new Geometry.Point((float) rnd.Next(0, 800), (float) rnd.Next(0, 400));
            VisualCurve L = new VisualCurve(new Line(a, b));
            VisualCurve B = new VisualCurve(new Bezier(a, b, c, d));

            IDrawer black = new BlackDrawer(e.Graphics);
            IDrawer green = new GreenDrawer(e.Graphics);

            L.Draw(green);
            B.Draw(black);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
