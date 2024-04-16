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
        public ChiralDrawer(IDrawer D, int Width, int Height)
        {
            subdrawer = D;
            this.Height = Height;
            this.Width = Width;
        }
        public void DrawCurve(ICurve C)
        {
            IPoint S = C.GetPoint(0);
            IPoint E = C.GetPoint(-1);
            // Draw start and end points
            DrawPoint(S);
            DrawPoint(E);

            // start and of the segment
            IPoint A = new Geometry.Point();
            IPoint B = new Geometry.Point();

            double ax, ay, bx, by;
            int axmult = 1; 
            int aymult = 1;
            int bxmult = 1;
            int bymult = 1;

            int n = 10;
            for (int i = 0; i < n; i++)
            {
                A = C.GetPoint(i / (double) n);
                B = C.GetPoint((i + 1) / (double) n);

                axmult = (A.getX() > S.getX()) ? -1 : 1;
                aymult = (A.getY() > S.getY()) ? -1 : 1;
                bxmult = (B.getX() > S.getX()) ? -1 : 1;
                bymult = (B.getY() > S.getY()) ? -1 : 1;

                ax = S.getX() + axmult * Math.Abs(S.getX() - A.getX());
                ay = S.getY() + aymult * Math.Abs(S.getY() - A.getY());
                bx = S.getX() + bxmult * Math.Abs(S.getX() - B.getX());
                by = S.getY() + bymult * Math.Abs(S.getY() - B.getY());

                A.setX(ax); B.setX(bx);
                A.setY(ay); B.setY(by);

                DrawSegment(C, A, B);
            }
        }
        public void DrawPoint(IPoint p)
        {
            subdrawer.DrawPoint(p);
        }
        public void DrawSegment(ICurve C, IPoint A, IPoint B)
        {
            subdrawer.DrawSegment(C, A, B);
        }

        private IDrawer subdrawer = null;
        private int Height;
        private int Width;
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
