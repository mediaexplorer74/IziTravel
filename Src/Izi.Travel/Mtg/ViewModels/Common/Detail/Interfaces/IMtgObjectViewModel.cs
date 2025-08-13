// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.Detail.Interfaces.IMtgObjectViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Common.Model;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.Detail.Interfaces
{
  public interface IMtgObjectViewModel : IMtgObjectProvider
  {
    IEnumerable<ButtonInfo> AvailableAppBarButtons { get; }

    IEnumerable<MenuItemInfo> AvailableAppBarMenuItems { get; }
  }
}
