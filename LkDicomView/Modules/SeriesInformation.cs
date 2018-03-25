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
        private float scale = 1;
        private bool isLazyLoad;
        public SeriesInformation(bool isLazyLoad = true)
        {
            this.isLazyLoad = isLazyLoad;
            imageFrames = new List<DicomImage>();
            imageTempFrames = new List<Image>();
        }

        private List<DicomImage> imageFrames { get; set; }

        private List<Image> imageTempFrames { get; set; }

        public DicomDataset DicomMetaInfo { get; set; }

        private void RefreshFramesToTemp()
        {
            imageTempFrames.Clear();
            foreach (var image in imageFrames)
            {
                for (var i = 0; i < image.NumberOfFrames; i++)
                {
                    imageTempFrames.Add(new Bitmap(image.RenderImage(i).AsBitmap()));
                }
            }
        }

        public void AddFrames(DicomImage image)
        {
            imageFrames.Add(image);
            if (!isLazyLoad)
            {
                for (var i = 0; i < image.NumberOfFrames; i++)
                {
                    imageTempFrames.Add(new Bitmap(image.RenderImage(i).AsBitmap()));
                }
            }   
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
                if (!isLazyLoad)
                {
                    RefreshFramesToTemp();
                }
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
                if (!isLazyLoad)
                {
                    RefreshFramesToTemp();
                }
            }
        }

        public float Scale
        {
            get
            {
                return (float)Math.Round(scale, 1);
            }
            set
            {
                if (value > 5)
                {
                    value = 5;
                }
                if (value < 0.5f)
                {
                    value = 0.5f;
                }
                scale = value;
                imageFrames.ForEach(a => a.Scale = value);
                if (!isLazyLoad)
                {
                    RefreshFramesToTemp();
                }
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
                if (isLazyLoad)
                {
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
                else
                {
                    return imageTempFrames[index];
                }
                
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
