using LkDicomView.AnnObjects.Enums;
using LkDicomView.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LkDicomView.AnnObjects
{
    public class LineAnnObject: AnnObject
    {
        public LineAnnObject(Point drawStartPosition, Point drawEndPosition):base(drawStartPosition, drawEndPosition)
        {

        }

        public override void Draw(Graphics graphics)
        {
            var rect = new Rectangle(
                Math.Min(DrawStartPosition.X, DrawEndPosition.X) - 25,
                Math.Min(DrawStartPosition.Y, DrawEndPosition.Y) - 25,
                Math.Abs(DrawStartPosition.X - DrawEndPosition.X) + 50,
                Math.Abs(DrawStartPosition.Y - DrawEndPosition.Y) + 50
                );

            var centerPoint = DrawStartPosition.GetCenterPoint(DrawEndPosition);
            var distance = StartPosition.GetDistance(EndPosition);
            var distanceStr = Math.Round(distance, 1).ToString();
            var font = new Font("微软雅黑", 9);
            var distanceSize = graphics.MeasureString(distanceStr, font).ToSize();

            var strPoint = centerPoint.IOffset(-distanceSize.Width / 2, -distanceSize.Height / 2);

            graphics.DrawLine(new Pen(Color.OrangeRed, 2), DrawStartPosition, DrawEndPosition);
            if (IsSelected)
            {
                graphics.DrawRectangle(new Pen(Color.White, 2), rect);
            }


            if (distance >= 100f)
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(strPoint, distanceSize));
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.DrawString(distanceStr, font, new SolidBrush(Color.OrangeRed), strPoint);
            }
            else
            {
                graphics.DrawString(distanceStr, font, new SolidBrush(Color.OrangeRed), centerPoint.IOffset(-distanceSize.Width / 2, -distanceSize.Height));
            }
        }
    }
}
