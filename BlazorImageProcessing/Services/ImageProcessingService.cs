using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System.IO;

namespace BlazorImageProcessing.Services
{
    public class ImageProcessingService
    {
        public byte[] ProcessImage(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
                using (var bitmap = new Bitmap(ms))
                {
                    var mat = Emgu.CV.BitmapExtension.ToMat(bitmap);

                    var grayMat = new Mat();
                    CvInvoke.CvtColor(mat, grayMat, ColorConversion.Bgr2Gray);

                    Bitmap processedBitmap = Emgu.CV.BitmapExtension.ToBitmap(grayMat);

                    using (var resultStream = new MemoryStream())
                    {
                        processedBitmap.Save(resultStream, System.Drawing.Imaging.ImageFormat.Png);
                        return resultStream.ToArray();
                    }
                }
            }
        }
    }
}
