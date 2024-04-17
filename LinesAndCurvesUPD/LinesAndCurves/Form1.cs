using Geometry;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Visual;

namespace LinesAndCurves
{
    public partial class Main_Form : Form
    {

        IDrawable[] curves;
        private bool mirrorMode = false;
        public Main_Form()
        {
            InitializeComponent();
            curves = new IDrawable[2];
        }

        private void Main_Form_Generate_Button_Clicked(object sender, EventArgs e)
        {
            panel1.Refresh();
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


            curves[0] = new VisualCurve(new Line(a, b));
            curves[1] = new VisualCurve(new Bezier(a, b, c, d));


            IDrawer black = new BlackDrawer(e.Graphics);
            IDrawer green = new GreenDrawer(e.Graphics);
            IDrawer chiralgreen = new ChiralDrawer(green, this.Width, this.Height);
            IDrawer chiralblack = new ChiralDrawer(black, this.Width, this.Height);

            curves[0].Draw(green);
            curves[0].Draw(chiralgreen);
            curves[1].Draw(black);
            curves[1].Draw(chiralblack);
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
            // Создаем SVG-файл и открываем его для записи
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Записываем заголовок SVG
                writer.WriteLine("<?xml version=\"1.0\" standalone=\"no\"?>");
                writer.WriteLine("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" ");
                writer.WriteLine("\"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
                writer.WriteLine($"<svg width=\"{width}\" height=\"{height}\" version=\"1.1\" ");
                writer.WriteLine("xmlns=\"http://www.w3.org/2000/svg\">");

                // Проходим по списку линий и записываем каждую в SVG
                foreach (var curve in curves)
                {
                    //writer.WriteLine(curve.ToSvgPath());
                }

                // Закрываем тег SVG
                writer.WriteLine("</svg>");
            }

            MessageBox.Show("Отрисованные линии успешно сохранены в формате SVG по пути: " + filePath);
        }




    }
}
