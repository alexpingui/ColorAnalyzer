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
    public MainWindow()
    {
        InitializeComponent();
    }

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
            rawImage.Source = new BitmapImage(new Uri(rawImgPath)); 
        }

    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if(rawImgPath != string.Empty)
            rawImage.Source = ConvertBitmapToImageSource(ContoursMaker.ErodeImg(rawImgPath, (int)KernelSlider.Value));
    }

    public ImageSource ConvertBitmapToImageSource(Bitmap bitmap)
    {
        using (MemoryStream memory = new MemoryStream())
        {
            bitmap.Save(memory, ImageFormat.Png); // можно JPEG, BMP и т.д.
            memory.Position = 0;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = memory;
            bitmapImage.EndInit();
            bitmapImage.Freeze(); // чтобы использовать в разных потоках

            return bitmapImage;
        }
    }
}