namespace Geometry
{
    public interface IPoint
    {
        public double GetX();
        public double GetY();
        public void SetX(double x);
        public void SetY(double y);
    }

    public interface ICurve
    {
        public void GetPoint(double t, out IPoint p);
    }

    public class Point : IPoint
    {
        public double GetX()
        {
            return x;
        }
        public double GetY()
        {
            return y;
        }
        public void SetX(double x)
        {
            this.x = x;
        }
        public void SetY(double y)
        {
            this.y = y;
        }

        private double x, y;
    }

    public abstract class ACurve : ICurve
    {
        public ACurve(IPoint a, IPoint b)
        {
            this.a = a;
            this.b = b;
        }
        public abstract void GetPoint(double t, out IPoint p);

        protected IPoint a, b;
    }

    public class Line : ACurve
    {
        public Line(IPoint a, IPoint b) : base(a, b) { }

        public override void GetPoint(double t, out IPoint p)
        {
            IPoint new_point = new Point();
            new_point.SetX((1 - t) * a.GetX() + t * b.GetX());
            new_point.SetY((1 - t) * a.GetY() + t * b.GetY());
            p = new_point;
        }
    }

    public class Bezier : ACurve
    {
        public Bezier(IPoint a, IPoint b, IPoint c, IPoint d) : base(a, b)
        {
            this.c = c;
            this.d = d;
        }

        public override void GetPoint(double t, out IPoint p)
        {
            IPoint new_point = new Point();
            new_point.SetX( Math.Pow(1 - t, 3) * a.GetX() +
                            3 * t * Math.Pow(1 - t, 2) * c.GetX() +
                            3 * Math.Pow(t, 2) * (1 - t) * d.GetX() +
                            Math.Pow(t, 3) * b.GetX() );
            new_point.SetY(Math.Pow(1 - t, 3) * a.GetY() +
                            3 * t * Math.Pow(1 - t, 2) * c.GetY() +
                            3 * Math.Pow(t, 2) * (1 - t) * d.GetY() +
                            Math.Pow(t, 3) * b.GetY());
            p = new_point;
        }

        private IPoint c, d;
    }
}
