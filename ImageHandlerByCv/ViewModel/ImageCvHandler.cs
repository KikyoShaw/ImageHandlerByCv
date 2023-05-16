using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ImageHandlerByCv.ViewModel
{
    public class ImageCvHandler
    {
        private static readonly Lazy<ImageCvHandler> LazeVm = new Lazy<ImageCvHandler>(() => new ImageCvHandler());
        internal static ImageCvHandler Instance => LazeVm.Value;

        public ImageCvHandler()
        {

        }

        private BitmapSource? GetBitmapImage(Mat mat)
        {
            try
            {
                var mem = mat.ToMemoryStream();
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = mem;
                bmp.EndInit();
                bmp.Freeze();
                return bmp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return null;
        }

        /// <summary>
        /// 图片还原成正常状态
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? GetImageSource(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.Unchanged);
                //return GetBitmapImage(mat);
                var bmp = mat.ToBitmap();
                return Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return null;
        }

        /// <summary>
        /// 红蓝颜色通道互换
        /// </summary>
        /// <param name="path"></param>
        public BitmapSource? SwapBlueRedColor(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.Unchanged);
                for (var y = 0; y < mat.Height; y++)
                {
                    for (var x = 0; x < mat.Width; x++)
                    {
                        Vec3b color = mat.Get<Vec3b>(y, x);
                        (color.Item0, color.Item2) = (color.Item2, color.Item0);
                        mat.Set(y, x, color);
                    }
                }
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 图片置灰
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? AshSettingImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.Grayscale);
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 2倍缩小读取图像
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? TwoShrinkImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.ReducedGrayscale2);
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 腐蚀图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? CorrosionImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                using var dst = new Mat();
                Cv2.Erode(mat, dst, new Mat());
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 膨胀图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? ExpansionImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                using var dst = new Mat();
                Cv2.Dilate(mat, dst, new Mat());
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 反转图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? ReversalImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                using var dst = new Mat();
                Cv2.BitwiseNot(mat, dst, new Mat());
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 变换顶点
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? TransformVertexImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                using var dst = new Mat();
                //设置原图变换顶点
                var affinePoints0 = new List<Point2f>() { new Point2f(100, 50), new Point2f(100, 390), new Point2f(600, 50) };
                //设置目标图像变换顶点
                var affinePoints1 = new List<Point2f>() { new Point2f(200, 100), new Point2f(200, 330), new Point2f(500, 50) };
                //计算变换矩阵
                Mat trans = Cv2.GetAffineTransform(affinePoints0, affinePoints1);
                //矩阵仿射变换
                Cv2.WarpAffine(mat, dst, trans, new OpenCvSharp.Size() { Height = mat.Cols, Width = mat.Rows });
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 模糊
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? BlurImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                using var dst = new Mat();
                Cv2.Blur(mat, dst, new OpenCvSharp.Size(7, 7));
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 边缘化
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? MarginalizationImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                using var dst = new Mat();
                Cv2.Canny(mat, dst, 10, 400, 3);
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 图片亮度
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? BrightnessImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                for (var y = 0; y < mat.Height; y++)
                {
                    for (var x = 0; x < mat.Width; x++)
                    {
                        Vec3b color = mat.Get<Vec3b>(y, x);
                        int item0 = color.Item0;
                        int item1 = color.Item1;
                        int item2 = color.Item2;
                        #region  变暗
                        item0 -= 60;
                        item1 -= 60;
                        item2 -= 60;
                        if (item0 < 0)
                            item0 = 0;
                        if (item1 < 0)
                            item1 = 0;
                        if (item2 < 0)
                            item2 = 0;
                        #endregion
                        #region  变亮
                        //item0 += 80;
                        //item1 += 80;
                        //item2 += 80;
                        //if (item0 > 255)
                        //    item0 = 255;
                        //if (item1 > 255)
                        //    item1 = 255;
                        //if (item2 > 255)
                        //    item2 = 255;
                        #endregion

                        color.Item0 = (byte)item0;
                        color.Item1 = (byte)item1;
                        color.Item2 = (byte)item2;
                        mat.Set(y, x, color);
                    }
                }
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 高斯模糊
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? GaussianBlurImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                using var dst = new Mat();
                Cv2.GaussianBlur(mat, dst, new OpenCvSharp.Size(5, 5), 1.5);
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 美颜磨皮 双边滤波 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapSource? BeautyImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;

                using var mat = new Mat(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
                using var dst = new Mat();
                Cv2.BilateralFilter(mat, dst, 15, 35d, 35d);
                return GetBitmapImage(mat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            return null;
        }
    }
}
