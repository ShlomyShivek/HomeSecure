using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data.Entities;
using HomeSecure.Data;
using HomeSecure.Data.Interfaces;


namespace HomeSecure.Client.Configuration
{
    public class HomeSecureConfig : IHomeSecureConfig
    {
        private ClientDataAccess _dataContext;

        public HomeSecureConfig(string connectionString, IList<IDataBaseExtender> dbExtenders)
        {
            _dataContext = new ClientDataAccess(connectionString, dbExtenders);
        }

        public List<CameraDevice> GetConfiguredCameraDevicesData()
        {
            List<CameraDevice> result = _dataContext.CameraDevices.ToList();
            if (result == null)
            {
                result = new List<CameraDevice>();
            }
            return result;
        }

        public void AddCameraDevice(CameraDevice cameraDevice)
        {
            _dataContext.CameraDevices.Add(cameraDevice);
        }

        public List<NotificationEntityData> GetNotificationsData()
        {
            List<NotificationEntityData> result = _dataContext.NotificationEntities.ToList();
            if (result == null)
            {
                result = new List<NotificationEntityData>();
            }
            return result;
        }

        public void DeleteAllNotificationEntityParams()
        {
            foreach(NotificationEntityParams prm in _dataContext.NotificationEntityParameters)
            {
                _dataContext.NotificationEntityParameters.Remove(prm);
            }
        }

        public void DelteAllNotificationEntityData()
        {
            foreach (NotificationEntityData notificationEntityData in _dataContext.NotificationEntities)
            {
                _dataContext.NotificationEntities.Remove(notificationEntityData);
            }
        }

        public void AddNotificationEntityData(NotificationEntityData notificationData)
        {
            _dataContext.NotificationEntities.Add(notificationData);
        }

        public void AddSecurityEvent(SecurityEvent securityEvent)
        {
            if (securityEvent is MotionDetectionEvent)
            {
                _dataContext.MotionDetectionEvents.Add(securityEvent as MotionDetectionEvent);
            }
        }

        public List<MotionDetectionEvent> GetSecurityEvents()
        {
            return _dataContext.MotionDetectionEvents.ToList();
        }

        public AppSettings GetAppSettings()
        {
            return _dataContext.AppSettings.FirstOrDefault();
        }

        public void SetAppSettings(AppSettings appSettings)
        {
            AppSettings settingsFromDb = GetAppSettings();
            while (settingsFromDb != null)
            {
                _dataContext.AppSettings.Remove(settingsFromDb);
                settingsFromDb = GetAppSettings();
            }
            _dataContext.AppSettings.Add(appSettings);
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}
