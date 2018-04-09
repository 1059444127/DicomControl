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
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            AnnObjectContainer = new AnnObjectContainer();

        }

        protected override bool ScaleChildren => true;

        public void AddAnnObject(AnnObjectType annObjectType, int frameIndex, Point p1, Point p2)
        {
            var annObject = AnnObjectContainer.CreateAnnObject(annObjectType, p1, p2);
            annObject.Parent = this;
            annObject.AutoSize = true;
            annObject.BackColor = Color.Black;
            annObject.FrameIndex = frameIndex;
            
            annObject.MouseDown += new MouseEventHandler((o, e) => {
                if(e.Button == MouseButtons.Left)
                {
                    var selected = (AnnObject)o;
                    selected.IsSelected = true;
                }
            });
            AnnObjectContainer.Add(annObject);
            ShowAnnObjects();
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
                ShowAnnObjects();
            }
        }

        private void ShowAnnObjects()
        {
            foreach (var i in AnnObjectContainer)
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

            AnnObjectContainer.ForEach(a => a.IsSelected = false);
            Invalidate();
        }

        public new void OnKeyDown(KeyEventArgs e)
        {
            if(e.KeyData == Keys.Delete)
            {
                var currentSelected = AnnObjectContainer.Where(a => a.FrameIndex == frameIndex && a.IsSelected);
                currentSelected.ToList().ForEach(a => {
                    AnnObjectContainer.Remove(a);
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
                var begin = PointToClient(beginMousePosition);
                var end = PointToClient(MousePosition);
                AddAnnObject(AnnObjectType.Ruler, frameIndex, begin, end);
                return;
            }
            base.OnMouseUp(e);
        }

        #endregion
    }
}
