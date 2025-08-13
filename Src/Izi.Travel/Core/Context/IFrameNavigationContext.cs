// ********************************************************************
// Type: Izi.Travel.Core.Context.IFrameNavigationContext
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml.Navigation;
#nullable disable
namespace Izi.Travel.Core.Context
{
  public interface IFrameNavigationContext
  {
    Uri Uri { get; }

    object Content { get; }

    NavigationMode NavigationMode { get; }
  }
}

