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

        private Bitmap bmp;

        protected const int WWidth = Form1.WWidth;
        protected const int WHeight = Form1.WHeight;

        protected abstract Bitmap DrawImage();

        /// <summary>
        /// 构造函数
        /// </summary>
        protected SetImage(double xmin, double xmax, double ymin, double ymax)
        {
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;
            xcenter = (xmin + xmax) / 2;
            ycenter = (ymin + ymax) / 2;
        }

    }
}
