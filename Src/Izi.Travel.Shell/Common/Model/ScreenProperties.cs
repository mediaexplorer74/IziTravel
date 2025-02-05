// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.Model.ScreenProperties
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Common.Model
{
  public class ScreenProperties : PropertyChangedBase
  {
    private ApplicationBarMode _appBarMode;
    private IEnumerable<ButtonInfo> _appBarButtons;
    private IEnumerable<MenuItemInfo> _appBarMenuItems;

    public ApplicationBarMode AppBarMode
    {
      get => this._appBarMode;
      set
      {
        if (this._appBarMode == value)
          return;
        this._appBarMode = value;
        this.NotifyOfPropertyChange<ApplicationBarMode>((Expression<Func<ApplicationBarMode>>) (() => this.AppBarMode));
      }
    }

    public IEnumerable<ButtonInfo> AppBarButtons
    {
      get => this._appBarButtons;
      set
      {
        if (object.Equals((object) this._appBarButtons, (object) value))
          return;
        this._appBarButtons = value;
        this.NotifyOfPropertyChange<IEnumerable<ButtonInfo>>((Expression<Func<IEnumerable<ButtonInfo>>>) (() => this.AppBarButtons));
      }
    }

    public IEnumerable<MenuItemInfo> AppBarMenuItems
    {
      get => this._appBarMenuItems;
      set
      {
        if (object.Equals((object) this._appBarMenuItems, (object) value))
          return;
        this._appBarMenuItems = value;
        this.NotifyOfPropertyChange<IEnumerable<MenuItemInfo>>((Expression<Func<IEnumerable<MenuItemInfo>>>) (() => this.AppBarMenuItems));
      }
    }
  }
}
