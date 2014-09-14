using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Raysoft.Utility
{
    public class Utility
    {
        /// <summary>
        /// 判断当前网络是否可用
        /// </summary>
        public static bool IsNetworkAvalible
        {
            get
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
        }

        /// <summary>
        /// 颜色转换
        /// </summary>
        /// <param name="colorStr">colorStr</param>
        /// <returns>Color</returns>
        public static Color ConvertColorFromHex(string colorStr)
        {
            if (colorStr != null && (colorStr.Length == 7 || colorStr.Length == 9))
            {
                byte a = 255;
                int posi = 1;
                if (colorStr.Length == 9)
                {
                    a = Byte.Parse(colorStr.Substring(posi, 2), NumberStyles.HexNumber);
                    posi += 2;
                }

                byte r = Byte.Parse(colorStr.Substring(posi, 2), NumberStyles.HexNumber);
                posi += 2;
                byte g = Byte.Parse(colorStr.Substring(posi, 2), NumberStyles.HexNumber);
                posi += 2;
                byte b = Byte.Parse(colorStr.Substring(posi, 2), NumberStyles.HexNumber);

                return Color.FromArgb(a, r, g, b);
            }

            return Colors.Black;
        }
    }
}
