using LkDicomView.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LkDicomView.AnnObjects
{
    public class RectangleAnnObject : AnnObject
    {
        public RectangleAnnObject(Point drawStartPosition, Point drawEndPosition) : base(drawStartPosition, drawEndPosition)
        {

        }

        public override void Draw(Graphics graphics)
        {
            var pen = new Pen(Color.OrangeRed, 2);

            var rect = new Rectangle(
                Math.Min(DrawStartPosition.X, DrawEndPosition.X),
                Math.Min(DrawStartPosition.Y, DrawEndPosition.Y),
                Math.Abs(DrawStartPosition.X - DrawEndPosition.X),
                Math.Abs(DrawStartPosition.Y - DrawEndPosition.Y)
                );
            graphics.DrawRectangle(pen, rect);

            var centerPoint = DrawStartPosition.GetCenterPoint(DrawEndPosition);
            var str = $"{Rectangle.Width}*{Rectangle.Height}";
            var font = new Font("微软雅黑", 9);
            var strSize = graphics.MeasureString(str, font).ToSize();

            graphics.DrawString(str, font, new SolidBrush(Color.OrangeRed), centerPoint.IOffset(-strSize.Width / 2, -strSize.Height / 2));

            
        }
    }
}
