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
        }

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
            IPoint b = new Geometry.Point((float)rnd.Next(200, 500), (float)rnd.Next(100, 300));
            IPoint c = new Geometry.Point((float)rnd.Next(200, 500), (float)rnd.Next(100, 300));
            IPoint d = new Geometry.Point((float)rnd.Next(200, 500), (float)rnd.Next(100, 300));

            VisualCurve L = new VisualCurve(new Line(a, b));
            VisualCurve B = new VisualCurve(new Bezier(a, b, c, d));

            IDrawer black = new BlackDrawer(e.Graphics);
            IDrawer green = new GreenDrawer(e.Graphics);
            IDrawer chiralgreen = new ChiralDrawer(green, this.Width, this.Height);
            IDrawer chiralblack = new ChiralDrawer(black, this.Width, this.Height);

            //L.Draw(green);
            //L.Draw(chiralgreen);
            //B.Draw(black);
            B.Draw(chiralblack);
        }
    }
}
