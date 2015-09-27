using System;
using HomeSecure.Data.Entities;
using System.Collections.Generic;
namespace HomeSecure.Data
{
    public interface IHomeSecureConfig
    {
        void AddCameraDevice(CameraDevice cameraDevice);
        
        void AddNotificationEntityData(NotificationEntityData notificationData);
        
        void DeleteAllNotificationEntityParams();
        
        void DelteAllNotificationEntityData();
        
        List<CameraDevice> GetConfiguredCameraDevicesData();
        
        List<NotificationEntityData> GetNotificationsData();

        void AddSecurityEvent(SecurityEvent securityEvent);

        AppSettings GetAppSettings();

        void SetAppSettings(AppSettings appSettings);

        void Save();
    }
}
