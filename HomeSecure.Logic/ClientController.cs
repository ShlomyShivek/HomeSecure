using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data;
using HomeSecure.Data.Entities;
using HomeSecure.Client.Logic.Notifications;
using HomeSecure.Client.Logic.Notifications.Filters;
using HomeSecure.Log;

namespace HomeSecure.Client.Logic
{
    public class ClientController
    {
        private IHomeSecureConfig _config;
        private IClientView _view;
        private List<SecurityEventSubscriber> _notifications;
        private MotionDetector _motionDetector;
        private CameraDevice _cameraDevice;
        private AppSettings _appSettings;

        public ClientController(IClientView view, IHomeSecureConfig config)
        {
            _config = config;
            _view = view;
            _appSettings = _config.GetAppSettings();
        }

        public void ApplicationStarted()
        {
            InitConfiguration();

            InitNotifications();

            InitCameraDevice();
        }

        private void InitCameraDevice()
        {
            CameraDeviceFactory cameraDeviceFactory = new CameraDeviceFactory(_config);
            _cameraDevice = cameraDeviceFactory.GetFirstCameraDevice();
            if (_cameraDevice != null)
            {
                _view.AddCamera(_cameraDevice);
                _motionDetector = new CameraMotionDetector(_cameraDevice);
                _motionDetector.MotionDetected += new EventHandler<MotionDetectedEventArgs>(_motionDetector_MotionDetected);
            }
            else
            {
                _motionDetector = new NullMotionDetector();
                Logger.Debug("Could not detect camera device");
            }
        }

        private void InitConfiguration()
        {
            if ((_appSettings == null) || (_appSettings.IsDbInitialized == false))
            {
                DbInitialization.InitDbOnFirstRun(_config);
                _appSettings = new AppSettings();
                _appSettings.IsDbInitialized = true;
                _config.SetAppSettings(_appSettings);
                _config.Save();
            }
        }

        public void ApplicationClosing()
        {
            _motionDetector.StopDetection();
        }

        public void AddSecurityEventSubscriber(SecurityEventSubscriber subscriber)
        {
            _notifications.Add(subscriber);
        }

        public void StartMotionDetection()
        {
            _motionDetector.StartDetection();
        }

        public void StopMotionDetection()
        {
            _motionDetector.StopDetection();
        }

        private void InitNotifications()
        {
            _notifications = new List<SecurityEventSubscriber>();
            List<NotificationEntityData> notificationsData = _config.GetNotificationsData();
            foreach (NotificationEntityData notificationData in notificationsData)
            {
                SecurityEventSubscriber notification = Activator.CreateInstance(Type.GetType(notificationData.Type)) as SecurityEventSubscriber;
                notification.InitParams(notificationData.NotificationParams.ToDictionary(a => a.Key));

                TimeoutFilter timeoutFilter = new TimeoutFilter(notification);
                AddSecurityEventSubscriber(timeoutFilter);
            }

            SaveToDatabaseSecurityEventSubscriber saveToDbSubscriber = new SaveToDatabaseSecurityEventSubscriber(_config);
            TimeoutFilter saveToDbTimeoutFilter = new TimeoutFilter(saveToDbSubscriber);
            AddSecurityEventSubscriber(saveToDbTimeoutFilter);

            ServiceSecurityEventSubscriber serviceSubscriber = new ServiceSecurityEventSubscriber();
            TimeoutFilter serviceTimeoutFilter = new TimeoutFilter(serviceSubscriber);
            AddSecurityEventSubscriber(serviceTimeoutFilter);

        }


        private void _motionDetector_MotionDetected(object sender, MotionDetectedEventArgs e)
        {
            _view.OnMotionDetectionEvent(e.MotionDetectionEvent);
            foreach (SecurityEventSubscriber notification in _notifications)
            {
                notification.Notify(e.MotionDetectionEvent);
            }
        }

    }
}
