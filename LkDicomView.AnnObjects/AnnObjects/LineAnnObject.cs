using LkDicomView.AnnObjects.Enums;
using LkDicomView.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LkDicomView.AnnObjects.AnnObjects
{
    public class LineAnnObject: AnnObject
    {
        public LineAnnObject(Point p1, Point p2) : base()
        {
            this.p1 = p1;
            this.p2 = p2;
            SetLocation();
            defaultRect = ClientRectangle;
        }

        private Rectangle defaultRect;

        protected override void OnPaint(PaintEventArgs e)
        {
            var scale = ClientRectangle.Width / (float)defaultRect.Width;
            e.Graphics.DrawLine(new Pen(Color.OrangeRed, 2), p1.ScalePoint(scale).IOffset(-Left, -Top), p2.ScalePoint(scale).IOffset(-Left, -Top));
            e.Graphics.DrawString(Math.Round(p1.GetDistance(p2)* scale, 1).ToString(), new Font("微软雅黑", 9), new SolidBrush(Color.OrangeRed), p2.ScalePoint(scale).IOffset(-Left, -Top));
            base.OnPaint(e);
        }

    }
}
