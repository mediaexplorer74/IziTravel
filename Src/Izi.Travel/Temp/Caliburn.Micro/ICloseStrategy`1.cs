// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ICloseStrategy`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Used to gather the results from multiple child elements which may or may not prevent closing.
  /// </summary>
  /// <typeparam name="T">The type of child element.</typeparam>
  public interface ICloseStrategy<T>
  {
    /// <summary>Executes the strategy.</summary>
    /// <param name="toClose">Items that are requesting close.</param>
    /// <param name="callback">The action to call when all enumeration is complete and the close results are aggregated.
    /// The bool indicates whether close can occur. The enumerable indicates which children should close if the parent cannot.</param>
    void Execute(IEnumerable<T> toClose, Action<bool, IEnumerable<T>> callback);
  }
}
