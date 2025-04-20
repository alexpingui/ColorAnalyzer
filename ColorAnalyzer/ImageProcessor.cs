using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Drawing;
using System.Drawing.Imaging;




namespace ColorAnalyzer
{
    public static class ImageProcessor
    {
        private static int _kernelSize = 1;
        public static int KernelSize 
        {
            set
            {
                if (value >= 1 && value <= 11 && value != _kernelSize) 
                _kernelSize = value;
                _kernel = CreateKernel(_kernelSize);
            }
        }

        private static Mat _kernel = CreateKernel(_kernelSize); 

        private static Mat CreateKernel(int kernelSize)
        {
            return CvInvoke.GetStructuringElement(
            ElementShape.Rectangle,
            new Size(2 * _kernelSize + 1, 2 * _kernelSize + 1),
            new Point(_kernelSize, _kernelSize) // Автоматически по центру
            );
        }

        public static void ErodeImg(ref Image<Rgb, byte> inputImage)
        {         
            CvInvoke.Erode(inputImage, inputImage, _kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
        }
        
        public static void DelateImg(ref Image<Rgb, byte> inputImage)
        {
            CvInvoke.Dilate(inputImage, inputImage, _kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
        }
    }
}
