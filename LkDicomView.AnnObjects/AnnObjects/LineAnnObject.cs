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
        public LineAnnObject(Point startLocation, Point endLocation) : base()
        {
            this.BeginPosition = startLocation;
            this.EndPosition = endLocation;
            Unit = UnitType.Pixel;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var newPoints = SetLocation();
            e.Graphics.DrawLine(new Pen(Color.OrangeRed, PenWidth), newPoints[0], newPoints[1]);
            e.Graphics.DrawString(Distance.ToString(), new Font("宋体",12),new SolidBrush(Color.OrangeRed), newPoints[1]);
            base.OnPaint(e);
        }

        public double Distance
        {
            get
            {
                switch (Unit)
                {
                    case UnitType.Pixel:
                        return Math.Round(BeginPosition.GetDistance(EndPosition), 1);
                    case UnitType.Centimeter:
                        var dpix = this.CreateGraphics().DpiX;
                        var dpiy = this.CreateGraphics().DpiY;
                        var cmx = (EndPosition.X - BeginPosition.X) * 25.4 / dpix;
                        var cmy = (EndPosition.Y - BeginPosition.Y) * 25.4 / dpiy;
                        return Math.Sqrt(cmx * cmx + cmy * cmy);
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public UnitType Unit { get; set; }
    }
}
