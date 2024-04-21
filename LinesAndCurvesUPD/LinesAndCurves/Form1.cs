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
                WriteSvgHeader(writer, width, height);
                WriteMarkerDefinition(writer);

                foreach (var curve in curves)
                {
                    ICurve curveObject = curve.CurveAccessor;

                    if (checkBox1.Checked) // Check if the mirror checkbox is checked
                    {
                        // Reflect the curve
                        IPoint p1, p2, p3, p4;
                        if (curveObject is Line line)
                        {
                            p1 = new Geometry.Point(panel1.Width - line.GetPoint(0).getX(), panel1.Height - line.GetPoint(0).getY());
                            p2 = new Geometry.Point(panel1.Width - line.GetPoint(1).getX(), panel1.Height - line.GetPoint(1).getY());
                            WriteLine(writer, p1, p2);
                        }
                        else if (curveObject is Bezier bezier)
                        {
                            p1 = new Geometry.Point(panel1.Width - bezier.P1.getX(), panel1.Height - bezier.P1.getY());
                            p2 = new Geometry.Point(panel1.Width - bezier.P2.getX(), panel1.Height - bezier.P2.getY());
                            p3 = new Geometry.Point(panel1.Width - bezier.P3.getX(), panel1.Height - bezier.P3.getY());
                            p4 = new Geometry.Point(panel1.Width - bezier.P4.getX(), panel1.Height - bezier.P4.getY());
                            WriteBezier(writer, p1, p2, p3, p4);
                        }
                    }
                    else
                    {
                        // Save the original curve
                        if (curveObject is Line line)
                        {
                            WriteLine(writer, line.GetPoint(0), line.GetPoint(1));
                        }
                        else if (curveObject is Bezier bezier)
                        {
                            WriteBezier(writer, bezier.P1, bezier.P2, bezier.P3, bezier.P4);
                        }
                    }
                }

                writer.WriteLine("</svg>");
            }

            MessageBox.Show("Curves have been saved successfully!");
        }

        private void WriteSvgHeader(StreamWriter writer, int width, int height)
        {
            writer.WriteLine("<?xml version=\"1.0\" standalone=\"no\"?>");
            writer.WriteLine("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" ");
            writer.WriteLine("\"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
            writer.WriteLine($"<svg width=\"{width}\" height=\"{height}\" version=\"1.1\" ");
            writer.WriteLine("xmlns=\"http://www.w3.org/2000/svg\">");
        }

        private void WriteMarkerDefinition(StreamWriter writer)
        {
            writer.WriteLine("<defs>");
            writer.WriteLine("  <marker id=\"arrow\" viewBox=\"0 0 5 5\" refX=\"3\" refY=\"3\" markerWidth=\"5\" markerHeight=\"5\" orient=\"auto\">");
            writer.WriteLine("    <path d=\"M 0 0 L 5 3 L 0 5 Z\" fill=\"green\"/>");
            writer.WriteLine("  </marker>");
            writer.WriteLine("</defs>");
        }

        private void WriteLine(StreamWriter writer, IPoint p1, IPoint p2)
        {
            int x1 = (int)p1.getX();
            int y1 = (int)p1.getY();
            int x2 = (int)p2.getX();
            int y2 = (int)p2.getY();

            writer.WriteLine($"<ellipse cx=\"{x1}\" cy=\"{y1}\" rx=\"3\" ry=\"3\" stroke=\"green\" stroke-width=\"3\" fill=\"none\" />");
            writer.WriteLine($"<line x1=\"{x1}\" y1=\"{y1}\" x2=\"{x2}\" y2=\"{y2}\" style=\"stroke:green;stroke-width:3\" marker-end=\"url(#arrow)\" />");
        }

        private void WriteBezier(StreamWriter writer, IPoint p1, IPoint p2, IPoint p3, IPoint p4)
        {
            int x1 = (int)p1.getX();
            int y1 = (int)p1.getY();
            int x2 = (int)p2.getX();
            int y2 = (int)p2.getY();
            int x3 = (int)p3.getX();
            int y3 = (int)p3.getY();
            int x4 = (int)p4.getX();
            int y4 = (int)p4.getY();

            writer.WriteLine($"<rect x=\"{(x1 - 2)}\" y=\"{(y1 - 2)}\" width=\"5\" height=\"5\" stroke=\"black\" stroke-width=\"3\" fill=\"none\" />");
            writer.WriteLine($"<path d=\"M{x1} {y1} C {x2} {y2}, {x3} {y3}, {x4} {y4}\" stroke=\"black\" stroke-width=\"3\" stroke-dasharray=\"10 3\" fill=\"none\" />");
            writer.WriteLine($"<rect x=\"{(x4 - 2)}\" y=\"{(y4 - 2)}\" width=\"5\" height=\"5\" stroke=\"black\" stroke-width=\"3\" fill=\"none\" />");
        }


    }
}
