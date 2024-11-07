// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.MtgObjectTabViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail
{
  public abstract class MtgObjectTabViewModel : Conductor<IScreen>, IMtgObjectProvider
  {
    private IEnumerable<ButtonInfo> _appBarButtons;
    private IEnumerable<MenuItemInfo> _appBarMenuItems;

    protected IMtgObjectViewModel MtgViewModel => this.Parent as IMtgObjectViewModel;

    public MtgObject MtgObject
    {
      get => this.MtgViewModel == null ? (MtgObject) null : this.MtgViewModel.MtgObject;
    }

    public virtual MtgObject MtgObjectParent => (MtgObject) null;

    public virtual MtgObject MtgObjectRoot => (MtgObject) null;

    public Content MtgObjectContent
    {
      get => this.MtgObject == null ? (Content) null : this.MtgObject.MainContent;
    }

    public bool IsRated
    {
      get
      {
        return this.MtgObject != null && this.MtgObject.Rating != null && this.MtgObject.Rating.Average > 0.0;
      }
    }

    public double Rating
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.Rating == null ? 0.0 : this.MtgObject.Rating.Average / 2.0;
      }
    }

    public string RatingLabel
    {
      get
      {
        return string.Format(AppResources.LabelShortRatingCount, (object) (this.MtgObject == null || this.MtgObject.Rating == null ? 0 : this.MtgObject.Rating.Count));
      }
    }

    public IEnumerable<ButtonInfo> AppBarButtons => this._appBarButtons;

    public IEnumerable<MenuItemInfo> AppBarMenuItems => this._appBarMenuItems;

    protected override void OnInitialize()
    {
      base.OnInitialize();
      if (this.MtgViewModel == null)
        return;
      if (this.MtgViewModel.AvailableAppBarButtons != null && this._appBarButtons == null)
      {
        string[] appBarButtonKeys = this.GetAppBarButtonKeys();
        if (appBarButtonKeys != null)
          this._appBarButtons = (IEnumerable<ButtonInfo>) this.MtgViewModel.AvailableAppBarButtons.Where<ButtonInfo>((Func<ButtonInfo, bool>) (x => ((IEnumerable<string>) appBarButtonKeys).Contains<string>(x.Key))).OrderBy<ButtonInfo, int>((Func<ButtonInfo, int>) (x => x.Order));
      }
      if (this.MtgViewModel.AvailableAppBarMenuItems == null || this._appBarMenuItems != null)
        return;
      string[] appBarMenuItemKeys = this.GetAppBarMenuItemKeys();
      if (appBarMenuItemKeys == null)
        return;
      this._appBarMenuItems = (IEnumerable<MenuItemInfo>) this.MtgViewModel.AvailableAppBarMenuItems.Where<MenuItemInfo>((Func<MenuItemInfo, bool>) (x => ((IEnumerable<string>) appBarMenuItemKeys).Contains<string>(x.Key))).OrderBy<MenuItemInfo, int>((Func<MenuItemInfo, int>) (x => x.Order));
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      this.NotifyOfPropertyChange<IEnumerable<ButtonInfo>>((Expression<Func<IEnumerable<ButtonInfo>>>) (() => this.AppBarButtons));
      this.NotifyOfPropertyChange<IEnumerable<MenuItemInfo>>((Expression<Func<IEnumerable<MenuItemInfo>>>) (() => this.AppBarMenuItems));
    }

    protected virtual string[] GetAppBarButtonKeys() => (string[]) null;

    protected virtual string[] GetAppBarMenuItemKeys() => (string[]) null;
  }
}
