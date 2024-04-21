using Geometry;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Visual;

namespace LinesAndCurves
{
    public partial class Main_Form : Form
    {
        List<VisualCurve> curves;
        private bool generate_new = false;
        bool isMirror = false;
        public Main_Form()
        {
            InitializeComponent();
            generate_new = false;
            curves = new List<VisualCurve> ();
        }

        private void Main_Form_Generate_Button_Clicked(object sender, EventArgs e)
        {
            generate_new = true;
            
            panel1.Refresh();
            checkBox1.Checked = false;


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (generate_new == true)
            {
                curves.Clear();
                Random rnd = new Random();
                IPoint a = new Geometry.Point((float)rnd.Next(200, 500), (float)rnd.Next(100, 300));
                IPoint b = new Geometry.Point((float)rnd.Next(200, 500), (float)rnd.Next(100, 300));
                IPoint c = new Geometry.Point((float)rnd.Next(200, 500), (float)rnd.Next(100, 300));
                IPoint d = new Geometry.Point((float)rnd.Next(200, 500), (float)rnd.Next(100, 300));

                curves.Add(new VisualCurve(new Line(a, b)));
                curves.Add(new VisualCurve(new Bezier(a, b, c, d)));
                generate_new = false;
            }

            IDrawer black = new BlackDrawer(e.Graphics);
            IDrawer green = new GreenDrawer(e.Graphics);

            if (curves.Count > 0)
            {
                curves[0].Draw(green, isMirror);
                curves[1].Draw(black, isMirror);
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            generate_new = false;
            isMirror = checkBox1.Checked;
            panel1.Refresh();
        }

        private void Save_Button_Clicked(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SVG files (*.svg)|*.svg";
            saveFileDialog.Title = "Save SVG File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                SaveToSVG(filePath, panel1.Width, panel1.Height);
            }
        }


        private void SaveToSVG(string filePath, int width, int height)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the SVG header
                writer.WriteLine("<?xml version=\"1.0\" standalone=\"no\"?>");
                writer.WriteLine("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" ");
                writer.WriteLine("\"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
                writer.WriteLine($"<svg width=\"{width}\" height=\"{height}\" version=\"1.1\" ");
                writer.WriteLine("xmlns=\"http://www.w3.org/2000/svg\">");

                // Write the lines and Bezier curve to the SVG file
                foreach (var curve in curves)
                {
                    if (curve is VisualCurve)
                    {
                        VisualCurve visualCurve = (VisualCurve)curve;
                        ICurve curveObject = visualCurve.CurveAccessor; // Use the CurveAccessor property

                        if (curveObject is Line)
                        {
                            Line line = (Line)curveObject;
                            IPoint p1 = line.GetPoint(0);
                            IPoint p2 = line.GetPoint(1);

                            string x1 = p1.getX().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string y1 = p1.getY().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string x2 = p2.getX().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string y2 = p2.getY().ToString(CultureInfo.CreateSpecificCulture("en-US"));

                            writer.WriteLine($"<line x1=\"{x1}\" y1=\"{y1}\" x2=\"{x2}\" y2=\"{y2}\" stroke=\"green\" stroke-width=\"3\" />");
                        }
                        else if (curveObject is Bezier)
                        {
                            Bezier bezier = (Bezier)curveObject;
                            IPoint p1 = bezier.P1;
                            IPoint p2 = bezier.P2;
                            IPoint p3 = bezier.P3;
                            IPoint p4 = bezier.P4;

                            string x1 = p1.getX().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string y1 = p1.getY().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string x2 = p2.getX().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string y2 = p2.getY().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string x3 = p3.getX().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string y3 = p3.getY().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string x4 = p4.getX().ToString(CultureInfo.CreateSpecificCulture("en-US"));
                            string y4 = p4.getY().ToString(CultureInfo.CreateSpecificCulture("en-US"));

                            writer.WriteLine($"<path d=\"M{x1} {y1} C {x2} {y2}, {x3} {y3}, {x4} {y4}\" stroke=\"black\" stroke-width=\"3\" fill=\"none\" />");
                        }
                    }
                }

                // Close the SVG file
                writer.WriteLine("</svg>");
            }

            MessageBox.Show("Lines and Bezier curve saved to SVG file successfully!");
        }


    }
}
