using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMtest
{
    internal class Circle
    {

        private Point center;
        private int radius = 10;

        public Circle(Point p, int rad) => (center, radius) = (p,rad);
        public void Paint(Graphics g)
        {
            g.FillEllipse(Brushes.Red, center.X-radius, center.Y-radius, radius*2, radius*2);
        }

        public bool IsMyPoint(Point p)
        {
            double dist =Math.Sqrt( 
                (p.X - center.X) * (p.X - center.X) + 
                (p.Y - center.Y) * (p.Y - center.Y));
            return dist<radius;
        }
    }
}
