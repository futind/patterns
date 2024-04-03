using Geometry;
using System.Drawing;
using System.Reflection;

namespace Visual
{
    public interface IDrawer
    {
        public void DrawCurve(ICurve C);
        public void DrawFirstPoint(ICurve C);
        public void DrawLastPoint(ICurve C);
        public void DrawSegment(ICurve C, double t1, double t2);
    }

    public class BlackDrawer : IDrawer
    {

        public BlackDrawer()
        {
            customPen = new Pen(Color.Black, 3);
            customPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            bmp = new Bitmap(800, 400);
            g = Graphics.FromImage(bmp);
        }

        public void DrawFirstPoint(ICurve C)
        {
            IPoint start = C.GetPoint(0);
            g.DrawRectangle(customPen, (int)(start.getX() - 2.5), (int) (start.getY() + 2.5), 5, 5);
        }

        public void DrawLastPoint(ICurve C)
        {
            IPoint end = C.GetPoint(1);
            g.DrawRectangle(customPen, (int)(end.getX() - 2.5), (int)(end.getY() + 2.5), 5, 5);
        }

        public void DrawSegment(ICurve C, double t1, double t2)
        {
            IPoint start = C.GetPoint(t1);
            IPoint end = C.GetPoint(t2);

            g.DrawLine(customPen, (int)start.getX(), (int)start.getY(), (int)end.getX(), (int)end.getY());
        }

        public void DrawCurve(ICurve C)
        {
            DrawFirstPoint(C);
            DrawLastPoint(C);

            int n = 100;
            for (int i = 0; i < n; i++)
            {
                DrawSegment(C, i / (double)n, (i + 1) / (double)n);
            }
        }

        private Graphics g;
        private Bitmap bmp;
        private Pen customPen;
    }


    public class GreenDrawer : IDrawer
    {
        public GreenDrawer()
        {
            customPen = new Pen(Color.Green, 3);
            customPen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

            bmp = new Bitmap(800, 400);
            g = Graphics.FromImage(bmp);
        }

        public void DrawFirstPoint(ICurve C)
        {
            IPoint start = C.GetPoint(0);
            g.DrawEllipse(customPen, (int) start.getX(), (int) start.getY(), 5, 5);
        }

        public void DrawLastPoint(ICurve C)
        {
            IPoint end = C.GetPoint(1);
            g.DrawEllipse(customPen, (int) end.getX(), (int) end.getY(), 5, 5);
        }

        public void DrawSegment(ICurve C, double t1, double t2)
        {
            IPoint start = C.GetPoint(t1);
            IPoint end = C.GetPoint(t2);

            g.DrawLine(customPen, (int) start.getX(), (int) start.getY(), (int) end.getX(), (int) end.getY());
        }

        public void DrawCurve(ICurve C)
        {
            DrawFirstPoint(C);
            DrawLastPoint(C);

            int n = 100;
            for (int i = 0; i < n; i++)
            {
                DrawSegment(C, i / (double)n, (i + 1) / (double)n);
            }

        }

        private Pen customPen;
        private Bitmap bmp;
        private Graphics g;
    }



    public interface IDrawable
    {
        public void Draw(IDrawer g);
    }

    public class VisualCurve : IDrawable, ICurve
    {
        public VisualCurve(ICurve C)
        {
            curve = C;
        }

        public void Draw(IDrawer d)
        {
            d.DrawCurve(this);
        }

        public IPoint GetPoint(double t)
        {
            return curve.GetPoint(t);
        }

        protected ICurve curve;
    }

    //public class VisualLine : VisualCurve
    //{
    //    public VisualLine(ICurve C) : base(C) { }

    //    public override IPoint GetPoint(double t)
    //    {
    //        return curve.GetPoint(t);
    //    }
    //}

    //public class VisualBezier : VisualCurve
    //{
    //    public VisualBezier(ICurve C) : base(C) {}

    //    public override IPoint GetPoint(double t)
    //    {
    //        return curve.GetPoint(t);
    //    }
    //}

}
