using Dicom;
using Dicom.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LkDicomView.Modules
{
    public class SeriesInformation
    {
        private int windowWidth = 400;
        private int windowCenter = 40;

        public SeriesInformation()
        {
            imageFrames = new List<DicomImage>();
            imageTempFrames = new List<Image>();
        }

        private List<DicomImage> imageFrames { get; set; }

        private List<Image> imageTempFrames { get; set; }

        public DicomDataset DicomMetaInfo { get; set; }

        public void AddFrames(DicomImage image)
        {
            image.ShowOverlays = true;
            imageFrames.Add(image); 
        }

        public int WindowWidth
        {
            get
            {
                return windowWidth;
            }
            set
            {
                windowWidth = value;
                imageFrames.ForEach(a => a.WindowWidth = value);
            }
        }
        public int WindowCenter
        {
            get
            {
                return windowCenter;
            }
            set
            {
                windowCenter = value;
                imageFrames.ForEach(a => a.WindowCenter = value);
            }
        }

        public DicomImage GetCurrentDicomImage(int index)
        {
            if (index > FramesCount)
            {
                throw new IndexOutOfRangeException("index超过总帧数");
            }
            var location = 0;
            foreach (var i in imageFrames)
            {
                if (location + i.NumberOfFrames < index)
                {
                    location = location + i.NumberOfFrames;
                }
                else
                {
                    return i;
                }
            }
            throw new Exception("未知错误");
        }

        public Image this[int index]
        {
            get
            {
                if (index > FramesCount)
                {
                    throw new IndexOutOfRangeException("index超过总帧数");
                }
                var location = 0;
                foreach (var i in imageFrames)
                {
                    if (location + i.NumberOfFrames < index)
                    {
                        location = location + i.NumberOfFrames;
                    }
                    else
                    {
                        var renderIndex = index - location - 1;
                        if (renderIndex == -1)
                        {
                            renderIndex = 0;
                        }
                        return new Bitmap(i.RenderImage(renderIndex).AsBitmap());
                    }
                }
                throw new Exception("未知错误");
            }
        }

        public int FramesCount
        {
            get
            {
                return imageFrames.Sum(a => a.NumberOfFrames);
            }
        }
    }
}
