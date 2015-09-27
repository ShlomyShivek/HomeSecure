using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Drawing;
using HomeSecure.Data;
using HomeSecure.Data.Entities;
using HomeSecure.Data.Interfaces;
using System.Data.Entity;

namespace HomeSecure.Client.Logic
{
    public class LocalCameraDevice : CameraDevice, IDataBaseExtender
    {
        private VideoCaptureDevice _videoCaptureDevice;

        public LocalCameraDevice()
        {
        }

        public void Init(VideoCaptureDevice videoCaptureDevice)
        {
            _videoCaptureDevice = videoCaptureDevice;
            _videoCaptureDevice.NewFrame += new NewFrameEventHandler(_videoCaptureDevice_NewFrame);
            ID = _videoCaptureDevice.Source;
        }

        public void SetCaptureDevice(VideoCaptureDevice videoCaptureDevice)
        {
            _videoCaptureDevice = videoCaptureDevice;
        }

        public override void StartVideo()
        {
            _videoCaptureDevice.Start();
        }

        public override void StopVideo()
        {
            if (_videoCaptureDevice != null)
            {
                // signal to stop
                _videoCaptureDevice.SignalToStop();
                _videoCaptureDevice.WaitForStop();
            }
        }

        private void _videoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            CameraFrameEventArgs args = new CameraFrameEventArgs(bitmap);
            OnCameraFrame(args);
            bitmap.Dispose();
        }

        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalCameraDevice>();
        }
    }
}
