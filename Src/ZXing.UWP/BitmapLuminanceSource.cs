// ********************************************************************
// Type: ZXing.BitmapLuminanceSource
// Assembly: zxing.wp8.0, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DD293DF0-BBAA-4BF0-BAC7-F5FAF5AC94ED
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\zxing.wp8.0.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\zxing.wp8.0.xml

//using System.Windows.Media;
//using System.Windows.Media.Imaging;

#nullable disable
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace ZXing
{
  public class BitmapLuminanceSource : BaseLuminanceSource
  {
        //TEMP
        //private int[] Pixels;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ZXing.BitmapLuminanceSource" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        protected BitmapLuminanceSource(int width, int height)
           : base(width, height)
        {
        }

        public BitmapLuminanceSource(WriteableBitmap writeableBitmap)
           : base(writeableBitmap.PixelWidth, writeableBitmap.PixelHeight)
        {
            int pixelHeight = writeableBitmap.PixelHeight;
            int pixelWidth = writeableBitmap.PixelWidth;

            // Get pixel data from PixelBuffer
            IBuffer buffer = writeableBitmap.PixelBuffer;
            byte[] bytes = buffer.ToArray();

            // Each pixel is 4 bytes (BGRA)
            int numPixels = pixelWidth * pixelHeight;
            int index1 = 0;
            for (int i = 0; i < numPixels; ++i)
            {
                int offset = i * 4;
                byte b = bytes[offset + 0];
                byte g = bytes[offset + 1];
                byte r = bytes[offset + 2];
                byte a = bytes[offset + 3];
                // Use the same luminance calculation as before
                this.luminances[index1] = (byte)((19562 * r + 38550 * g + 7424 * b) >> 16);
                ++index1;
            }
        }

        /// <summary>
        /// Should create a new luminance source with the right class type.
        /// The method is used in methods crop and rotate.
        /// </summary>
        /// <param name="newLuminances">The new luminances.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        protected override LuminanceSource CreateLuminanceSource(
      byte[] newLuminances,
      int width,
      int height)
    {
      BitmapLuminanceSource luminanceSource = new BitmapLuminanceSource(width, height);
      luminanceSource.luminances = newLuminances;
      return (LuminanceSource) luminanceSource;
    }
  }
}
