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

        private AnnObjectContainer annObjectContainer;

        public AnnObjectContainer AnnObjectContainer
        {
            get
            {
                return annObjectContainer;
            }
        }

        public DicomImageBox()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            annObjectContainer = new AnnObjectContainer();

        }

        public void AddAnnObject(AnnObjectType annObjectType, int frameIndex, Point begin, Point end)
        {
            var annObject = AnnObjectContainer.CreateAnnObject(annObjectType, begin, end);
            annObject.Parent = this;
            annObject.AutoSize = true;
            annObject.FrameIndex = frameIndex;
            annObject.Scale = Scale;
            annObject.MouseDown += new MouseEventHandler((o, e) => {
                if(e.Button == MouseButtons.Left)
                {
                    var selected = (AnnObject)o;
                    selected.IsSelected = true;
                }
            });
            annObjectContainer.Add(annObject);
            ShowAnnObjects();
        }

        private int frameIndex;
        private float scale;

        public List<AnnObject> GetAnnObjects(int frameIndex)
        {
            var annObjects = annObjectContainer.Where(a => a.FrameIndex == frameIndex);
            return annObjectContainer.ToList();
        }

        public new float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                foreach (AnnObject c in Controls)
                {
                    c.Scale = value;
                    c.Invalidate();
                }
            }
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
                ShowAnnObjects();
            }
        }

        private void ShowAnnObjects()
        {
            foreach (var i in annObjectContainer)
            {
                if (i.FrameIndex == FrameIndex)
                {
                    Controls.Add(i);
                }
                else
                {
                    Controls.Remove(i);
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {

            annObjectContainer.ForEach(a => a.IsSelected = false);
            Invalidate();
        }

        public new void OnKeyDown(KeyEventArgs e)
        {
            if(e.KeyData == Keys.Delete)
            {
                var currentSelected = annObjectContainer.Where(a => a.FrameIndex == frameIndex && a.IsSelected);
                currentSelected.ToList().ForEach(a => {
                    annObjectContainer.Remove(a);
                    Controls.Remove(a);
                });
                base.OnKeyDown(e);
            }
        }

        #region 标记
        private bool isLeftMouseDown;
        private Point beginMousePosition;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isLeftMouseDown = true;
                beginMousePosition = MousePosition;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isLeftMouseDown)
            {
                Refresh();
                CreateGraphics().DrawLine(new Pen(Color.Orange, 2), PointToClient(beginMousePosition), PointToClient(MousePosition));
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp( MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isLeftMouseDown && beginMousePosition.GetDistance(MousePosition) > 2)
            {
                isLeftMouseDown = false;
                var begin = PointToClient(beginMousePosition).ScaleSmallerPoint(scale);
                var end = PointToClient(MousePosition).ScaleSmallerPoint(scale);
                AddAnnObject(AnnObjectType.Ruler, frameIndex, begin, end);
                return;
            }
            base.OnMouseUp(e);
        }

        #endregion
    }
}
