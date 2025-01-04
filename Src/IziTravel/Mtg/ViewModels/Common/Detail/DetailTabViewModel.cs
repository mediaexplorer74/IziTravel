// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.DetailTabViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail
{
  public class DetailTabViewModel : MtgObjectTabViewModel
  {
    private string _language;

    protected IDetailViewModel DetailViewModel => this.Parent as IDetailViewModel;

    public override MtgObject MtgObjectParent
    {
      get => this.DetailViewModel == null ? (MtgObject) null : this.DetailViewModel.MtgObjectParent;
    }

    public override MtgObject MtgObjectRoot
    {
      get => this.DetailViewModel == null ? (MtgObject) null : this.DetailViewModel.MtgObjectRoot;
    }

    public string Language => this._language;

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this._language = this.GetDetailLanguage();
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      string detailLanguage = this.GetDetailLanguage();
      if (!(this._language != detailLanguage))
        return;
      this._language = detailLanguage;
      this.OnLanguageChanged();
    }

    protected virtual void OnLanguageChanged()
    {
      if (this.ActiveItem is IListWithSearchViewModel activeItem)
      {
        activeItem.Clear();
        if (activeItem.RefreshCommand.CanExecute((object) null))
          activeItem.RefreshCommand.Execute((object) null);
      }
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Language));
    }

    private string GetDetailLanguage()
    {
      return this.DetailViewModel == null || this.DetailViewModel.DetailPartViewModel == null ? (string) null : this.DetailViewModel.DetailPartViewModel.SelectedLanguage;
    }
  }
}
