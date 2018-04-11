using LkDicomView.AnnObjects;
using LkDicomView.AnnObjects.AnnObjects;
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
            //this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            AnnObjectContainer = new AnnObjectContainer();

        }

        private float currentScale = 1;

        public float CurrentScale
        {
            get
            {
                return currentScale;
            }
            set
            {
                currentScale = value;
                foreach (var i in AnnObjectContainer)
                {
                    i.Scale = value;
                }
                Invalidate();
            }
        }

        public void AddAnnObject(AnnObjectType annObjectType, int frameIndex, Point p1, Point p2)
        {
            var annObject = AnnObjectContainer.CreateAnnObject(annObjectType, p1, p2);
            annObject.FrameIndex = frameIndex;
            annObject.PenWidth = 2;
            annObject.IsSelected = true;
            annObject.Type = AnnObjectType.Ruler;

            AnnObjectContainer.Add(annObject);
            Invalidate();
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
                var rect = new Rectangle(
                    Math.Min(i.DrawStartPosition.X, i.DrawEndPosition.X) - 25,
                    Math.Min(i.DrawStartPosition.Y, i.DrawEndPosition.Y) - 25,
                    Math.Abs(i.DrawStartPosition.X - i.DrawEndPosition.X) + 50,
                    Math.Abs(i.DrawStartPosition.Y - i.DrawEndPosition.Y) + 50
                    );
                if (i.FrameIndex == FrameIndex)
                {
                    switch (i.Type)
                    {
                        case AnnObjectType.Ruler:
                            g.DrawLine(new Pen(Color.OrangeRed, 2), i.DrawStartPosition, i.DrawEndPosition);
                            break;
                    }

                    if (i.IsSelected)
                    {
                        g.DrawRectangle(new Pen(Color.White, 2), rect);
                    }
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
                    i.IsSelected = true;
                }
                else
                {
                    i.IsSelected = false;
                }
            }
            Invalidate();
        }

        public void KeyPressed(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                var currentSelected = AnnObjectContainer.Where(a => a.FrameIndex == frameIndex && a.IsSelected);
                currentSelected.ToList().ForEach(a =>
                {
                    AnnObjectContainer.Remove(a);
                });
                Invalidate();
            }
        }

        #region 标记
        private bool isLeftMouseDown;
        private Point beginMousePosition;
        private Graphics g;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isLeftMouseDown = true;
                beginMousePosition = MousePosition;
                g = CreateGraphics();
            }
            base.OnMouseDown(e);
        }

        public AnnObjectType CurrentAnnType { get; set; }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isLeftMouseDown)
            {
                g.DrawLine(new Pen(Color.Orange, 2), PointToClient(beginMousePosition), PointToClient(MousePosition));
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isLeftMouseDown && beginMousePosition.GetDistance(MousePosition) > 2)
            {
                isLeftMouseDown = false;
                var begin = PointToClient(beginMousePosition);
                var end = PointToClient(MousePosition);
                AddAnnObject(CurrentAnnType, frameIndex, begin, end);
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
