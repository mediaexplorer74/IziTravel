﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces.IMtgObjectViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Common.Model;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces
{
  public interface IMtgObjectViewModel : IMtgObjectProvider
  {
    IEnumerable<ButtonInfo> AvailableAppBarButtons { get; }

    IEnumerable<MenuItemInfo> AvailableAppBarMenuItems { get; }
  }
}
