// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.List.ReviewListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.List
{
  public class ReviewListViewModel : ChildrenListViewModel<ReviewListItemViewModel>
  {
    private string _isListEmptyPlaceholder;
    private RelayCommand _rateCommand;

    public double Average
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.Rating == null ? 0.0 : this.MtgObject.Rating.Average / 2.0;
      }
    }

    public int Count
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.Rating == null ? 0 : this.MtgObject.Rating.Count;
      }
    }

    public string AverageString => this.Average.ToString("F1");

    public string CountString
    {
      get => string.Format(AppResources.LabelBasedOnRatings, (object) this.Count);
    }

    public bool IsRated
    {
      get
      {
        return this.MtgObject != null && this.MtgObject.Rating != null && this.MtgObject.Rating.Average > 0.0;
      }
    }

    public bool CanRate => RateHelper.CanRate(this.MtgObject.Uid, this.MtgObject.Hash);

    public string IsListEmptyPlaceholder
    {
      get => this._isListEmptyPlaceholder;
      set
      {
        this.SetProperty<string>(ref this._isListEmptyPlaceholder, value, propertyName: nameof (IsListEmptyPlaceholder));
      }
    }

    public RelayCommand RateCommand
    {
      get
      {
        return this._rateCommand ?? (this._rateCommand = new RelayCommand(new Action<object>(this.Rate)));
      }
    }

    private void Rate(object parameter)
    {
      ShellServiceFacade.NavigationService.UriFor<RatePartViewModel>().WithParam<string>((Expression<Func<RatePartViewModel, string>>) (x => x.Uid), this.MtgObject.Uid).WithParam<MtgObjectType>((Expression<Func<RatePartViewModel, MtgObjectType>>) (x => x.Type), this.MtgObject.Type).WithParam<string>((Expression<Func<RatePartViewModel, string>>) (x => x.Hash), this.MtgObject.Hash).WithParam<string>((Expression<Func<RatePartViewModel, string>>) (x => x.Language), this.MtgObject.MainContent.Language).WithParam<string>((Expression<Func<RatePartViewModel, string>>) (x => x.Title), this.MtgObject.MainContent.Title).Navigate();
    }

    protected override void OnActivate()
    {
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.CanRate));
      this.RefreshCommand.Execute((object) null);
    }

    protected override async Task<IEnumerable<ReviewListItemViewModel>> GetDataAsync()
    {
      bool error = false;
      try
      {
        IMtgObjectService mtgObjectService = ServiceFacade.MtgObjectService;
        GetReviewsFilter filter = new GetReviewsFilter();
        filter.Uid = this.MtgObject.Uid;
        filter.Limit = new int?(10);
        filter.Offset = new int?(this.Items.Count<ReviewListItemViewModel>());
        CancellationToken ct = new CancellationToken();
        return ((IEnumerable<Review>) await mtgObjectService.GetReviewsAsync(filter, ct)).Where<Review>((Func<Review, bool>) (x => this.Items.All<ReviewListItemViewModel>((Func<ReviewListItemViewModel, bool>) (y => y.Id != x.Id)))).Select<Review, ReviewListItemViewModel>((Func<Review, ReviewListItemViewModel>) (x => new ReviewListItemViewModel((IListViewModel) this, x)));
      }
      catch (Exception ex)
      {
        error = true;
        this.Logger.Error(ex);
        return (IEnumerable<ReviewListItemViewModel>) null;
      }
      finally
      {
        this.IsListEmptyPlaceholder = error ? AppResources.LabelReviewsAreCurrentlyUnavailable : AppResources.LabelBeTheFirstToReview;
      }
    }

    protected override bool CanExecuteLoadDataCommand(object parameter) => true;
  }
}
