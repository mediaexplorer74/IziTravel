// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IGuardClose
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Denotes an instance which may prevent closing.</summary>
  public interface IGuardClose : IClose
  {
    /// <summary>Called to check whether or not this instance can close.</summary>
    /// <param name="callback">The implementer calls this action with the result of the close check.</param>
    void CanClose(Action<bool> callback);
  }
}
