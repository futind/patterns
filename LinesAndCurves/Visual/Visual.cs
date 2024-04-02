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
            this._a = a;
            this._b = b;
        }

        public void Draw(Graphics g)
        {
            int n = 1000;
            IPoint a = GetPoint(0);

            for (int i = 1; i <= n; i++)
            {
                IPoint b = GetPoint((i / (n * 1.0)));
                g.DrawLine(Pens.Black, (int)a.getX(), (int)a.getY(), (int)b.getX(), (int)(b.getY()));
                a = b;
            }
        }

        public abstract IPoint GetPoint(double t);

        protected IPoint _a, _b;
    }

    public class VisualLine : VisualCurve
    {
        public VisualLine(IPoint a, IPoint b) : base(a, b) { }

        public override IPoint GetPoint(double t)
        {
            IPoint np = new Geometry.Point();
            np.setX((1 - t) * _a.getX() + t * _b.getX());
            np.setY((1 - t) * _a.getY() + t * _b.getY());
            return np;
        }
    }

    public class VisualBezier : VisualCurve
    {
        public VisualBezier(IPoint a, IPoint b, IPoint c, IPoint d) : base(a, b)
        {
            _c = c;
            _d = d;
        }

        public override IPoint GetPoint(double t)
        {
            IPoint np = new Geometry.Point();
            np.setX(Math.Pow((1 - t), 3) * _a.getX() +
                     3 * t * Math.Pow((1 - t), 2) * _c.getX() +
                     3 * Math.Pow(t, 2) * (1 - t) * _d.getX() +
                     Math.Pow(t, 3) * _b.getX());
            np.setY(Math.Pow((1 - t), 3) * _a.getY() +
                     3 * t * Math.Pow((1 - t), 2) * _c.getY() +
                     3 * Math.Pow(t, 2) * (1 - t) * _d.getY() +
                     Math.Pow(t, 3) * _b.getY());
            return np;
        }

        private IPoint _c, _d;
    }

}
