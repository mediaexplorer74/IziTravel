// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.List.ChildrenListViewModel`1
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.List
{
  public abstract class ChildrenListViewModel<TListItem> : BaseListWithSearchViewModel<TListItem> where TListItem : class
  {
    protected IMtgObjectProvider MtgObjectProvider => this.Parent as IMtgObjectProvider;

    protected MtgObject MtgObject
    {
      get => this.MtgObjectProvider == null ? (MtgObject) null : this.MtgObjectProvider.MtgObject;
    }

    protected MtgObject MtgObjectRoot
    {
      get
      {
        return this.MtgObjectProvider == null ? (MtgObject) null : this.MtgObjectProvider.MtgObjectRoot;
      }
    }

    protected Content MtgObjectContent
    {
      get => this.MtgObject == null ? (Content) null : this.MtgObject.MainContent;
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.Items.Clear();
      this.LoadDataAsync();
    }

    protected override void OnActivate()
    {
    }

    protected override void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is ListItemViewModel listItemViewModel))
        return;
      ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), listItemViewModel.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), listItemViewModel.Language).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.ParentUid), this.MtgObject.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.RootUid), this.MtgObjectRoot != null ? this.MtgObjectRoot.Uid : (string) null).Navigate();
      this.Query = string.Empty;
    }

    protected override bool CanExecuteLoadDataCommand(object parameter) => false;
  }
}
