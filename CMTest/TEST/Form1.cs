using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMtest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            new Init(items).Execute();
        }

        List<Circle> items = new List<Circle>();

        class Init : ACommand
        {
            List<Circle> items;
            public Init(List<Circle> items) => this.items = items;

            public override void doExecute() => items.Clear();
            
            
        }

        private Circle InCircle(Point p) {
            foreach (var c in items)
                if (c.IsMyPoint(p)) return c;
            return null;
         }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Circle res = InCircle(e.Location);
            if (res !=null)
            {
                new Remove(items, res).Execute();
            }
            else {
                new Add(items, new Circle(e.Location, 30)).Execute();
                //items.Add(new Circle(e.Location, 30));
                
            }
            Refresh();
        }

        class Remove : ACommand
        {
            List<Circle> items;
            Circle res;
            public Remove(List<Circle> items, Circle c)
            {
                this.items = items; this.res = c;
            }

            public override void doExecute() => items.Remove(res);


        }

        class Add : ACommand
        {
            List<Circle> items;
            Circle res;
            Point loc;
            public Add(List<Circle> items, Circle c)
            {
                this.items = items;
                res = c;
            }

            public override void doExecute() => items.Add(res);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var c in items)
            {
                c.Paint(e.Graphics);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CM.GetInstance().Undo();

            Refresh();
        }
    }
}
