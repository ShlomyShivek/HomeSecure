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
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using HomeSecure.Data;
using System.Configuration;
using System.Net;
using HomeSecure.Data.Entities;
using HomeSecure.Client.Configuration;
using HomeSecure.Client.Logic;
using HomeSecure.Client.Logic.Notifications.Filters;
using HomeSecure.Data.Interfaces;

namespace HomeSecure.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClientView
    {
        private EventLog _eventLogForm;
        private HomeSecureConfig _config;
        private ClientController _controller;

        public MainWindow()
        {
            InitializeComponent();

            IList<IDataBaseExtender> dbExtenders = new List<IDataBaseExtender>();
            dbExtenders.Add(new LocalCameraDevice());
            _config = new HomeSecureConfig("HomeSecureData", dbExtenders);

            _controller = new ClientController(this, _config);
            _controller.ApplicationStarted();

            _eventLogForm = new EventLog();

            _config.GetSecurityEvents().ForEach(a => _eventLogForm.AddSecurityEvent(a));

            EventLogNotification eventLogNotification = new EventLogNotification(_eventLogForm);
            TimeoutFilter filter = new TimeoutFilter(eventLogNotification);
            _controller.AddSecurityEventSubscriber(filter);
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (CamViewer camViewer in CamViewers.Children)
            {
                camViewer.Stop();
            }
            
            _controller.ApplicationClosing();

            foreach (Window window in Application.Current.Windows)
            {
                if (window != this)
                {
                    window.Close();
                }
            }
        }

        private void butStartDetection_Click_1(object sender, RoutedEventArgs e)
        {
            _controller.StartMotionDetection();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _controller.StopMotionDetection();
        }

        private void butShowEventLog_Click(object sender, RoutedEventArgs e)
        {
            _eventLogForm.Show();
        }

        public void OnMotionDetectionEvent(MotionDetectionEvent motionDetectionEvent)
        {
            lblAlert.Dispatcher.BeginInvoke(new Action(() =>
            lblAlert.Content = "Alert " + motionDetectionEvent.NumberOfPixelsDetected));
        }

        public void AddCamera(CameraDevice camera)
        {
            if (camera != null)
            {
                CamViewer camViewer = new CamViewer();
                camViewer.Init(camera);
                CamViewers.Children.Add(camViewer);
            }
        }

        private void butSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings  settingsForm = new Settings(_config);
            settingsForm.Show();
        }
    }
}
