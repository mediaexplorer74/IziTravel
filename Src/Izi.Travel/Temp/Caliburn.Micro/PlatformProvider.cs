// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.PlatformProvider
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Access the current <see cref="T:Caliburn.Micro.IPlatformProvider" />.
  /// </summary>
  public static class PlatformProvider
  {
    private static IPlatformProvider current = (IPlatformProvider) new DefaultPlatformProvider();

    /// <summary>
    /// Gets or sets the current <see cref="T:Caliburn.Micro.IPlatformProvider" />.
    /// </summary>
    public static IPlatformProvider Current
    {
      get => PlatformProvider.current;
      set => PlatformProvider.current = value;
    }
  }
}
