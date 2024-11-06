// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.ProfileListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Managers;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile
{
  public abstract class ProfileListItemViewModel : ListItemViewModel
  {
    private DownloadProcessState _state;
    private double _progress;

    public DownloadProcessState State
    {
      get => this._state;
      set
      {
        this.SetProperty<DownloadProcessState>(ref this._state, value, new Action(this.OnStateChanged), nameof (State));
      }
    }

    public double Progress
    {
      get => this._progress;
      set
      {
        this.SetProperty<double>(ref this._progress, value, new Action(this.OnProgressChanged), nameof (Progress));
      }
    }

    public virtual string StateDownloadedString
    {
      get
      {
        return this.State == DownloadProcessState.Downloaded || this.State == DownloadProcessState.Updated ? AppResources.LabelDownloaded : (string) null;
      }
    }

    public virtual bool IsUpdating => this.State == DownloadProcessState.Updating;

    protected ProfileListItemViewModel(
      IListViewModel listViewModel,
      MtgObject mtgObject,
      bool skipDownloadManagerCheck = false)
      : base(listViewModel, mtgObject)
    {
      if (skipDownloadManagerCheck)
        return;
      Tuple<DownloadProcessState, double> objectDownloadInfo = DownloadManager.Instance.GetMtgObjectDownloadInfo(mtgObject);
      if (objectDownloadInfo == null)
        return;
      this.State = objectDownloadInfo.Item1;
      this.Progress = objectDownloadInfo.Item2;
    }

    protected virtual void OnStateChanged()
    {
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsUpdating));
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.StateDownloadedString));
    }

    protected virtual void OnProgressChanged()
    {
    }
  }
}
