using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MathPaint
{
    static class Palette
    {
        /// <summary>
        /// 返回色相不同的可循环的颜色
        /// </summary>
        public static Color HueToColor(double hue)
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
        /// 返回色相不同的可循环的颜色
        /// </summary>
        public static Color HueToCyclicColor(double hue)
        {
            if (hue <= 0.5)
                return HueToColor(hue * 2);
            else
                return HueToColor(2 - hue * 2);
        }

        /// <summary>
        /// 返回亮度不同的颜色
        /// </summary>
        public static Color LightnessToCyclicColor(double light)
        {
            return Color.White;
        }

        /// <summary>
        /// 返回色相、亮度不同的可循环的颜色
        /// </summary>
        public static Color HLToCyclicColor(double value)
        {
            return Color.White;
        }

        /// <summary>
        /// 返回不同的灰色
        /// </summary>
        public static Color LightnessToGray(double value)
        {
            int rgb = (int)(value * 255);
            return Color.FromArgb(rgb, rgb, rgb);
        }

    }
}
