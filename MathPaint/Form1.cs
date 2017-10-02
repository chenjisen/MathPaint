using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace MathPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetNewImage();
            //button2.Focus();
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // Connect the Paint event of the PictureBox to the event handler method.
        }

        /// <summary>
        /// 窗口大小：644*644（1：1）
        /// </summary>
        public const int WWidth = 644, WHeight = 644;  //之前的窗口大小：1120*630
        static double xmin = -2.1, xmax = 0.7, ymin = -1.4, ymax = 1.4;
        static double xcenter, ycenter;//xcenter = -0.7, ycenter = 0;
        MandelbrotImage MImage;


        private void GetNewImage()
        {
            textBoxXMin.Text = xmin.ToString();
            textBoxXMax.Text = xmax.ToString();
            textBoxYMin.Text = ymin.ToString();
            textBoxYMax.Text = ymax.ToString();
            Cursor = Cursors.WaitCursor;//改变光标形状
            MImage = new MandelbrotImage(xmin, xmax, ymin, ymax);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 绘制函数
        /// </summary>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //绘制图形
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
            double xmin = MImage.xmin, xmax = MImage.xmax;
            double ymin = MImage.ymin,ymax = MImage.ymax;
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


        //显示图形
        private void buttonShow_Click(object sender, EventArgs e)
        {
            //此时x,y的最值与TextBox有关
            xmin = double.Parse(textBoxXMin.Text);
            xmax = double.Parse(textBoxXMax.Text);
            ymin = double.Parse(textBoxYMin.Text);
            ymax = double.Parse(textBoxYMax.Text);

            //调整xm, ym至相应比例
            //选出合适的点列
            double dx = (xmax - xmin) / WWidth;
            double dy = (ymax - ymin) / WHeight;
            if (dx < dy) {
                dx = dy;
                double dxm = (WWidth * dx - (xmax - xmin)) / 2;
                xmin -= dxm;
                xmax += dxm;
            } else {
                dy = dx;
                double dym = (WHeight * dy - (ymax - ymin)) / 2;
                ymin -= dym;
                ymax += dym;
            }
            GetNewImage();
            pictureBox1.Refresh();
        }

        //显示鼠标位置
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            double deltax = xmax - xmin;
            double deltay = ymax - ymin;
            xcenter = xmin + e.X / (double)WWidth * deltax;
            ycenter = ymin + e.Y / (double)WHeight * deltay;
            textBoxXCursor.Text = xcenter.ToString();
            textBoxYCursor.Text = ycenter.ToString();
        }

        //点击任意位置放大
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            double deltax = xmax - xmin;
            double deltay = ymax - ymin;
            xmin = xcenter - deltax / 4;
            xmax = xcenter + deltax / 4;
            ymin = ycenter - deltay / 4;
            ymax = ycenter + deltay / 4;
            GetNewImage();
            pictureBox1.Refresh();
        }

        //放大
        private void buttonPlus_Click(object sender, EventArgs e)
        {
            double deltax = xmax - xmin;
            double deltay = ymax - ymin;
            xmin += deltax / 4;
            xmax -= deltax / 4;
            ymin += deltay / 4;
            ymax -= deltay / 4;
            GetNewImage();
            pictureBox1.Refresh();
        }

        //缩小
        private void buttonMinus_Click(object sender, EventArgs e)
        {
            double deltax = xmax - xmin;
            double deltay = ymax - ymin;
            xmin -= deltax / 2;
            xmax += deltax / 2;
            ymin -= deltay / 2;
            ymax += deltay / 2;
            GetNewImage();
            pictureBox1.Refresh();
        }


        //左
        private void buttonLeft_Click(object sender, EventArgs e)
        {
            double deltax = xmax - xmin;
            xmin -= deltax / 4;
            xmax -= deltax / 4;
            GetNewImage();
            pictureBox1.Refresh();
        }

        //上
        private void buttonUp_Click(object sender, EventArgs e)
        {
            double deltay = ymax - ymin;
            ymin -= deltay / 4;
            ymax -= deltay / 4;
            GetNewImage();
            pictureBox1.Refresh();
        }

        //右
        private void buttonRight_Click(object sender, EventArgs e)
        {
            double deltax = xmax - xmin;
            xmin += deltax / 4;
            xmax += deltax / 4;
            GetNewImage();
            pictureBox1.Refresh();
        }

        //下
        private void buttonDown_Click(object sender, EventArgs e)
        {
            double deltay = ymax - ymin;
            ymin += deltay / 4;
            ymax += deltay / 4;
            GetNewImage();
            pictureBox1.Refresh();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {

        }
    }
}
