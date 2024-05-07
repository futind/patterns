using Geometry;

namespace Visual
{
    public interface IDrawer
    {
        void DrawPoint(IPoint p);
        void DrawLine(IPoint a, IPoint b, bool EnableEndCap);

        void DrawPointSVG(IPoint p, StreamWriter writer);
        void DrawLineSVG(IPoint a, IPoint b, bool EnableEndCap, StreamWriter writer);
    }

    public class HorizontalReflection : IDrawer
    {
        public HorizontalReflection(IDrawer D, int Width) 
        { 
            _innerDrawer = D;
            _width = Width;
        }

        public void setDrawer(IDrawer D) 
        {
            _innerDrawer = D;
        }

        public void DrawPoint(IPoint p)
        {
            p.setX(_width - p.getX());
            _innerDrawer.DrawPoint(p);
        }

        public void DrawLine(IPoint A, IPoint B, bool EnableEndCap)
        {
            A.setX(_width - A.getX());
            B.setX(_width - B.getX());
            _innerDrawer.DrawLine(A, B, EnableEndCap);
        }

        public void DrawPointSVG(IPoint p, StreamWriter writer)
        {
            p.setX(_width - p.getX());
            _innerDrawer.DrawPointSVG(p, writer);
        }

        public void DrawLineSVG(IPoint A, IPoint B, bool EnableEndCap, StreamWriter writer)
        {
            A.setX(_width - A.getX());
            B.setX(_width - B.getX());
            _innerDrawer.DrawLineSVG(A, B, EnableEndCap, writer);
        }

        private IDrawer _innerDrawer;
        private int _width;
    }

    public class VerticalReflection : IDrawer
    {
        public VerticalReflection(IDrawer D, int Height)
        {
            _innerDrawer = D;
            this._height = Height;
        }

        public void DrawPoint(IPoint p)
        {
            p.setY(_height - p.getY());
            _innerDrawer.DrawPoint(p);
        }

        public void DrawLine(IPoint A, IPoint B, bool EnableEndCap)
        {
            A.setY(_height - A.getY());
            B.setY(_height - B.getY());
            _innerDrawer.DrawLine(A, B, EnableEndCap);
        }

        public void DrawPointSVG(IPoint p, StreamWriter writer)
        {
            p.setY(_height - p.getY());
            _innerDrawer.DrawPointSVG(p, writer);
        }

        public void DrawLineSVG(IPoint A, IPoint B, bool EnableEndCap, StreamWriter writer)
        {
            A.setY(_height - A.getY());
            B.setY(_height - B.getY());
            _innerDrawer.DrawLineSVG(A, B, EnableEndCap, writer);
        }

        private int _height;
        private IDrawer _innerDrawer;
    }

    public class GreenDrawer : IDrawer
    {
        public GreenDrawer(Graphics g) 
        {
            this._g = g;
            _pen = new Pen(Color.Green, 3);
        }

        public void DrawPoint(IPoint p)
        {
            _g.DrawEllipse(_pen, (int) p.getX(), (int) p.getY(), (float) 4, (float) 4);
        }

        public void DrawPointSVG(IPoint P, StreamWriter writer)
        {
            writer.WriteLine($"<ellipse cx=\"{(int) P.getX()}\" cy=\"{(int) P.getY()}\" rx=\"3\" ry=\"3\" stroke=\"green\" stroke-width=\"3\" fill=\"none\" />");
        }

        public void DrawLine(IPoint A, IPoint B, bool EnableEndCap)
        {
            if (EnableEndCap) _pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(3, 3);
            _g.DrawLine(_pen, (int) A.getX(), (int) A.getY(), (int) B.getX(), (int) B.getY());
            if (EnableEndCap) _pen.EndCap = new System.Drawing.Drawing2D.LineCap();
        }

