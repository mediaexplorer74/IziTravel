// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IParent
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.Collections;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   Interface used to define an object associated to a collection of children.
  /// </summary>
  public interface IParent
  {
    /// <summary>Gets the children.</summary>
    /// <returns>The collection of children.</returns>
    IEnumerable GetChildren();
  }
}
