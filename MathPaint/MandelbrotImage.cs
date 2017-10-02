using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using static System.Math;
using static MathPaint.Palette;
using static MathPaint.MandelbrotColorArray;

namespace MathPaint
{
    class MandelbrotImage
    {
        public readonly Bitmap bmp;
        public readonly double xmin, xmax, ymin, ymax;


        protected const int WWidth = Form1.WWidth;
        protected const int WHeight = Form1.WHeight;

        private readonly MandelbrotColorArray ColorArray;


        /// <summary>
        /// 构造函数
        /// </summary>
        public MandelbrotImage(double xmin, double xmax, double ymin, double ymax)
        {
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;
            //得到迭代次数
            ColorArray = new MandelbrotColorArray(xmin, xmax, ymin, ymax, WWidth, WHeight);//迭代次数数组

            //得到图像
            bmp = DrawImage();

        }

        /// <summary>
        /// 绘制图形
        /// override
        /// </summary>
        protected  Bitmap DrawImage()
        {

            Bitmap bmp = new Bitmap(WWidth, WHeight);

            Color PixelColor;



            for (int py = 0; py < WHeight; ++py) {
                for (int px = 0; px < WWidth; ++px) {

                    //得到颜色
                    //PixelColor = IterationsToColor(iterations[px, py], DoubleIterations[px, py],
                    // NormalizedArray, px, py);
                    PixelColor = ColorArray.at(px, py);
                    //绘制点
                    bmp.SetPixel(px, py, PixelColor);
                }
            }

            return bmp;
        }





    }
}
