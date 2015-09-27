using System;
using System.Collections.Generic;
using System.Linq;
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
using HomeSecure.Data;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using HomeSecure.Data.Entities;

namespace HomeSecure.Client
{
    /// <summary>
    /// Interaction logic for CamViewer.xaml
    /// </summary>
    public partial class CamViewer : UserControl
    {
        private CameraDevice _cameraDevice;
        private bool _cameraConnected;

        public CamViewer()
        {
            InitializeComponent();
            _cameraConnected = false;
        }

        public void Init(CameraDevice camera)
        {
            _cameraDevice = camera;
            _cameraDevice.CameraFrame += new EventHandler<CameraFrameEventArgs>(_cameraDevice_CameraFrame);
        }

        public void Start()
        {
            _cameraConnected = true;
            _cameraDevice.StartVideo();
        }

        public void Stop()
        {
            _cameraConnected = false;
            _cameraDevice.StopVideo();
        }

        private void _cameraDevice_CameraFrame(object sender, CameraFrameEventArgs e)
        {
            if (_cameraConnected)
            {
                ReplaceImage(e.Frame.Clone() as Bitmap);
            }
        }

        private void ReplaceImage(Bitmap bitmap)
        {
            if (VideoImage.Dispatcher.Thread == Thread.CurrentThread)
            {
                VideoImage.Source = GetBitmapSource(bitmap);
            }
            else
            {
                VideoImage.Dispatcher.BeginInvoke(new Action(() => ReplaceImage(bitmap)));
            }
        }

        private static BitmapSource GetBitmapSource(Bitmap bitmap)
        {
            BitmapSource bitmapSource = null;
            MemoryStream bitmapStream = new MemoryStream();
            bitmap.Save(bitmapStream, ImageFormat.Bmp);
            bitmapSource = BitmapFrame.Create(bitmapStream);
            return bitmapSource;
        }

        private void StartCamera_Click(object sender, RoutedEventArgs e)
        {
            var uriSource = new Uri(@"/HomeSecure.Client;component/Images/Connecting_to_camera.GIF", UriKind.Relative);
            VideoImage.Source = new BitmapImage(uriSource);

            Start();
        }

        private void StopCamera_Click(object sender, RoutedEventArgs e)
        {
            Stop();

            var uriSource = new Uri(@"/HomeSecure.Client;component/Images/no_image.gif", UriKind.Relative);
            VideoImage.Source = new BitmapImage(uriSource);
        }

    }
}
