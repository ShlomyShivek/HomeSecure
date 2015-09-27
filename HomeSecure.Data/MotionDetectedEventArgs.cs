using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data.Entities;

namespace HomeSecure.Data
{
    public class MotionDetectedEventArgs : EventArgs
    {
        private MotionDetectionEvent _motionDetectionEvent;

        public MotionDetectedEventArgs(MotionDetectionEvent motionEvent)
        {
            MotionDetectionEvent = motionEvent;
        }

        public MotionDetectionEvent MotionDetectionEvent
        {
            get { return _motionDetectionEvent; }
            private set { _motionDetectionEvent = value; }
        }

    }
}
