using LkDicomView.AnnObjects.Enums;
using LkDicomView.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LkDicomView.AnnObjects
{
    public abstract class AnnObject
    {
        public AnnObject(Point drawStartPosition, Point drawEndPosition)
        {
            defaultStartPosition = drawStartPosition;
            defaultEndPosition = drawEndPosition;
            defaultRectangle = new Rectangle(
                drawStartPosition, 
                new Size(
                    Math.Abs(drawStartPosition.X - drawEndPosition.X), 
                    Math.Abs(drawStartPosition.Y - drawEndPosition.Y)
                )
            );
        }

        public AnnObject()
        {

        }
        public int PenWidth { get; set; } = 2;
        public int FrameIndex { get; set; }
        public bool IsSelected { get; set; }
        public Point DrawStartPosition { get; set; }
        public Point DrawEndPosition { get; set; }
        public AnnObjectType Type { get; set; }
        private float scale = 1;

        private readonly Point defaultStartPosition;
        private readonly Point defaultEndPosition;
        private readonly Rectangle defaultRectangle;

        public Point StartPosition => defaultStartPosition;
        public Point EndPosition => defaultEndPosition;
        public Rectangle Rectangle => defaultRectangle;

        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                DrawStartPosition = defaultStartPosition.ScalePoint(value);
                DrawEndPosition = defaultEndPosition.ScalePoint(value);
            }
        }

        public abstract void Draw(Graphics graphics);
    }
}