        public void DrawLineSVG(IPoint A, IPoint B, bool EnableEndCap, StreamWriter writer)
        {
            if (EnableEndCap)
            {
                writer.WriteLine($"<line x1=\"{(int)A.getX()}\" y1=\"{(int)A.getY()}\" x2=\"{(int)B.getX()}\" y2=\"{(int)B.getY()}\" style=\"stroke:green;stroke-width:3\" marker-end=\"url(#arrow)\" />");
            }
            else
            {
                writer.WriteLine($"<line x1=\"{(int)A.getX()}\" y1=\"{(int)A.getY()}\" x2=\"{(int)B.getX()}\" y2=\"{(int)B.getY()}\" style=\"stroke:green;stroke-width:3\"/>");
            }
        }

        private Pen _pen;
        private Graphics _g;
    }

    public class BlackDrawer : IDrawer
    {
        public BlackDrawer(Graphics g)
        {
            this._g = g;
            _pen = new Pen(Color.Black, 3);
            _pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        public void DrawPoint(IPoint p)
        {
            _g.DrawRectangle(_pen, (int)(p.getX() - 2.5), (int)(p.getY() - 2.5), 5, 5);
        }

        public void DrawPointSVG(IPoint p, StreamWriter writer)
        {
            writer.WriteLine($"<rect x=\"{(int)(p.getX() - 2)}\" y=\"{(int)(p.getY() - 2)}\" width=\"5\" height=\"5\" stroke=\"black\" stroke-width=\"3\" fill=\"none\" />");
        }

        public void DrawLine(IPoint A, IPoint B, bool EnableEndCap)
        {
            _g.DrawLine(_pen, (int) A.getX(), (int) A.getY(), (int) B.getX(), (int) B.getY());
            if (EnableEndCap) DrawPoint(B);
        }

        public void DrawLineSVG(IPoint A, IPoint B, bool EnableEndCap, StreamWriter writer)
        {
            writer.WriteLine($"<line x1 =\"{(int)A.getX()}\" y1=\"{(int)A.getY()}\" x2=\"{(int)B.getX()}\" y2=\"{(int)B.getY()}\" stroke=\"black\" stroke-width=\"3\" stroke-dasharray=\"10 3\" fill=\"none\" />");
            if (EnableEndCap) DrawPointSVG(B, writer);
        }

        private Pen _pen;
        private Graphics _g;
    }

    // ABSTRACTION
    public interface IDrawable
    {
        public void Draw(IDrawer d);
        public void DrawSVG(IDrawer d, StreamWriter writer);
    }

    public class AVisualCurve : IDrawable, ICurve
    {
        public AVisualCurve(ICurve c)
        {
            _c = c;
        }
        public void Draw(IDrawer d)
        {
            d.DrawPoint(_c.GetPoint(0));
            int n = 10;
            for (int i = 0; i < n; ++i)
            {
                d.DrawLine(_c.GetPoint(i / (double)n), _c.GetPoint((i + 1) / (double)n), i == n - 1);
            }
        }

        public void DrawSVG(IDrawer d, StreamWriter writer)
        {
            d.DrawPointSVG(_c.GetPoint(0), writer);
            int n = 10;
            for (int i = 0; i < n; ++i)
            {
                d.DrawLineSVG(_c.GetPoint(i / (double)n), _c.GetPoint((i + 1) / (double)n), i == n - 1, writer);
            }
        }

        public IPoint GetPoint(double t)
        {
            return _c.GetPoint(t);
        }

        protected ICurve _c;
    }

    //public class VisualLine : AVisualCurve
    //{
    //    public VisualLine(Line l) : base(l) { }
    //    public override void Draw(IDrawer d)
    //    {
    //        d.DrawPoint(_c.GetPoint(0));
    //        int n = 10;
    //        for (int i = 0; i < n; ++i)
    //        {
    //            d.DrawLine(_c.GetPoint(i / (double)n), _c.GetPoint((i + 1) / (double)n), i == n - 1);
    //        }
    //    }
    //}

    //public class VisualBezier : AVisualCurve
    //{
    //    public VisualBezier(Bezier b) : base(b) { }
    //    public override void Draw(IDrawer d)
    //    {
    //        d.DrawPoint(_c.GetPoint(0));
    //        int n = 10;
    //        for (int i = 0; i < n; ++i)
    //        {
    //            d.DrawLine(_c.GetPoint(i / (double)n), _c.GetPoint((i + 1) / (double)n), i == n - 1);
    //        }
    //    }
    //}


}
