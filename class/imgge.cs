
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mytest.@class
{
    internal class imgge
    {
        /// <summary>
        /// 彩色图转灰度图
        /// </summary>
        /// <param name="original">原始图</param>
        /// <returns>灰度图</returns>
        public static Bitmap ConvertToGrayscale(Bitmap original)
        {

            // 创建一个与原始图像相同尺寸的灰度图像
            Bitmap grayScale = new Bitmap(original.Width, original.Height);

            // 锁定原始图像和灰度图像的内存区域以加快处理速度
            BitmapData originalData = original.LockBits(
                new Rectangle(0, 0, original.Width, original.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            BitmapData grayData = grayScale.LockBits(
                new Rectangle(0, 0, grayScale.Width, grayScale.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb); // 注意：这里仍然使用24bppRgb，但只使用最低8位

            // 获取原始图像和灰度图像的字节数组
            int originalStride = originalData.Stride;
            int grayStride = grayData.Stride;
            byte[] rgbValues = new byte[Math.Abs(originalStride) * original.Height];
            byte[] grayValues = new byte[Math.Abs(grayStride) * grayScale.Height];

            // 复制原始图像数据到字节数组
            System.Runtime.InteropServices.Marshal.Copy(originalData.Scan0, rgbValues, 0, rgbValues.Length);

            // 遍历原始图像的每个像素
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    // 计算灰度值（这里使用了一个简单的灰度计算公式：亮度 = (红*0.3 + 绿*0.59 + 蓝*0.11)）
                    int blue = rgbValues[y * originalStride + x * 3];
                    int green = rgbValues[y * originalStride + x * 3 + 1];
                    int red = rgbValues[y * originalStride + x * 3 + 2];

                    int gray = (int)(red * 0.3 + green * 0.59 + blue * 0.11);

                    // 设置灰度图像的对应像素值（只设置最低的8位，因为灰度图像只需要一个颜色通道）
                    grayValues[y * grayStride + x * 3] = (byte)gray; // Blue
                    grayValues[y * grayStride + x * 3 + 1] = (byte)gray; // Green
                    grayValues[y * grayStride + x * 3 + 2] = (byte)gray; // Red（所有颜色通道设置为相同的灰度值）
                }
            }

            // 复制灰度数据到灰度图像
            System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, grayData.Scan0, grayValues.Length);

            // 解锁图像的内存区域
            original.UnlockBits(originalData);
            grayScale.UnlockBits(grayData);

            return grayScale;
        }


    }
}
