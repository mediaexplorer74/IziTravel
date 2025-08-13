// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.List.ReviewListItemViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Common.ViewModels.List;
using Izi.Travel.Core.Resources;
using System;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.List
{
  public class ReviewListItemViewModel
  {
    public int Id { get; set; }

    public string Text { get; set; }

    public string ReviewerName { get; set; }

    public double Rating { get; set; }

    public DateTime Date { get; set; }

    public ReviewListItemViewModel(IListViewModel listViewModel, Review review)
    {
      this.Id = review.Id;
      this.ReviewerName = review.ReviewerName ?? AppResources.EmptyReviewerName;
      this.Rating = (double) review.Rating / 2.0;
      this.Date = review.Date;
      this.Text = review.Text;
    }
  }
}
