using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;


namespace MathPaint
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 窗口大小：644*644（1：1）
        /// </summary>
        public const int WWidth = 644, WHeight = 644;  //之前的窗口大小：1120*630
        static double xmin = -2.1, xmax = 0.7, ymin = -1.4, ymax = 1.4, xcenter = -0.7, ycenter = 0;

        /// <summary>
        /// 绘制函数
        /// </summary>
        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            ChangeText();

            // 绘制图形
            MandelbrotImage MImage = new MandelbrotImage(xmin, xmax, ymin, ymax);
            e.Graphics.DrawImage(MImage.bmp, Point.Empty);

            //绘制坐标
            DrawCoordinates(e.Graphics);
        }

        /// <summary>
        /// 绘制坐标
        /// </summary>
        private void DrawCoordinates(Graphics g)
        {
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            double x, y;
            string str;

            double dx = (xmax - xmin) / WWidth;
            double dy = (ymax - ymin) / WHeight;
            for (int px = 0; px <= WWidth; px += 100) {
                x = xmin + dx * px;
                str = x.ToString();
                if (str.Length > 5) str = str.Substring(0, 5);
                g.DrawString(str, drawFont, drawBrush, px, 0);
            }

            for (int py = 0; py <= WHeight; py += 100) {
                y = ymin + dy * py;
                str = y.ToString();
                if (str.Length > 5) str = str.Substring(0, 5);
                g.DrawString(str, drawFont, drawBrush, 0, py);
            }

        }

    }
}