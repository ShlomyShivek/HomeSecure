using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HomeSecure.Data.Entities
{
    [DataContract]
    public class CameraDevice : IInputDevice
    {
        public event EventHandler<CameraFrameEventArgs> CameraFrame;

        public CameraDevice()
        {
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ID { get; set; }

        public virtual void StartVideo() { }

        public virtual void StopVideo() { }

        protected void OnCameraFrame(CameraFrameEventArgs args)
        {
            var cameraFrame = CameraFrame;
            if (cameraFrame != null)
            {
                cameraFrame(this, args);
            }
        }
    }
}
