// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Context.IFrameNavigationContext
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows.Navigation;

#nullable disable
namespace Izi.Travel.Shell.Core.Context
{
  public interface IFrameNavigationContext
  {
    Uri Uri { get; }

    object Content { get; }

    NavigationMode NavigationMode { get; }
  }
}
