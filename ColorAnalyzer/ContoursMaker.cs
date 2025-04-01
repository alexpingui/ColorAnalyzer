using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Windows.Media.Imaging;
using Emgu.CV.CvEnum;
using System.Drawing;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using System.Drawing.Imaging;
using System.Windows;



namespace ColorAnalyzer
{
    public static class ContoursMaker
    {
        public static void MakeContours(string imgPath)
        {
            var image = new Image<Bgr, byte>(imgPath);
            var grayImage = image.Convert<Gray, byte>();
            grayImage.Save("Gray.jpg");
            Image<Gray, byte> blurredImage = grayImage.SmoothGaussian(55);
            blurredImage.Save("blurred.jpg");
            // Применяем алгоритм Canny
            Image<Gray, byte> cannyEdges = grayImage.Canny(150, 300);
            cannyEdges.Save("cannyEdges.jpg");
            // Сохраняем результат
            /*var contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(cannyEdges, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxSimple);

            // 6. Создаём копию оригинального изображения для вывода
            Image<Bgr, byte> output = image.Clone();

            // 7. Проходим по контурам и рисуем их
            for (int i = 0; i < contours.Size; i++)
            {
                // Получаем текущий контур
                var contour = contours[i];

                // Вычисляем центр контура (для номера)
                Moments moments = CvInvoke.Moments(contour);
                int x = (int)(moments.M10 / moments.M00);
                int y = (int)(moments.M01 / moments.M00);

                // Рисуем контур на изображении
                CvInvoke.DrawContours(output, contours, i, new MCvScalar(0, 255, 0), 2);

                // Добавляем номер контура
                CvInvoke.PutText(output, (i + 1).ToString(), new System.Drawing.Point(x, y),
                                 FontFace.HersheySimplex, 0.6, new MCvScalar(255, 0, 0), 2);
            }
            output.Save("rezult.jpg");*/
        }

        public static BitmapSource ToBitmapSource(Image<Bgr, byte> image)
        {
            using (MemoryStream stream = new MemoryStream())
            {

                Bitmap bitmap = image.ToBitmap();

                // Сохраняем Bitmap в поток
                bitmap.Save(stream, ImageFormat.Bmp);  // Используем ImageFormat.Bmp

                stream.Seek(0, SeekOrigin.Begin);

                // Создаем BitmapImage из потока
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}
