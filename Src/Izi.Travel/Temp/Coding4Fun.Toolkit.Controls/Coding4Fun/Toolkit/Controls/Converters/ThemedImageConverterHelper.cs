// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.ThemedImageConverterHelper
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Converters
{
  public static class ThemedImageConverterHelper
  {
    private static readonly Dictionary<string, BitmapImage> ImageCache = new Dictionary<string, BitmapImage>();

    public static BitmapImage GetImage(string path, bool negateResult = false)
    {
      if (string.IsNullOrEmpty(path))
        return (BitmapImage) null;
      bool flag = Application.Current.Resources.Contains((object) "PhoneDarkThemeVisibility") && (Visibility) Application.Current.Resources[(object) "PhoneDarkThemeVisibility"] == Visibility.Visible;
      if (negateResult)
        flag = !flag;
      path = string.Format(path, flag ? (object) "dark" : (object) "light");
      BitmapImage image;
      if (!ThemedImageConverterHelper.ImageCache.TryGetValue(path, out image))
      {
        image = new BitmapImage(new Uri(path, UriKind.Relative));
        ThemedImageConverterHelper.ImageCache.Add(path, image);
      }
      return image;
    }
  }
}
