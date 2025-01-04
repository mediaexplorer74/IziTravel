// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.ViewModels.List.BaseListWithSearchViewModel`1
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Common.ViewModels.List
{
  public abstract class BaseListWithSearchViewModel<TItemViewModel> : 
    BaseListViewModel<TItemViewModel>,
    IListWithSearchViewModel,
    IListViewModel,
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged
    where TItemViewModel : class
  {
    private string _query = string.Empty;

    public string Query
    {
      get => this._query;
      set
      {
        if (!(this._query != value))
          return;
        this._query = value;
        foreach (TItemViewModel itemViewModel in (Collection<TItemViewModel>) this.Items)
          this.SetItemIsHidden(itemViewModel, !BaseListWithSearchViewModel<TItemViewModel>.Contains(this.GetItemTitle(itemViewModel), this._query.Trim()));
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Query));
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.NoResults));
      }
    }

    public bool NoResults
    {
      get
      {
        return !this.IsListEmpty && !string.IsNullOrWhiteSpace(this.Query) && this.Items.Count<TItemViewModel>((Func<TItemViewModel, bool>) (x => !this.GetItemIsHidden(x))) == 0;
      }
    }

    public void Clear() => this.Query = string.Empty;

    protected virtual string GetItemTitle(TItemViewModel item) => (string) null;

    protected virtual bool GetItemIsHidden(TItemViewModel item) => false;

    protected virtual void SetItemIsHidden(TItemViewModel item, bool value)
    {
    }

    private static bool Contains(string source, string value)
    {
      return source != null && value != null && source.ToLower().Contains(value.ToLower());
    }
  }
}
