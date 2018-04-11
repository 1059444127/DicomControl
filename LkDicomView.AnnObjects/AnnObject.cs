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
            this.drawStartPosition = drawStartPosition;
            DrawStartPosition = drawStartPosition;
            this.drawEndPosition = drawEndPosition;
            DrawEndPosition = drawEndPosition;
        }

        public int PenWidth { get; set; } = 2;
        public int FrameIndex { get; set; }
        public bool IsSelected { get; set; }
        public Point DrawStartPosition { get; set; }
        public Point DrawEndPosition { get; set; }
        public AnnObjectType Type { get; set; }
        private float scale = 1;

        private readonly Point drawStartPosition;
        private readonly Point drawEndPosition;

        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                DrawStartPosition = drawStartPosition.ScalePoint(value);
                DrawEndPosition = drawEndPosition.ScalePoint(value);
            }
        }
    }
}
