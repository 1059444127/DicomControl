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
        public static PointF ScaleBiggerPoint(this PointF point, float scale)
        {
            var newPoint = new PointF();
            newPoint.X = point.X * scale;
            newPoint.Y = point.Y * scale;
            return newPoint;
        }

        public static Point ScaleBiggerPoint(this Point point, float scale)
        {
            var newPoint = new Point();
            newPoint.X = (int)(point.X * scale);
            newPoint.Y = (int)(point.Y * scale);
            return newPoint;
        }

        public static PointF ScaleSmallerPoint(this PointF point, float scale)
        {
            var newPoint = new PointF();
            newPoint.X = point.X / scale;
            newPoint.Y = point.Y / scale;
            return newPoint;
        }

        public static Point ScaleSmallerPoint(this Point point, float scale)
        {
            var newPoint = new Point();
            newPoint.X = (int)(point.X / scale);
            newPoint.Y = (int)(point.Y / scale);
            return newPoint;
        }

        public static double GetDistance(this Point point1, Point point)
        {
            var x = point.X - point1.X;
            var y = point.Y - point1.Y;
            return Math.Sqrt(x * x + y * y);
        }

        public static double GetDistance(this PointF point1, PointF point)
        {
            var x = (point.X - point1.X);
            var y = (point.Y - point1.Y);
            return Math.Sqrt(x * x + y * y);
        }

        public static PointF GetCenterPoint(PointF point1, PointF point2)
        {
            return new PointF((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
        }

        public static PointF Offset(this PointF point, float x, float y)
        {
            return new PointF(point.X + x, point.Y + y);
        }
    }
}
