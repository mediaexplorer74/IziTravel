// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IClose
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Denotes an object that can be closed.</summary>
  public interface IClose
  {
    /// <summary>
    /// Tries to close this instance.
    /// Also provides an opportunity to pass a dialog result to it's corresponding view.
    /// </summary>
    /// <param name="dialogResult">The dialog result.</param>
    void TryClose(bool? dialogResult = null);
  }
}
