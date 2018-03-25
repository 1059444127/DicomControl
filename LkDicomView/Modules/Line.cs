using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LkDicomView.Modules
{
    public class Line
    {
        public Point StartLocation { get; set; }
        public Point EndLocation { get; set; }
        public int Width { get; set; }
    }
}
