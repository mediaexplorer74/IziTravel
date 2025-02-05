// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Helpers.ImageHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.IO;
using System.Windows.Media.Imaging;

#nullable disable
namespace Izi.Travel.Shell.Core.Helpers
{
  public class ImageHelper
  {
    public static BitmapImage ImageFromUri(Uri uri)
    {
      if (uri == (Uri) null || !uri.IsWellFormedOriginalString())
        return (BitmapImage) null;
      return new BitmapImage()
      {
        CreateOptions = BitmapCreateOptions.IgnoreImageCache,
        UriSource = uri
      };
    }

    public static BitmapImage ImageFromByteArray(byte[] imageData)
    {
      if (imageData == null || imageData.Length == 0)
        return (BitmapImage) null;
      BitmapImage bitmapImage = new BitmapImage();
      using (MemoryStream streamSource = new MemoryStream(imageData))
        bitmapImage.SetSource((Stream) streamSource);
      return bitmapImage;
    }
  }
}
