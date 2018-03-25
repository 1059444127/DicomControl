using LkDicomView.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LkDicomView.AnnObjects
{
    public abstract class AnnObject : Panel
    {
        public AnnObject()
        {
            SetStyle(ControlStyles.Opaque, true);
        }

        private PointF beginPosition;
        private PointF endPosition;

        public PointF BeginPosition
        {
            get {
                return beginPosition;
            }
            set {
                beginPosition = value;
                Invalidate();
            }
        }

        public PointF EndPosition
        {
            get
            {
                return endPosition;
            }
            set
            {
                endPosition = value;
                Invalidate();
            }
        }

        public int PenWidth { get; set; } = 2;

        public int FrameIndex { get; set; }

        private float scale;
        public new float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                Invalidate();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;  // 开启 WS_EX_TRANSPARENT,使控件支持透明
                return cp;
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; Invalidate(); }
        }

        /// <summary>
        /// 自定义绘制窗体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(0, BackColor), 0), 0, 0, Size.Width, Size.Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, BackColor)), 0, 0, Size.Width, Size.Height);
            if (IsSelected)
            {
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.White),2),e.ClipRectangle);
            }
            base.OnPaint(e);
        }

        protected List<PointF> SetLocation()
        {
            return SetLocation(new List<PointF>() { BeginPosition, EndPosition });
        }

        protected List<PointF> SetLocation(List<PointF> points)
        {
            var scaledPoints = points.Select(a=>a.ScaleBiggerPoint(Scale));
            var maxX = scaledPoints.Max(a => a.X);
            var minX = scaledPoints.Min(a => a.X);
            var maxY = scaledPoints.Max(a => a.Y);
            var minY = scaledPoints.Min(a => a.Y);
            Width = (int)(maxX - minX) + 50;
            Height = (int)(maxY - minY) + 20;
            Top = (int)minY;
            Left = (int)minX;

            var result = new List<PointF>();
            foreach(var i in scaledPoints)
            {
                result.Add(new PointF() {
                    X = i.X - Left,
                    Y = i.Y - Top,
                });
            }
            return result;
        }
    }
}
