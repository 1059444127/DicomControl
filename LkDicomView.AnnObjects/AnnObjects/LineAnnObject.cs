using LkDicomView.AnnObjects.Enums;
using LkDicomView.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LkDicomView.AnnObjects.AnnObjects
{
    public class LineAnnObject: AnnObject
    {
        public LineAnnObject(Point drawStartPosition, Point drawEndPosition):base(drawStartPosition, drawEndPosition)
        {

        }
    }
}
