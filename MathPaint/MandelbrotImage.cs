using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;


namespace MathPaint
{
    class MandelbrotImage : SetImage
    {
        /// <summary>
        /// 最大循环次数
        /// </summary>
        const int MaxRepeats = 1000;

        /// <summary>
        /// 不大于此值时取背景色
        /// </summary>
        const int BGRepeats = 11;

        //颜色变量
        readonly Color BGColor = Color.OrangeRed;
        readonly Color CoreColor = Color.Black;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MandelbrotImage(double xmin0, double xmax0, double ymin0, double ymax0)
            : base(xmin0, xmax0, ymin0, ymax0) { }

        /// <summary>
        /// 绘制图形
        /// </summary>
        protected override Bitmap DrawImage()
        {
            double dx = (xmax - xmin) / WWidth;
            double dy = (ymax - ymin) / WHeight;
            Bitmap bmp = new Bitmap(WWidth, WHeight);
            Complex c, z;
            Color PixelColor;
            int repeats;

            //通过循环开始判断与绘制点
            for (int wy = 0; wy < WHeight; ++wy) {
                for (int wx = 0; wx < WWidth; ++wx) {
                    //判断点是否属于集合
                    c = new Complex(xmin + dx * wx, ymin + dy * wy);  //x=xmin+dx*wx, y=ymin+dy*wy
                    z = Complex.Zero;
                    repeats = 0;
                    do {
                        z = z * z + c;
                        ++repeats;
                    } while (z.Magnitude <= 2 && repeats <= MaxRepeats);


                    //绘制点
                    if (repeats > MaxRepeats)
                        PixelColor = CoreColor;
                    else if (repeats <= BGRepeats)
                        PixelColor = HueToColor(WxWyToBGHue(wx, wy));
                    else
                        PixelColor = HueToColor(RepeatsToHue(repeats));
                    bmp.SetPixel(wx, wy, PixelColor);
                }
            }

            return bmp;
        }

        /// <summary>
        /// 得到0~1之间的数
        /// </summary>
        private double WxWyToBGHue(int wx, int wy)
        {
            int z = wx + wy;
            int max = WWidth + WHeight;
            int min = 0;
            double maxHue = 0.2;
            double minHue = 0.05;

            if (z > max) return maxHue;
            if (z < min) return minHue;
            return minHue + ((z - min) / (max - min)) * (maxHue - minHue);
        }

        /// <summary>
        /// 得到0~1之间的数
        /// </summary>
        private double RepeatsToHue(int repeats)
        {
            double max = MaxRepeats + 1;
            double min = BGRepeats + 1;
            if (repeats > max) return 1;
            if (repeats < min) return 0;
            return (repeats - min) / (max - min);
        }

        /// <summary>
        /// 将0~1之间的数值转化为颜色 
        /// </summary>
        private Color HueToColor(double hue)
        {
            // 0<=hue<=1

            int r = 0, g = 0, b = 0;
            double dDeltaHue = hue * 5.0;
            int iHue = (int)dDeltaHue;
            dDeltaHue -= iHue;
            switch (iHue) {
                case 0:
                    r = 255;
                    g = (int)(255 * dDeltaHue);
                    break;
                case 1:
                    g = 255;
                    r = (int)(255 - dDeltaHue * 255);
                    break;
                case 2:
                    g = 255;
                    b = (int)(dDeltaHue * 255);
                    break;
                case 3:
                    b = 255;
                    g = (int)(255 - dDeltaHue * 255);
                    break;
                case 4:
                    b = 255;
                    r = (int)(dDeltaHue * 255);
                    break;
                case 5: //hue=1
                    b = 255;
                    r = 255;
                    break;
            }
            return Color.FromArgb(r, g, b);
        }

    }
}
