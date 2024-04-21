using Geometry;
using System.Drawing;
using System.Reflection;
using System.Windows.Media.Animation;

namespace Visual
{
    public interface IDrawer
    { 
        public void DrawCurve(ICurve C, bool isMirror);
        public void DrawPoint(IPoint C, bool isMirror);
        public void DrawSegment(ICurve C, IPoint A, IPoint B, bool isMirror);
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
            DrawPoint(p, false); // call the modified method with isMirror = false
        }
        public void DrawPoint(IPoint p, bool isMirror)
        {
            if (isMirror)
            {
                // Reflect the point
                float centerX = g.VisibleClipBounds.Width / 2;
                float centerY = g.VisibleClipBounds.Height / 2;
                p = new Geometry.Point(centerX - (p.getX() - centerX), centerY - (p.getY() - centerY));
            }
            g.DrawRectangle(customPen, (int)(p.getX() - 2.5), (int)(p.getY() + 2.5), 5, 5);
        }

        public void DrawSegment(ICurve C, IPoint A, IPoint B)
        {
            DrawSegment(C, A, B, false); // call the modified method with isMirror = false
        }
        public void DrawSegment(ICurve C, IPoint A, IPoint B, bool isMirror)
        {
            if (isMirror)
            {
                // Reflect the line segment
                A = new Geometry.Point(g.VisibleClipBounds.Width - A.getX(), g.VisibleClipBounds.Height - A.getY());
                B = new Geometry.Point(g.VisibleClipBounds.Width - B.getX(), g.VisibleClipBounds.Height - B.getY());
            }
            g.DrawLine(customPen, (int)A.getX(), (int)A.getY(), (int)B.getX(), (int)B.getY());
        }

        public void DrawCurve(ICurve C)
        {
            DrawCurve(C, false); // call the modified method with isMirror = false
        }
        public void DrawCurve(ICurve C, bool isMirror)
        {
            int n = 10;
            
                DrawPoint(C.GetPoint(0), isMirror);
                DrawPoint(C.GetPoint(1), isMirror);
                // Reflect the curve
                for (int i = 0; i < n; i++)
                {
                    DrawSegment(C, C.GetPoint(i / (double)n), C.GetPoint((i + 1) / (double)n), isMirror);
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
            DrawPoint(p, false); // call the modified method with isMirror = false
        }
        public void DrawPoint(IPoint p, bool isMirror)
        {
            if (isMirror)
            {
                // Reflect the point
                p = new Geometry.Point(g.VisibleClipBounds.Width - p.getX(), g.VisibleClipBounds.Height - p.getY());
            }
            g.DrawEllipse(customPen, (int)p.getX(), (int)p.getY(), 5, 5);
        }

        public void DrawSegment(ICurve C, IPoint A, IPoint B)
        {
            DrawSegment(C, A, B, false); // call the modified method with isMirror = false
        }

        public void DrawSegment(ICurve C, IPoint A, IPoint B, bool isMirror)
        {
            if (isMirror)
            {
                // Reflect the line segment
                A = new Geometry.Point(g.VisibleClipBounds.Width - A.getX(), g.VisibleClipBounds.Height - A.getY());
                B = new Geometry.Point(g.VisibleClipBounds.Width - B.getX(), g.VisibleClipBounds.Height - B.getY());
            }
            g.DrawLine(customPen, (int)A.getX(), (int)A.getY(), (int)B.getX(), (int)B.getY());
        }

        public void DrawCurve(ICurve C)
        {
            DrawCurve(C, false); // call the modified method with isMirror = false
        }
        public void DrawCurve(ICurve C, bool isMirror)
        {

            int n = 10;

            DrawPoint(C.GetPoint(0), isMirror);
            DrawPoint(C.GetPoint(1), isMirror);
            // Reflect the curve
            for (int i = 0; i < n-1; i++)
            {
                DrawSegment(C, C.GetPoint(i / (double)n), C.GetPoint((i + 1) / (double)n), isMirror);
            }
            customPen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
            DrawSegment(C, C.GetPoint((n - 1) / (double)n), C.GetPoint(1), isMirror);
            customPen.EndCap = new System.Drawing.Drawing2D.LineCap();

        }

        private Pen customPen;
        private Graphics g;
    }

    public interface IDrawable
    {
        public void Draw(IDrawer g, bool isMirror);
    }

    public class VisualCurve : IDrawable, ICurve
    {
        public VisualCurve(ICurve C)
        {
            curve = C;
        }

        public void Draw(IDrawer d, bool isMirror)
        {
            d.DrawCurve(this, isMirror);
        }

        public IPoint GetPoint(double t)
        {
            return curve.GetPoint(t);
        }

        protected ICurve curve;
    }
}
