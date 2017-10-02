using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using System.Numerics;
using static MathPaint.MandelbrotImage;
using System.Drawing;
using static MathPaint.Palette;

namespace MathPaint
{
    class MandelbrotColorArray
    {

        /// <summary>
        /// 最大循环次数
        /// </summary>
        protected const int MaxIterations = 1000;

        /// <summary>
        /// 最大的复数的模
        /// </summary>
        protected const int EscapeRadius = 256;  //2^8

        /// <summary>
        /// 不大于此值时取背景色
        /// </summary>
        protected const int BGIterations = 11;


        private int[,] iterationsArray;             //readonly
        private double[,] DoubleIterationsArray;    //readonly
        private double[] NormalizedArray;           //readonly
        private int[] histogram = new int[MaxIterations + 1];


        //颜色变量
        protected readonly Color BGColor = Color.White;
        protected readonly Color CoreColor = Color.Black;

        readonly int WWidth, WHeight;

        /// <summary>
        /// 待重写
        /// </summary>
        public MandelbrotColorArray(double xmin, double xmax, double ymin, double ymax,
                                    int WWidth, int WHeight)
        {
            this.WWidth = WWidth;
            this.WHeight = WHeight;

            iterationsArray = new int[WWidth, WHeight];   // z迭代的次数，初始值为0
            DoubleIterationsArray = new double[WWidth, WHeight];

            GetIterationsArray(xmin, xmax, ymin, ymax);
            NormalizedArray = GetNormalizedArray();

        }

        /// <summary>
        /// 得到颜色
        /// IterationsToColor
        /// </summary>
        public Color at(int px, int py)
        {
            int iterations = iterationsArray[px, py];
            if (iterations >= MaxIterations)
                return CoreColor;
            else if (iterations <= BGIterations)
                return BGColor;     //return HueToColor(WxWyToBGHue(px, py));
            else {
                double DoubleIterations = DoubleIterationsArray[px, py];
                return HueToCyclicColor(NormalizeIterations(iterations, DoubleIterations));
            }

        }



        private void GetIterationsArray(double xmin, double xmax, double ymin, double ymax)
        {
            double dx = (xmax - xmin) / WWidth;
            double dy = (ymax - ymin) / WHeight;
            Complex c, z;
            double nu;

            //通过循环开始判断与绘制点
            for (int py = 0; py < WHeight; ++py) {
                for (int px = 0; px < WWidth; ++px) {

                    //判断点是否属于集合
                    c = new Complex(xmin + dx * px, ymin + dy * py);  //x=xmin+dx*px, y=ymin+dy*py
                    z = Complex.Zero;

                    while (z.Magnitude < EscapeRadius && iterationsArray[px, py] < MaxIterations) {
                        z = z * z + c;
                        ++iterationsArray[px, py];
                    }

                    DoubleIterationsArray[px, py] = iterationsArray[px, py];

                    ++histogram[iterationsArray[px, py]];
                    if (iterationsArray[px, py] < MaxIterations && iterationsArray[px, py] > BGIterations) {

                        // potential function
                        nu = Log(Log(z.Magnitude) / Log(2)) / Log(2);
                        DoubleIterationsArray[px, py] += 3 - nu;

                        //++histogram[iterations[px, py]];
                    }

                }
            }
        }

        /// <summary>
        /// 使用柱状图法
        /// 得到0~1之间的数组成的数组
        /// 目前的算法似乎有问题，因为放大以后颜色会变化
        /// FIXME: 继承时有问题，待修改
        /// </summary>
        private double[] GetNormalizedArray()
        {
            double total = 0;
            for (int i = BGIterations; i < MaxIterations; ++i)
                total += histogram[i];

            double[] NormalizedArray = new double[MaxIterations]; //初始值为0
            for (int i = BGIterations; i < MaxIterations; ++i) {
                NormalizedArray[i] = NormalizedArray[i - 1] + histogram[i] / total;
            }
            return NormalizedArray;

        }

        /// <summary>
        /// 使用柱状图法和线性拟合
        /// 得到0~1之间的数组成的数组
        /// </summary>
        private double NormalizeIterations(int iterations, double DoubleIterations)
        {


            double value1 = NormalizedArray[iterations - 1];
            double value2 = NormalizedArray[iterations];
            // iteration % 1 = Iterations - (int)Iterations = fractional part of iteration.
            double value = value1 + (value2 - value1) * (DoubleIterations - (int)DoubleIterations);

            //将hue取值范围变换成0~1
            double valueMin = NormalizedArray[BGIterations];
            double valueMax = NormalizedArray[MaxIterations - 1];
            value = (value - valueMin) / (valueMax - valueMin);

            return value;
        }



        /// <summary>
        /// 得到0~1之间的数
        /// </summary>
        private double BGPixelNormalization(int px, int py)
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


    }
}
