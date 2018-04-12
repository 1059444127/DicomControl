using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LkDicomView.Library
{
    public static class PointUtils
    {

        public static Point ScalePoint(this Point point, float scale)
        {
            var newPoint = new Point();
            newPoint.X = (int)(point.X * scale);
            newPoint.Y = (int)(point.Y * scale);
            return newPoint;
        }

        public static double GetDistance(this Point point1, Point point)
        {
            var x = point.X - point1.X;
            var y = point.Y - point1.Y;
            return Math.Sqrt(x * x + y * y);
        }

        public static Point GetCenterPoint(this Point point1, Point point2)
        {
            return new Point((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
        }

        public static Point IOffset(this Point point, int x, int y)
        {
            return new Point(point.X + x, point.Y + y);
        }
    }
}
