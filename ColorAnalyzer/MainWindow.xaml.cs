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

namespace ColorAnalyzer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
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
            rawImage.Source = new BitmapImage(new Uri(fileDialog.FileName));
            ContoursMaker.MakeContours(fileDialog.FileName);
        }

    }
}