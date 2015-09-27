using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeSecure.Data.Entities
{
    public class NullMotionDetector : MotionDetector
    {
        public override void StartDetection()
        {
        }

        public override void StopDetection()
        {
        }
    }
}
