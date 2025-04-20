using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV.Features2D;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Microsoft.Win32;
using System.Drawing.Imaging;

namespace ColorAnalyzer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    string rawImgPath = "";
    Image<Rgb, byte>? image = null;

    delegate void ChangeImage(ref Image<Rgb, byte> image);
    ChangeImage changeImage;
    public MainWindow()
    {
        InitializeComponent();
    }

    //delegate void ChangeImage(ref Image<Rgb, byte>)


    private void UploadImage_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog fileDialog = new OpenFileDialog
        {
            Filter = "Изображения (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif",
            Title = "Выберите изображение"
        };
        if(fileDialog.ShowDialog() == true)
        {
            rawImgPath = fileDialog.FileName;
            image = new Image<Rgb, byte>(rawImgPath);
            rawImage.Source = ConvertGenericImageToImageSource(image); 
        }
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (image != null)
        {
            ImageProcessor.KernelSize = (int)KernelSlider.Value;
            changeImage(ref image);
            rawImage.Source = ConvertGenericImageToImageSource(image);
        }
    }

    public ImageSource ConvertGenericImageToImageSource(Image<Rgb, byte> image)
    {
        Bitmap bmpImage = image.ToBitmap();

        using (MemoryStream memory = new MemoryStream())
        {
            bmpImage.Save(memory, ImageFormat.Png); 
            memory.Position = 0;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = memory;
            bitmapImage.EndInit();
            bitmapImage.Freeze(); 

            return bitmapImage;
        }
    }

    private void ErodeImg_Checked(object sender, RoutedEventArgs e)
    {
        changeImage = ImageProcessor.ErodeImg;
    }

    private void DilateImg_Checked(object sender, RoutedEventArgs e)
    {
        changeImage = ImageProcessor.DelateImg;
    }

    private void ResetImageBtn_Click(object sender, RoutedEventArgs e)
    {
        image = new Image<Rgb, byte>(rawImgPath);
        rawImage.Source = ConvertGenericImageToImageSource(image);
    }
}