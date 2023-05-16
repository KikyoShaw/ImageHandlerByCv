using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ImageHandlerByCv.ViewModel;
using OpenCvSharp;
using Window = System.Windows.Window;

namespace ImageHandlerByCv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ImageFilePath { get; set; } = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnChange0_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.GetImageSource(ImageFilePath);
        }

        private void BtnChange1_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.SwapBlueRedColor(ImageFilePath);
        }

        private void BtnChange2_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.AshSettingImage(ImageFilePath);
        }

        private void BtnChange3_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.TwoShrinkImage(ImageFilePath);
        }

        private void BtnChange4_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.CorrosionImage(ImageFilePath);
        }

        private void BtnChange5_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.ExpansionImage(ImageFilePath);
        }

        private void BtnChange6_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.ReversalImage(ImageFilePath);
        }

        private void BtnChange7_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.TransformVertexImage(ImageFilePath);
        }

        private void BtnChange8_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.BlurImage(ImageFilePath);
        }

        private void BtnChange9_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.MarginalizationImage(ImageFilePath);
        }

        private void BtnChange10_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.BrightnessImage(ImageFilePath);
        }

        private void BtnChange11_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.GaussianBlurImage(ImageFilePath);
        }

        private void BtnChange12_Click(object sender, RoutedEventArgs e)
        {
            OutPutImage.Source = ImageCvHandler.Instance.BeautyImage(ImageFilePath);
        }

        private void BtnChange13_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                //该值确定是否可以选择多个文件,当前不然多选
                dialog.Multiselect = true;
                dialog.Title = "请选择文件夹";
                dialog.Filter = "所有文件(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ImageFilePath = dialog.FileName;
                    OutPutImage.Source = ImageCvHandler.Instance.GetImageSource(ImageFilePath);
                    TargetImage.Source = ImageCvHandler.Instance.GetImageSource(ImageFilePath);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }
        }
    }
}
