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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeText();
            //button2.Focus();
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // Connect the Paint event of the PictureBox to the event handler method.
        }

        /// <summary>
        /// 改变文本框里的数
        /// </summary>
        private void ChangeText()
        {
            textBoxXMin.Text = xmin.ToString();
            textBoxXMax.Text = xmax.ToString();
            textBoxYMin.Text = ymin.ToString();
            textBoxYMax.Text = ymax.ToString();
        }

        //显示图形
        private void buttonShow_Click(object sender, EventArgs e)
        {
            //此时x,y的最值与TextBox有关
            xmin = double.Parse(textBoxXMin.Text);
            xmax = double.Parse(textBoxXMax.Text);
            ymin = double.Parse(textBoxYMin.Text);
            ymax = double.Parse(textBoxYMax.Text);

            //调整比例
            ChangeXYMaxMin();

            pictureBox1.Refresh();
        }

        /// <summary>
        /// 调整xm, ym至相应比例
        /// </summary>
        private void ChangeXYMaxMin()
        {
            //选出合适的点列
            double dx = (xmax - xmin) / WWidth;
            double dy = (ymax - ymin) / WHeight;
            if (dx < dy) {
                dx = dy;
                double dxm = (WWidth * dx - (xmax - xmin)) / 2;
                xmin -= dxm;
                xmax += dxm;
            }
            if (dx > dy) {
                dy = dx;
                double dym = (WHeight * dy - (ymax - ymin)) / 2;
                ymin -= dym;
                ymax += dym;
            }
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
            pictureBox1.Refresh();
        }


        //左
        private void buttonLeft_Click(object sender, EventArgs e)
        {
            double deltax = xmax - xmin;
            xmin -= deltax / 4;
            xmax -= deltax / 4;
            pictureBox1.Refresh();
        }

        //上
        private void buttonUp_Click(object sender, EventArgs e)
        {
            double deltay = ymax - ymin;
            ymin -= deltay / 4;
            ymax -= deltay / 4;
            pictureBox1.Refresh();
        }

        //右
        private void buttonRight_Click(object sender, EventArgs e)
        {
            double deltax = xmax - xmin;
            xmin += deltax / 4;
            xmax += deltax / 4;
            pictureBox1.Refresh();
        }

        //下
        private void buttonDown_Click(object sender, EventArgs e)
        {
            double deltay = ymax - ymin;
            ymin += deltay / 4;
            ymax += deltay / 4;
            pictureBox1.Refresh();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {

        }
    }
}
