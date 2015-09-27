using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video.DirectShow;
using HomeSecure.Data;
using HomeSecure.Data.Entities;

namespace HomeSecure.Client.Logic
{
    public class CameraDeviceFactory
    {
        private IHomeSecureConfig _config;

        public CameraDeviceFactory(IHomeSecureConfig config)
        {
            _config = config;
        }

        public static CameraDevice DetectCameraDeviceHardware()
        {
            LocalCameraDevice cameraDevice = null;
            // enumerate video devices
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if ((videoDevices != null) && (videoDevices.Count > 0))
            {
                // create video source
                VideoCaptureDevice videoDevice = new VideoCaptureDevice(videoDevices[0].MonikerString);
                cameraDevice = new LocalCameraDevice();
                cameraDevice.Init(videoDevice);
            }
            return cameraDevice;
        }

        public CameraDevice GetFirstCameraDevice()
        {
            CameraDevice cameraDevice = null;
            // enumerate video devices
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if ((videoDevices != null) && (videoDevices.Count > 0))
            {
                // create video source
                VideoCaptureDevice videoDevice = new VideoCaptureDevice(videoDevices[0].MonikerString);
                List<CameraDevice> devicesData = _config.GetConfiguredCameraDevicesData().Where(a => a.ID == videoDevice.Source).ToList();
                if ((devicesData != null) && (devicesData.Count > 0))
                {
                    cameraDevice = devicesData[0];
                }
                else
                {
                    cameraDevice = new LocalCameraDevice();
                    cameraDevice.ID = videoDevice.Source;
                    cameraDevice.Name = "Camera";
                    _config.AddCameraDevice(cameraDevice);
                    _config.Save();
                }
                (cameraDevice as LocalCameraDevice).Init(videoDevice);

            }

            return cameraDevice;
        }
    }
}
