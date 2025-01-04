// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Display.ScreenSizeInformation
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Display
{
  public class ScreenSizeInformation
  {
    private static readonly double ScreenWidth = Application.Current.Host.Content.ActualWidth;
    private static readonly double ScreenHeight = Application.Current.Host.Content.ActualHeight;

    public double Width => ScreenSizeInformation.ScreenWidth;

    public double WidthNegative => -ScreenSizeInformation.ScreenWidth;

    public double Height => ScreenSizeInformation.ScreenHeight;

    public double HeightNegative => -ScreenSizeInformation.ScreenHeight;
  }
}
