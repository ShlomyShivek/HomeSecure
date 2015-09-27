using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeSecure.Data;
using System.Drawing;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;
using HomeSecure.Data.Entities;

namespace HomeSecure.Client.Logic
{
    public class CameraMotionDetector : MotionDetector
    {
        private Bitmap _previousFrame;
        private Bitmap _currentFrame;

        private CameraDevice _camera;

        private bool _detecting;

        public CameraMotionDetector(CameraDevice camera)
        {
            _camera = camera;
            _detecting = false;
            _camera.CameraFrame += new EventHandler<CameraFrameEventArgs>(camera_CameraFrame);
        }

        public override void StartDetection()
        {
            _detecting = true;
        }

        public override void StopDetection()
        {
            _detecting = false;

            if (_previousFrame != null)
            {
                _previousFrame.Dispose();
                _previousFrame = null;
            }

            if (_currentFrame != null)
            {
                _currentFrame.Dispose();
                _currentFrame = null;
            }

        }

        private void camera_CameraFrame(object sender, CameraFrameEventArgs e)
        {
            if (_detecting)
            {
                if (_previousFrame != null)
                {
                    _previousFrame.Dispose();
                    _previousFrame = null;
                }

                _previousFrame = _currentFrame;
                _currentFrame = e.Frame.Clone() as Bitmap;
                DetectMotion();
                GC.Collect();
            }
        }

        private void DetectMotion()
        {
            if ((_previousFrame != null) && (_currentFrame != null))
            {
                Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
                using (Bitmap frame1GS = grayscaleFilter.Apply(_previousFrame))
                {
                    using (Bitmap frame2GS = grayscaleFilter.Apply(_currentFrame))
                    {
                        Difference differenceFilter = new Difference();
                        IFilter thresholdFilter = new Threshold(15);
                        // set backgroud frame as an overlay for difference filter

                        differenceFilter.OverlayImage = (Bitmap)frame1GS;
                        // apply the filters

                        using (Bitmap tmp1 = differenceFilter.Apply((Bitmap)frame2GS))
                        {
                            using (Bitmap tmp2 = thresholdFilter.Apply(tmp1))
                            {

                                IFilter erosionFilter = new Erosion();
                                // apply the filter 


                                using (Bitmap tmp3 = erosionFilter.Apply(tmp2))
                                {
                                    int whitePixelsCount = CalculateWhitePixels(tmp3);

                                    if (whitePixelsCount > 1000)
                                    {
                                        MotionDetectionEvent motionEvent = new MotionDetectionEvent()
                                        {
                                            CameraDevice = _camera,
                                            NumberOfPixelsDetected = whitePixelsCount,
                                            InputDevice = _camera
                                        };
                                        MotionDetectedEventArgs args = new MotionDetectedEventArgs(motionEvent);
                                        OnMotionDetected(args);
                                    }


                                    //// extract red channel from the original image

                                    //IFilter extrachChannel = new ExtractChannel(RGB.R);
                                    //Bitmap redChannel = extrachChannel.Apply(frame2);
                                    ////  merge red channel with motion regions

                                    //Merge mergeFilter = new Merge();
                                    //mergeFilter.OverlayImage = tmp3;
                                    //Bitmap tmp4 = mergeFilter.Apply(redChannel);
                                    //// replace red channel in the original image

                                    //ReplaceChannel replaceChannel = new ReplaceChannel(RGB.R, tmp4);
                                    //replaceChannel.ChannelImage = tmp4;
                                    //Bitmap tmp5 = replaceChannel.Apply(frame2);
                                }
                            }
                        }
                    }
                }
            }
        }

        private int CalculateWhitePixels(Bitmap image)
        {
            int count = 0;
            // lock difference image

            BitmapData data = image.LockBits(
                new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            int offset = data.Stride - image.Width;
            unsafe
            {
                byte* ptr = (byte*)data.Scan0.ToPointer();
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++, ptr++)
                    {
                        count += ((*ptr) >> 7);
                    }
                    ptr += offset;
                }
            }
            // unlock image

            image.UnlockBits(data);
            return count;
        }

    }
}
