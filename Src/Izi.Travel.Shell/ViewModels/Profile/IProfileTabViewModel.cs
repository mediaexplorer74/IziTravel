// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.IProfileTabViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Model.Profile;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile
{
  public interface IProfileTabViewModel : 
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged
  {
    ProfileType Type { get; }

    IEnumerable<ButtonInfo> AppBarButtons { get; }

    IEnumerable<MenuItemInfo> AppBarMenuItems { get; }
  }
}
