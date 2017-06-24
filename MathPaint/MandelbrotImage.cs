using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using static System.Math;

namespace MathPaint
{
    class MandelbrotImage : SetImage
    {
        /// <summary>
        /// 最大循环次数
        /// </summary>
        const int MaxIterations = 1000;

        /// <summary>
        /// 最大的复数的模
        /// </summary>
        const int BailoutRadius = 256;  //2^8


        /// <summary>
        /// 不大于此值时取背景色
        /// </summary>
        const int BGIterations = 13;

        //颜色变量
        readonly Color BGColor = Color.DarkBlue;
        readonly Color CoreColor = Color.Black;

        public readonly Bitmap bmp;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MandelbrotImage(double xmin, double xmax, double ymin, double ymax)
            : base(xmin, xmax, ymin, ymax)
        {
            bmp = DrawImage();
        }

        /// <summary>
        /// 绘制图形
        /// </summary>
        protected override Bitmap DrawImage()
        {
            Bitmap bmp = new Bitmap(WWidth, WHeight);
            Complex c, z = new Complex(int.MinValue, int.MinValue);
            Color PixelColor;

            int[,] iterations = new int[WWidth, WHeight];   // z迭代的次数
            double[,] DoubleIterations = new double[WWidth, WHeight];

            /*int iterations = -1; 
            double DoubleIterations = -1;*/

            int[] histogram = new int[MaxIterations + 1];

            double dx = (xmax - xmin) / WWidth;
            double dy = (ymax - ymin) / WHeight;

            double nu;

            //通过循环开始判断与绘制点
            for (int py = 0; py < WHeight; ++py) {
                for (int px = 0; px < WWidth; ++px) {

                    //判断点是否属于集合
                    c = new Complex(xmin + dx * px, ymin + dy * py);  //x=xmin+dx*px, y=ymin+dy*py
                    z = Complex.Zero;
                    iterations[px, py] = 0;

                    while (z.Magnitude < BailoutRadius && iterations[px, py] < MaxIterations) {
                        z = z * z + c;
                        ++iterations[px, py];
                    }
                    DoubleIterations[px, py] = iterations[px, py];

                    ++histogram[iterations[px, py]];
                    if (iterations[px, py] < MaxIterations && iterations[px, py] > BGIterations) {

                        // sqrt of inner term removed using log simplification rules.
                        nu = Log(Log(z.Magnitude) / Log(2)) / Log(2);
                        // Rearranging the potential function.
                        // Dividing log_zn by log(2) instead of log(N = 1<<8)
                        // because we want the entire palette to range from the
                        // center to radius 2, NOT our bailout radius.
                        DoubleIterations[px, py] += 3 - nu;

                        //++histogram[iterations[px, py]];
                    }

                }
            }

            double[] hueArray = IterationsToHueArray(histogram);

            for (int py = 0; py < WHeight; ++py) {
                for (int px = 0; px < WWidth; ++px) {

                    //得到颜色
                    PixelColor = IterationsToColor(iterations[px, py], DoubleIterations[px, py],
                         hueArray, px, py);

                    //绘制点
                    bmp.SetPixel(px, py, PixelColor);
                }
            }

            return bmp;
        }

        private Color IterationsToColor(int iterations, double DoubleIterations,
            double[] hueArray, int px, int py)
        {

            if (iterations >= MaxIterations)
                return CoreColor;
            else if (iterations <= BGIterations)
                return BGColor;     //return HueToColor(WxWyToBGHue(px, py));
            else
                return HueToCyclicColor(DoubleIterationsToHue(DoubleIterations, iterations, hueArray));
        }

        /// <summary>
        /// 使用柱状图法
        /// 得到0~1之间的数组成的数组
        /// </summary>
        private double[] IterationsToHueArray(int[] histogram)
        {
            double total = 0;
            for (int i = BGIterations; i < MaxIterations; ++i)
                total += histogram[i];

            double[] hueArray = new double[MaxIterations]; //初始值为0
            for (int i = BGIterations; i < MaxIterations; ++i) {
                hueArray[i] = hueArray[i - 1] + histogram[i] / total;
            }

            return hueArray;
        }

        /// <summary>
        /// Linear Interpolate
        /// </summary>
        private double DoubleIterationsToHue(double DoubleIterations, int iterations,
                                             double[] hueArray)
        {
            double hue1 = hueArray[iterations - 1];
            double hue2 = hueArray[iterations];
            // iteration % 1 = fractional part of iteration.
            double hue = hue1 + (hue2 - hue1) * (DoubleIterations % 1);

            //将hue取值范围变换成0~1
            double hueMin = hueArray[BGIterations];
            double hueMax = hueArray[MaxIterations - 1];
            hue = (hue - hueMin) / (hueMax - hueMin);

            return hue;
        }


        /// <summary>
        /// 得到0~1之间的数
        /// </summary>
        private double WxWyToBGHue(int px, int py)
        {
            double z = px + py;
            int max = WWidth + WHeight;
            int min = 0;
            double maxHue = 0.5;//0.2
            double minHue = 0;//0.05

            if (z > max) return maxHue;
            if (z < min) return minHue;
            return minHue + ((z - min) / (max - min)) * (maxHue - minHue);
        }

        /// <summary>
        /// 得到0~1之间的数
        /// </summary>
        private double IterationsToHue(int iterations)
        {
            double max = MaxIterations + 1;
            double min = BGIterations + 1;
            if (iterations > max) return 1;
            if (iterations < min) return 0;
            return (iterations - min) / (max - min);
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


        /// <summary>
        /// 将0~1之间的数值转化为可循环的颜色 
        /// </summary>
        private Color HueToCyclicColor(double hue)
        {
            if (hue <= 0.5)
                return HueToColor(hue * 2);
            else
                return HueToColor(2 - hue * 2);
        }
    }
}
