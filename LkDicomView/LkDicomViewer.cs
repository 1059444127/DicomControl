using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dicom.Imaging;
using Dicom;
using System.IO;
using LkDicomView.Modules;
using LkDicomView.Controls;
using System;
using System.Text;

namespace LkDicomView
{
    public partial class LkDicomViewer : UserControl
    {
        public LkDicomViewer()
        {
            InitializeComponent();
            seriesInformation = new SeriesInformation();
            imageBox.Parent = this;
            imageBox.MouseDown += Move_OnMouseDown;
            imageBox.MouseMove += Move_OnMouseMove;
            imageBox.MouseUp += Move_OnMouseUp;
        }

        private SeriesInformation seriesInformation;
        private int currentFrameIndex;

        public DicomImageBox ImageSpace
        {
            get
            {
                return imageBox;
            }
        }

        public int CurrentFrameIndex
        {
            get
            {
                return currentFrameIndex;
            }
            set
            {
                if (seriesInformation.FramesCount == 0)
                {
                    currentFrameIndex = 0;
                    return;
                }
                currentFrameIndex = value;
                imageBox.Image = seriesInformation[value];
                imageBox.FrameIndex = currentFrameIndex;
                vScrollBar.Value = currentFrameIndex;
                SetLabel();
            }
        }

        private void SetLabel()
        {
            frameIndexLabel.Text = $"Frame: {currentFrameIndex + 1}/{seriesInformation.FramesCount}";
            var metaInfo = seriesInformation.GetCurrentDicomImage(currentFrameIndex).Dataset;
            patientIdLabel.Text = $"PatientID: {metaInfo.Get<string>(DicomTag.PatientID)}";
            patientNameLabel.Text = $"PatientName: {metaInfo.Get<string>(DicomTag.PatientName)}";
            
        }

        public void LoadDicomFile(string fileName)
        {            
            var dicomImage = new DicomImage(fileName);

            for (var i = 0; i < dicomImage.NumberOfFrames; i++)
            {
                seriesInformation.AddFrames(dicomImage);
            }
            imageBox.Image = seriesInformation[0];
            imageBox.Width = seriesInformation[0].Width;
            imageBox.Height = seriesInformation[0].Height;
            imageBox.FrameIndex = currentFrameIndex;
            vScrollBar.Maximum = seriesInformation.FramesCount - 1;
            vScrollBar.Value = currentFrameIndex;
            SetWindowLevel(40, 400);
            if (seriesInformation.FramesCount == 1)
            {
                SetLabel();
            }
        }

        public void LoadDicomFiles(List<string> fileNames)
        {
            fileNames.ForEach(i => LoadDicomFile(i));
        }

        public void LoadDicomDirectory(string directoryName)
        {
            LoadDicomFiles(Directory.EnumerateFiles(directoryName).ToList());
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                var tempLocation = imageBox.Location;
                var scaleType = e.Delta > 0 ? 1.1f : 0.9f;
                imageBox.CurrentScale = imageBox.CurrentScale * scaleType;
                imageBox.Scale(new SizeF(scaleType, scaleType));
                imageBox.Location = tempLocation;
            }
            else
            {
                if (e.Delta > 0 && CurrentFrameIndex > 0)
                {
                    CurrentFrameIndex = CurrentFrameIndex - 1;
                }
                else if (e.Delta < 0 && CurrentFrameIndex < seriesInformation.FramesCount - 1)
                {
                    CurrentFrameIndex = CurrentFrameIndex + 1;
                }
            }
            base.OnMouseWheel(e);
        }

        #region 拖动
        private bool isRightMouseDown;
        private Point beginMousePosition;
        private int beginX;
        private int beginY;
        private void Move_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isRightMouseDown = true;
                beginMousePosition = MousePosition;
                beginX = imageBox.Left;
                beginY = imageBox.Top;
            }
        }

        protected void Move_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && isRightMouseDown)
            {
                imageBox.Top = beginY + (MousePosition.Y - beginMousePosition.Y);
                imageBox.Left = beginX + (MousePosition.X - beginMousePosition.X);
            }
        }

        protected void Move_OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isRightMouseDown = false;
            }
        }
        #endregion

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if(e.OldValue == e.NewValue)
            {
                return;
            }
            CurrentFrameIndex = e.NewValue;
        }

        public void SetWindowLevel(int windowCenter, int windowWidth)
        {
            seriesInformation.WindowCenter = windowCenter;
            seriesInformation.WindowWidth = windowWidth;
            windowLabel.Text = $"WL:{windowCenter} WW:{windowWidth}";
            CurrentFrameIndex = CurrentFrameIndex;
        }

        public void SaveImage(bool hasSign, string path)
        {
            if (hasSign)
            {
                var image = new Bitmap(imageBox.Image);
                var g = Graphics.FromImage(image);
                var annObjects = imageBox.AnnObjectContainer.Where(a => a.FrameIndex == CurrentFrameIndex);
                foreach(var i in annObjects)
                {
                    i.Draw(g);
                }

                g.Save();
                image.Save(path);
            }
            else
            {
                imageBox.Image.Save(path);
            }
        }
    }
}
