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
            this.imageBox = new LkDicomView.Controls.DicomImageBox();
            this.patientIdLabel = new System.Windows.Forms.Label();
            this.patientNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(740, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 464);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // frameIndexLabel
            // 
            this.frameIndexLabel.AutoSize = true;
            this.frameIndexLabel.BackColor = System.Drawing.Color.Transparent;
            this.frameIndexLabel.ForeColor = System.Drawing.Color.White;
            this.frameIndexLabel.Location = new System.Drawing.Point(18, 14);
            this.frameIndexLabel.Name = "frameIndexLabel";
            this.frameIndexLabel.Size = new System.Drawing.Size(41, 12);
            this.frameIndexLabel.TabIndex = 2;
            this.frameIndexLabel.Text = "Frame:";
            // 
            // imageBox
            // 
            this.imageBox.FrameIndex = 0;
            this.imageBox.Location = new System.Drawing.Point(111, 32);
            this.imageBox.Name = "imageBox";
            this.imageBox.Scale = 0F;
            this.imageBox.Size = new System.Drawing.Size(477, 377);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            this.imageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_OnMouseDown);
            this.imageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Move_OnMouseMove);
            this.imageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Move_OnMouseUp);
            // 
            // patientIdLabel
            // 
            this.patientIdLabel.AutoSize = true;
            this.patientIdLabel.BackColor = System.Drawing.Color.Transparent;
            this.patientIdLabel.ForeColor = System.Drawing.Color.White;
            this.patientIdLabel.Location = new System.Drawing.Point(18, 34);
            this.patientIdLabel.Name = "patientIdLabel";
            this.patientIdLabel.Size = new System.Drawing.Size(65, 12);
            this.patientIdLabel.TabIndex = 3;
            this.patientIdLabel.Text = "PatientID:";
            // 
            // patientNameLabel
            // 
            this.patientNameLabel.AutoSize = true;
            this.patientNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.patientNameLabel.ForeColor = System.Drawing.Color.White;
            this.patientNameLabel.Location = new System.Drawing.Point(18, 54);
            this.patientNameLabel.Name = "patientNameLabel";
            this.patientNameLabel.Size = new System.Drawing.Size(77, 12);
            this.patientNameLabel.TabIndex = 4;
            this.patientNameLabel.Text = "PatientName:";
            // 
            // LkDicomViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.patientNameLabel);
            this.Controls.Add(this.patientIdLabel);
            this.Controls.Add(this.frameIndexLabel);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.imageBox);
            this.DoubleBuffered = true;
            this.Name = "LkDicomViewer";
            this.Size = new System.Drawing.Size(757, 464);
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
    }
}
