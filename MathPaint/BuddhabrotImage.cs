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
    class BuddhabrotImage : MandelbrotImage
    {
        public readonly new Bitmap bmp;

        public BuddhabrotImage(double xmin, double xmax, double ymin, double ymax) : base(xmin, xmax, ymin, ymax)
        {
        }

        //迭代次数

        /*
    /// <summary>
    /// 构造函数
    /// </summary>
    public BuddhabrotImage(double xmin, double xmax, double ymin, double ymax)
    {
        //得到迭代次数
        BuddhabrotIterations BIterations = new BuddhabrotIterations(xmin, xmax, ymin, ymax);

        //得到图像
        bmp = DrawImage(BIterations);
    }*/

        /*
        /// <summary>
        /// 绘制图形
        /// </summary>
        protected override Bitmap DrawImage()
        {
            Bitmap bmp = new Bitmap(WWidth, WHeight);

            Color PixelColor;

            for (int py = 0; py < WHeight; ++py) {
                for (int px = 0; px < WWidth; ++px) {

                    //得到颜色
                    PixelColor = IterationsToColor(BIterations, px, py);

                    //绘制点
                    bmp.SetPixel(px, py, PixelColor);
                }
            }

            return bmp;
        }

        */

        /*
        /// <summary>
        /// 绘制图形
        /// </summary>
        protected override Bitmap DrawImage()
        {


            const int n_max = 1000000;
            const int grid_resolution = 1024;
            const int grid_center = grid_resolution / 2;
            const int miniter = 10000;
            const long batchSize = 1000000;
            const double xmin = -1.0, xmax = 2.0, ymin = -1.3, ymax = 1.3;

            const double intensity = 255;

            int[,] exposureMap = new int[grid_resolution, grid_resolution];

            long i;
            double x, y;
            Complex z, c;
            int iter;
            int TempX, TempY, TempYm;

            int ppm_i, ppm_j, ppm_jhi, ppm_jlo;

            int iterations = 0;   // z迭代的次数

            //$OMP PARALLEL DO DEFAULT(PRIVATE),SHARED(exposureMap)
            for (i = 1; i <= batchSize; ++i) {

                //产生0-1之间的随机数，可能会重复
                Random rnd = new Random();
                x = rnd.NextDouble();
                y = rnd.NextDouble();

                z = new Complex(0, 0);
                c = new Complex((x * 2.5 - 2), y * 1.3); //choose a random point on complex plane

                while (z.Magnitude < EscapeRadius && iterations < MaxIterations) {
                    z = z * z + c;
                    ++iterations;
                }


                    if (iterations  < n_max && iterations > miniter) {
                    for (iter = 1; iter <= n_max; ++i) { //iterate and plot orbit
                        z = z * z + c; //mandelbrot formula : Z = Z²+C
                        if (iter >= miniter) {
                             TempX = (int)(grid_resolution * (z.Real + xmax) / (xmax - xmin));
                             TempY = (int)(grid_resolution * (z.Imaginary + ymax) / (ymax - ymin));
                             TempYm = (int)(grid_center - (TempY - grid_resolution / 2));
                            if ((TempX > 0) && (TempX < grid_resolution)
                                        && (TempY > 0) && (TempY < grid_resolution)) {
                                ++exposureMap[TempX, TempY];
                                ++exposureMap[TempX, TempYm];
                            }
                        }
                    }
                }

                //$END PARALLEL



                        exposureMap = Sqrt(exposureMap / exposureMap.Max()) * intensity;




                int intens = (int)(intensity);


                for (ppm_i = 1; ppm_i <= grid_resolution; ++ppm_i) {
                    for (ppm_jlo = 1; ppm_jlo <= grid_resolution; ppm_jlo += 4) {
                        ppm_jhi = Min(ppm_jlo + 3, grid_resolution);
                        exposureMap(ppm_i, ppm_j;
                        exposureMap(ppm_i, ppm_j);
                        exposureMap(ppm_i, ppm_j)
                            ppm_j = ppm_jlo
                            ppm_jhi 
                    }
                }


        }

    */
    }


}
