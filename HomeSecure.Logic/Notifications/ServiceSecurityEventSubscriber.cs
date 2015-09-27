using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data.Entities;
using HomeSecure.Data.Interfaces;
using System.ServiceModel;
using HomeSecure.Log;

namespace HomeSecure.Client.Logic.Notifications
{
    public class ServiceSecurityEventSubscriber : SecurityEventSubscriber
    {
        private IHomeSecureServerInputs _service;

        public ServiceSecurityEventSubscriber()
        {
            ChannelFactory<IHomeSecureServerInputs> channelFactory = new ChannelFactory<IHomeSecureServerInputs>("HomeSecureInputs");
            _service = channelFactory.CreateChannel();
        }

        public override void InitParams(Dictionary<string, NotificationEntityParams> parameters)
        {
        }

        public override void Notify(SecurityEvent securityEvent)
        {
            try
            {
                MotionDetectionEvent motionDetectionEvent = new MotionDetectionEvent()
                {
                    CameraDevice = new CameraDevice()
                    {
                        ID = securityEvent.InputDevice.ID,
                        Name = (securityEvent.InputDevice as CameraDevice).Name,
                    },
                    ID = securityEvent.ID,
                    NumberOfPixelsDetected = (securityEvent as MotionDetectionEvent).NumberOfPixelsDetected,
                    SecurityEventTime = securityEvent.SecurityEventTime
                };
                _service.AddMotionSecurityEvent(motionDetectionEvent);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to notify security event to server");
            }
        }
    }
}
