using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Drawing;




namespace ColorAnalyzer
{
    public static class ContoursMaker
    {
        public static void MakeContours(string imgPath, int kernelSize)
        {
        }
        public static Bitmap ErodeImg(string imgPath, int kernelSize)
        {           
            Image<Rgb, byte> inputImg = new Image<Rgb,byte>(imgPath);
            Image<Rgb, byte> outputErodeImg = new Image<Rgb, byte>(inputImg.Width, inputImg.Height);

            Mat kernel = CvInvoke.GetStructuringElement(
            ElementShape.Rectangle,
            new Size(2 * kernelSize + 1, 2 * kernelSize + 1),
            new Point(kernelSize, kernelSize) // Автоматически по центру
            );

            CvInvoke.Erode(inputImg, outputErodeImg, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

            return outputErodeImg.ToBitmap();
        }
    }
}
