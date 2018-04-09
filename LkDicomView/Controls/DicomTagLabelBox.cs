using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LkDicomView.Controls
{
    public class DicomTagLabelBox : Control
    {
        public DicomTagLabelBox()
        {
            SetStyle(ControlStyles.Opaque, true);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(0, BackColor), 0), 0, 0, Size.Width, Size.Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, BackColor)), 0, 0, Size.Width, Size.Height);
            if (string.IsNullOrEmpty(Text))
            {
                Size = new Size(0, Size.Height);
            }
            else
            {
                Size = new Size(15 * Text.Length, Size.Height);
            }
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 0, 0);

            base.OnPaint(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Refresh();
            Invalidate();
            base.OnTextChanged(e);
        }
    }
}
