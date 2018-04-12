using LkDicomView.AnnObjects.Enums;
using LkDicomView.Library;
using Newtonsoft.Json;
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
        public AnnObject(Point startPosition, Point endPosition)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            Rectangle = new Rectangle(
                startPosition, 
                new Size(
                    Math.Abs(startPosition.X - endPosition.X), 
                    Math.Abs(startPosition.Y - endPosition.Y)
                )
            );
        }

        public int PenWidth { get; set; } = 2;
        public int FrameIndex { get; set; }
        [JsonIgnore]
        public bool IsSelected { get; set; }
        [JsonIgnore]
        public Point DrawStartPosition
        {
            get
            {
                return StartPosition.ScalePoint(Scale);
            }
        }
        [JsonIgnore]
        public float Scale { get; set; }
        [JsonIgnore]
        public Point DrawEndPosition
        {
            get
            {
                return EndPosition.ScalePoint(Scale);
            }
        }
        public AnnObjectType Type { get; set; }

        public Point StartPosition { get; set; }
        public Point EndPosition { get; set; }
        public Rectangle Rectangle { get; set; }

        public abstract void Draw(Graphics graphics);
    }
}
