﻿// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IChild`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Denotes a node within a parent/child hierarchy.</summary>
  /// <typeparam name="TParent">The type of parent.</typeparam>
  public interface IChild<TParent> : IChild
  {
    /// <summary>Gets or Sets the Parent</summary>
    TParent Parent { get; set; }
  }
}
