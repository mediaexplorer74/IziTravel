// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IParent`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.Collections.Generic;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Interface used to define a specialized parent.</summary>
  /// <typeparam name="T">The type of children.</typeparam>
  public interface IParent<out T> : IParent
  {
    /// <summary>Gets the children.</summary>
    /// <returns>The collection of children.</returns>
    IEnumerable<T> GetChildren();
  }
}
