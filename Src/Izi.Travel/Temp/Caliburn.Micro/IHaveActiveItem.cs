// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IHaveActiveItem
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Denotes an instance which maintains an active item.</summary>
  public interface IHaveActiveItem
  {
    /// <summary>The currently active item.</summary>
    object ActiveItem { get; set; }
  }
}
