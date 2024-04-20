using Geometry;
using System.Drawing;
using System.Reflection;
using System.Windows.Media.Animation;

namespace Visual
{
    public interface IDrawer
    { 
        public void DrawCurve(ICurve C);
        public void DrawPoint(IPoint C);
        public void DrawSegment(ICurve C, IPoint A, IPoint B);
    }

    public class ChiralDrawer : IDrawer
    {
        private IDrawer subdrawer_;
        private ICurve curve_;
        private Rectangle drawingArea_;
        public ChiralDrawer(IDrawer subdrawer, ICurve curve, Rectangle drawingArea)
        {
            subdrawer_ = subdrawer;
            curve_ = curve;
            drawingArea_ = drawingArea;
        }

        public ChiralDrawer(IDrawer D)
        {
            subdrawer_ = D;
        }
        public void DrawCurve(ICurve C)
        {
            int width = drawingArea_.Width;
            int height = drawingArea_.Height;

            IPoint S = C.GetPoint(0);
            IPoint E = C.GetPoint(0);

            // Reflect start and end points
            IPoint reflectedS = new Geometry.Point(width - S.getX(), height - S.getY());
            IPoint reflectedE = new Geometry.Point(width - E.getX(), height - E.getY());

            // Draw reflected start and end points
            subdrawer_.DrawPoint(reflectedS);
            subdrawer_.DrawPoint(reflectedE);

            int n = 10;
            for (int i = 0; i < n; i++)
            {
                IPoint A = C.GetPoint(i / (double)n);
                IPoint B = C.GetPoint((i + 1) / (double)n);

                // Reflect line segments
                IPoint reflectedA = new Geometry.Point(width - A.getX(), height - A.getY());
                IPoint reflectedB = new Geometry.Point(width - B.getX(), height - B.getY());

                subdrawer_.DrawSegment(C, reflectedA, reflectedB);
            }
        }
        public void DrawPoint(IPoint p)
        {
            subdrawer_.DrawPoint(p);
        }
        public void DrawSegment(ICurve C, IPoint A, IPoint B)
        {
            subdrawer_.DrawSegment(C, A, B);
        }

    }

    public class BlackDrawer : IDrawer
    {
        public BlackDrawer(Graphics G)
        {
            customPen = new Pen(Color.Black, 3);
            customPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            g = G;
        }
        public void DrawPoint(IPoint p)
        {
            g.DrawRectangle(customPen, (int)(p.getX() - 2.5), (int)(p.getY() + 2.5), 5, 5);
        }
        public void DrawSegment(ICurve C, IPoint A, IPoint B)
        {
            g.DrawLine(customPen, (int) A.getX(), (int) A.getY(), (int) B.getX(), (int) B.getY());
        }
        public void DrawCurve(ICurve C)
        {
            DrawPoint(C.GetPoint(0));
            DrawPoint(C.GetPoint(1));

            int n = 10;
            for (int i = 0; i < n; i++)
            {
                DrawSegment(C, C.GetPoint(i / (double)n), C.GetPoint((i + 1) / (double)n));
            }
        }
        private Graphics g;
        private Pen customPen;
    }


    public class GreenDrawer : IDrawer
    {
        public GreenDrawer(Graphics G)
        {
            customPen = new Pen(Color.Green, 3);
            g = G;
        }
        public void DrawPoint(IPoint p)
        {
            g.DrawEllipse(customPen, (int) p.getX(), (int) p.getY(), 5, 5);
        }
        public void DrawSegment(ICurve C, IPoint A, IPoint B)
        {
            g.DrawLine(customPen, (int) A.getX(), (int) A.getY(), (int) B.getX(), (int) B.getY());
        }
        public void DrawCurve(ICurve C)
        {
            DrawPoint(C.GetPoint(0));
            DrawPoint(C.GetPoint(1));

            int n = 10;
            for (int i = 0; i < n - 1; i++)
            {
                DrawSegment(C, C.GetPoint(i / (double)n), C.GetPoint((i + 1) / (double)n));
            }
            customPen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
            DrawSegment(C, C.GetPoint((n - 1) / (double)n), C.GetPoint(1));
            customPen.EndCap = new System.Drawing.Drawing2D.LineCap();
        }

        private Pen customPen;
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
}
