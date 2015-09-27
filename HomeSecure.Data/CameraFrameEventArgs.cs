using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HomeSecure.Data
{
    public class CameraFrameEventArgs : EventArgs
    {
        private Bitmap _frame;

        public CameraFrameEventArgs(Bitmap frame)
        {
            Frame = frame;
        }

        public Bitmap Frame
        {
            get { return _frame; }
            private set { _frame = value; }
        }
    }
}
