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

        public int PenWidth { get; set; } = 2;

        public int FrameIndex { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;  // 开启 WS_EX_TRANSPARENT,使控件支持透明
                return cp;
            }
        }

        protected Point p1;
        protected Point p2;

        protected void SetLocation()
        {
            Top = Math.Min(p1.Y, p2.Y);
            Left = Math.Min(p1.X, p2.X);
            Width = Math.Abs(p2.X - p1.X) + 40;
            Height = Math.Abs(p2.Y - p1.Y) + 25;
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
    }
}
