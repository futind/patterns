using Geometry;
using System.Drawing;

namespace Visual
{
    public interface IDrawable
    {
        public void Draw(Graphics g);
    }

    public abstract class VisualCurve : IDrawable, ICurve
    {
        public VisualCurve(IPoint a, IPoint b)
        {
            this.a = a;
            this.b = b;
        }
        public void Draw(Graphics g)
        {
            int n = 10;
            this.GetPoint(0, out a);

            for(int i = 1; i <= n; i++)
            {
                this.GetPoint((double)(i / n), out b);
                g.DrawLine(Pens.Red, (int) a.GetX(), (int) a.GetY(), (int) b.GetX(), (int) b.GetY());
                a = b;
            }
        }
        public abstract void GetPoint(double t, out IPoint p);

        protected IPoint a, b;
    }

    public class VisualLine : VisualCurve
    {
        public VisualLine(IPoint a, IPoint b) : base(a, b) { }

        public override void GetPoint(double t, out IPoint p)
        {
            IPoint new_point = new Geometry.Point();
            new_point.SetX((1 - t) * a.GetX() + t * b.GetX());
            new_point.SetY((1 - t) * a.GetY() + t * b.GetY());
            p = new_point;
        }
    }

    public class VisualBezier : VisualCurve
    {
        public VisualBezier(IPoint a, IPoint b, IPoint c, IPoint d) : base(a, b)
        {
            this.c = c;
            this.d = d;
        }

        public override void GetPoint(double t, out IPoint p)
        {
            IPoint new_point = new Geometry.Point();
            new_point.SetX(Math.Pow(1 - t, 3) * a.GetX() +
                            3 * t * Math.Pow(1 - t, 2) * c.GetX() +
                            3 * Math.Pow(t, 2) * (1 - t) * d.GetX() +
                            Math.Pow(t, 3) * b.GetX());
            new_point.SetY(Math.Pow(1 - t, 3) * a.GetY() +
                            3 * t * Math.Pow(1 - t, 2) * c.GetY() +
                            3 * Math.Pow(t, 2) * (1 - t) * d.GetY() +
                            Math.Pow(t, 3) * b.GetY());
            p = new_point;
        }

        private IPoint c, d;
    }

}
