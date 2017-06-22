using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MathPaint
{
    abstract class SetImage
    {
        protected readonly double xmin, xmax, ymin, ymax, xcenter, ycenter;
        public readonly Bitmap bmp;

        protected const int WWidth = Form1.WWidth;
        protected const int WHeight = Form1.WHeight;

        protected abstract Bitmap DrawImage();

        /// <summary>
        /// 构造函数
        /// </summary>
        protected SetImage(double xmin0, double xmax0, double ymin0, double ymax0)
        {
            xmin = xmin0;
            xmax = xmax0;
            ymin = ymin0;
            ymax = ymax0;
            xcenter = (xmin0 + xmax0) / 2;
            ycenter = (ymin0 + ymax0) / 2;

            bmp = DrawImage();
        }

    }
}
