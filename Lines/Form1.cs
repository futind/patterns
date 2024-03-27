using Geometry;
using Visual;

namespace Starostin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Update();
            Form1_Paint();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            IPoint A = new Geometry.Point();
            A.SetX(50); A.SetY(50);

            IPoint B = new Geometry.Point();
            B.SetX(1000000); B.SetY(100000);

            Visual.VisualLine l = new VisualLine(A, B);
            l.Draw(e.Graphics);
        }
    }
}
