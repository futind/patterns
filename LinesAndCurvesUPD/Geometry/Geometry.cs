
using System.Net.Mail;

namespace Geometry
{
    public interface IPoint
    {
        public double getX();
        public double getY();

        public void setX(double x);
        public void setY(double y);
    }

    public class Point : IPoint
    {
        public Point()
        {
            _x = 0;
            _y = 0;
        }

        public Point(double x, double y)
        {
            _x = x; 
            _y = y;
        }
        
        public double getX()
        {
            return _x;
        }

        public double getY()
        {
            return _y;
        }

        public void setX(double x)
        {
            this._x = x;
        }

        public void setY(double y)
        {
            this._y = y;
        }

        private double _x, _y;
    }

    public interface ICurve
    {
        public IPoint GetPoint(double t);
    }

    public abstract class ACurve : ICurve
    {
        public ACurve(IPoint a, IPoint b)
        {
            _a = a;
            _b = b;
        }

        public abstract IPoint GetPoint(double t);

        protected IPoint _a, _b;
    }

    public class Line : ACurve
    {
        public Line(IPoint a, IPoint b) : base(a,b) { }

        public override IPoint GetPoint(double t)
        {
            IPoint np = new Point();
            np.setX((1 - t) * _a.getX() + t * _b.getX());
            np.setY((1 - t) * _a.getY() + t * _b.getY());
            return np;
        }
    }

    public class Bezier : ACurve
    {
        public Bezier(IPoint a, IPoint b, IPoint c, IPoint d) : base(a, b)
        {
            _c = c;
            _d = d;
        }

        public override IPoint GetPoint(double t)
        {
            IPoint np = new Point();
            np.setX( Math.Pow((1 - t),3) * _a.getX() + 
                     3 * t * Math.Pow((1 - t), 2) * _c.getX() + 
                     3 * Math.Pow(t, 2) * (1 - t) * _d.getX() +
                     Math.Pow(t, 3) * _b.getX() );
            np.setY(Math.Pow((1 - t), 3) * _a.getY() +
                     3 * t * Math.Pow((1 - t), 2) * _c.getY() +
                     3 * Math.Pow(t, 2) * (1 - t) * _d.getY() +
                     Math.Pow(t, 3) * _b.getY());
            return np;
        }

        private IPoint _c, _d;
    }

}
