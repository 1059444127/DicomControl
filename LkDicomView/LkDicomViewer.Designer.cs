using LkDicomView.Controls;

namespace LkDicomView
{
    partial class LkDicomViewer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.frameIndexLabel = new System.Windows.Forms.Label();
            this.patientIdLabel = new System.Windows.Forms.Label();
            this.patientNameLabel = new System.Windows.Forms.Label();
            this.windowLabel = new System.Windows.Forms.Label();
            this.patientAddressLabel = new System.Windows.Forms.Label();
            this.imageBox = new LkDicomView.Controls.DicomImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.LargeChange = 1;
            this.vScrollBar.Location = new System.Drawing.Point(870, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 661);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // frameIndexLabel
            // 
            this.frameIndexLabel.AutoSize = true;
            this.frameIndexLabel.BackColor = System.Drawing.Color.Transparent;
            this.frameIndexLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.frameIndexLabel.ForeColor = System.Drawing.Color.White;
            this.frameIndexLabel.Location = new System.Drawing.Point(17, 15);
            this.frameIndexLabel.Name = "frameIndexLabel";
            this.frameIndexLabel.Size = new System.Drawing.Size(53, 20);
            this.frameIndexLabel.TabIndex = 2;
            this.frameIndexLabel.Text = "Frame:";
            // 
            // patientIdLabel
            // 
            this.patientIdLabel.AutoSize = true;
            this.patientIdLabel.BackColor = System.Drawing.Color.Transparent;
            this.patientIdLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.patientIdLabel.ForeColor = System.Drawing.Color.White;
            this.patientIdLabel.Location = new System.Drawing.Point(17, 42);
            this.patientIdLabel.Name = "patientIdLabel";
            this.patientIdLabel.Size = new System.Drawing.Size(75, 20);
            this.patientIdLabel.TabIndex = 3;
            this.patientIdLabel.Text = "PatientID:";
            // 
            // patientNameLabel
            // 
            this.patientNameLabel.AutoSize = true;
            this.patientNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.patientNameLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.patientNameLabel.ForeColor = System.Drawing.Color.White;
            this.patientNameLabel.Location = new System.Drawing.Point(17, 69);
            this.patientNameLabel.Name = "patientNameLabel";
            this.patientNameLabel.Size = new System.Drawing.Size(100, 20);
            this.patientNameLabel.TabIndex = 4;
            this.patientNameLabel.Text = "PatientName:";
            // 
            // windowLabel
            // 
            this.windowLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.windowLabel.AutoSize = true;
            this.windowLabel.BackColor = System.Drawing.Color.Transparent;
            this.windowLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.windowLabel.ForeColor = System.Drawing.Color.White;
            this.windowLabel.Location = new System.Drawing.Point(17, 625);
            this.windowLabel.Name = "windowLabel";
            this.windowLabel.Size = new System.Drawing.Size(68, 20);
            this.windowLabel.TabIndex = 5;
            this.windowLabel.Text = "WL: WW:";
            // 
            // patientAddressLabel
            // 
            this.patientAddressLabel.AutoSize = true;
            this.patientAddressLabel.BackColor = System.Drawing.Color.Transparent;
            this.patientAddressLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.patientAddressLabel.ForeColor = System.Drawing.Color.White;
            this.patientAddressLabel.Location = new System.Drawing.Point(17, 96);
            this.patientAddressLabel.Name = "patientAddressLabel";
            this.patientAddressLabel.Size = new System.Drawing.Size(149, 20);
            this.patientAddressLabel.TabIndex = 6;
            this.patientAddressLabel.Text = "PatientAddressLabel:";
            // 
            // imageBox
            // 
            this.imageBox.FrameIndex = 0;
            this.imageBox.Location = new System.Drawing.Point(172, 57);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(512, 512);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            this.imageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_OnMouseDown);
            this.imageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Move_OnMouseMove);
            this.imageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Move_OnMouseUp);
            // 
            // LkDicomViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.patientAddressLabel);
            this.Controls.Add(this.windowLabel);
            this.Controls.Add(this.patientNameLabel);
            this.Controls.Add(this.patientIdLabel);
            this.Controls.Add(this.frameIndexLabel);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.imageBox);
            this.DoubleBuffered = true;
            this.Name = "LkDicomViewer";
            this.Size = new System.Drawing.Size(887, 661);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_OnMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Move_OnMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Move_OnMouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DicomImageBox imageBox;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.Label frameIndexLabel;
        private System.Windows.Forms.Label patientIdLabel;
        private System.Windows.Forms.Label patientNameLabel;
        private System.Windows.Forms.Label windowLabel;
        private System.Windows.Forms.Label patientAddressLabel;
    }
}
