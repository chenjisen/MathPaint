using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using System.Numerics;
using static MathPaint.MandelbrotImage;

namespace MathPaint
{
    class BuddhabrotIterations:MandelbrotColorArray
    {
        private readonly int[] histogram;
        public readonly new int[,] iterations;
        private readonly double[,] DoubleIterations;
        private readonly double[] NormalizedArray;

        /// <summary>
        /// 待重写
        /// </summary>
        public BuddhabrotIterations(double xmin, double xmax, double ymin, double ymax, 
            int WWidth, int WHeight) : base(xmin, xmax, ymin, ymax, WWidth, WHeight)
        {
        }

        /*
        /// <summary>
        /// 待重写
        /// </summary>
        public BuddhabrotIterations(double xmin, double xmax, double ymin, double ymax)
        {

            iterations = new int[WWidth, WHeight];   // z迭代的次数
            DoubleIterations = new double[WWidth, WHeight];
            histogram = new int[MaxIterations + 1];

            double deltax = xmax - xmin;
            double deltay = ymax - ymin;

            double dx = (xmax - xmin) / WWidth;
            double dy = (ymax - ymin) / WHeight;

            double nu, rndx, rndy;
            Random rnd = new Random();
            byte[] b = new byte[WWidth * WHeight * 2];
            rnd.NextBytes(b);

            Complex c, z;

            //通过循环开始判断与绘制点
            for (int py = 0; py < WHeight; ++py) {
                for (int px = 0; px < WWidth; ++px) {

                    rndx = b[(py * WWidth + px) * 2] / (double)byte.MaxValue;
                    rndy = b[(py * WWidth + px) * 2 + 1] / (double)byte.MaxValue;

                    //判断点是否属于集合
                    c = new Complex(xmin + dx * px, ymin + dy * py);  //x=xmin+dx*px, y=ymin+dy*py
                    z = new Complex(xmin + deltax * rndx, ymin + deltay * rndy);
                    iterations[px, py] = 0;

                    while (z.Magnitude < EscapeRadius && iterations[px, py] < MaxIterations) {
                        z = z * z + c;
                        ++iterations[px, py];
                    }
                    DoubleIterations[px, py] = iterations[px, py];

                    ++histogram[iterations[px, py]];
                    if (iterations[px, py] < MaxIterations && iterations[px, py] > BGIterations) {

                        // potential function
                        nu = Log(Log(z.Magnitude) / Log(2)) / Log(2);
                        DoubleIterations[px, py] += 3 - nu;

                        //++histogram[iterations[px, py]];
                    }

                }
            }

            NormalizedArray = GetNormalizedArray();

        }
        */

    }
}
