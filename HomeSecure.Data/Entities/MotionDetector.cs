using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeSecure.Data.Entities
{
    public abstract class MotionDetector
    {
        public event EventHandler<MotionDetectedEventArgs> MotionDetected;

        public abstract void StartDetection();

        public abstract void StopDetection();

        protected void OnMotionDetected(MotionDetectedEventArgs args)
        {
            var motionDetected = MotionDetected;
            if (motionDetected != null)
            {
                motionDetected(this, args);
            }
        }
    }
}
