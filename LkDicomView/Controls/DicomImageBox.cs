using LkDicomView.AnnObjects;
using LkDicomView.AnnObjects.Enums;
using LkDicomView.Library;
using LkDicomView.Modules;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LkDicomView.Controls
{
    public class DicomImageBox : PictureBox
    {

        public AnnObjectContainer AnnObjectContainer { get; }

        public DicomImageBox()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            AnnObjectContainer = new AnnObjectContainer();
            AnnObjectContainer.OnAdded += OnAdded;

        }

        private void OnAdded(AnnObject annObject)
        {
            annObject.Scale = CurrentScale;
            Invalidate();
        }

        public float CurrentScale
        {
            get
            {
                return (float)Width / (float)Image.Width;
            }
        }

        public void AddAnnObject(AnnObjectType annObjectType, int frameIndex, Point p1, Point p2)
        {
            var annObject = AnnObjectContainer.CreateAnnObject(annObjectType, p1, p2);
            annObject.FrameIndex = frameIndex;
            annObject.PenWidth = 2;
            annObject.Scale = CurrentScale;
            annObject.Type = annObjectType;
            AnnObjectContainer.Add(annObject);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            AnnObjectContainer.ForEach(a => a.Scale = CurrentScale);
            base.OnResize(e);
        }

        private int frameIndex;

        public List<AnnObject> GetAnnObjects(int frameIndex)
        {
            var annObjects = AnnObjectContainer.Where(a => a.FrameIndex == frameIndex);
            return AnnObjectContainer.ToList();
        }

        public int FrameIndex
        {
            get
            {
                return frameIndex;
            }
            set
            {
                frameIndex = value;
                Invalidate();
            }
        }

        private void ShowAnnObjects(Graphics g)
        {
            foreach (var i in AnnObjectContainer)
            {
                if (i.FrameIndex == FrameIndex)
                {
                    i.Draw(g);
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            bool IsHited(AnnObject annObject, Point point)
            {
                var hitedRange = annObject.DrawStartPosition.GetDistance(point) + annObject.DrawEndPosition.GetDistance(point);
                var lineRange = annObject.DrawStartPosition.GetDistance(annObject.DrawEndPosition);
                if (hitedRange - lineRange < 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            var clickPosition = PointToClient(MousePosition);
            var currentPageAnnObject = AnnObjectContainer.Where(a => a.FrameIndex == FrameIndex);
            foreach (var i in currentPageAnnObject)
            {
                
                if (IsHited(i, clickPosition))
                {
                    if (CurrentAnnType == AnnObjectType.Eraser)
                    {
                        AnnObjectContainer.Remove(i);
                        break;
                    }
                }
            }
            Invalidate();
        }

        #region 标记
        private bool isLeftMouseDown;
        private Point beginMousePosition;
        private Graphics g;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(CurrentAnnType == AnnObjectType.None || CurrentAnnType == AnnObjectType.Eraser)
                {
                    return;
                }
                isLeftMouseDown = true;
                beginMousePosition = MousePosition;
                g = CreateGraphics();
            }
            base.OnMouseDown(e);
        }

        public AnnObjectType CurrentAnnType { get; set; }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (CurrentAnnType == AnnObjectType.None || CurrentAnnType == AnnObjectType.Eraser)
            {
                return;
            }
            if (e.Button == MouseButtons.Left && isLeftMouseDown)
            {
                Refresh();
                g.DrawLine(new Pen(Color.Orange, 2), PointToClient(beginMousePosition), PointToClient(MousePosition));
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (CurrentAnnType == AnnObjectType.None || CurrentAnnType == AnnObjectType.Eraser)
            {
                return;
            }
            if (e.Button == MouseButtons.Left && isLeftMouseDown && beginMousePosition.GetDistance(MousePosition) > 2)
            {
                isLeftMouseDown = false;
                var beginPoint = new Point(
                        x: (int)(PointToClient(beginMousePosition).X / (float)Width * Image.Width),
                        y: (int)(PointToClient(beginMousePosition).Y / (float)Height * Image.Height)
                    );
                var endPoint = new Point(
                        x: (int)(PointToClient(MousePosition).X / (float)Width * Image.Width),
                        y: (int)(PointToClient(MousePosition).Y / (float)Height * Image.Height)
                    );
                AddAnnObject(CurrentAnnType, frameIndex, beginPoint, endPoint);
                return;
            }
            base.OnMouseUp(e);
        }

        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            ShowAnnObjects(pe.Graphics);
        }
    }
}
