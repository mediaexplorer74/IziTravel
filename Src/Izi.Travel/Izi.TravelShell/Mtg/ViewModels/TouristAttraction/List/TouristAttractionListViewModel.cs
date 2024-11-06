// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.List.TouristAttractionListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;
using Izi.Travel.Shell.Mtg.Views.Common.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.List
{
  [View(typeof (ChildrenListView))]
  public class TouristAttractionListViewModel : 
    ChildrenListViewModel<TouristAttractionListItemViewModel>
  {
    protected override async Task<IEnumerable<TouristAttractionListItemViewModel>> GetDataAsync()
    {
      if (this.MtgObject == null || this.MtgObject.MainContent == null)
        return (IEnumerable<TouristAttractionListItemViewModel>) null;
      MtgObjectChildrenFilter objectChildrenFilter = new MtgObjectChildrenFilter();
      objectChildrenFilter.Uid = this.MtgObject.Uid;
      objectChildrenFilter.Languages = new string[1]
      {
        this.MtgObject.MainContent.Language
      };
      objectChildrenFilter.Types = new MtgObjectType[1]
      {
        MtgObjectType.TouristAttraction
      };
      objectChildrenFilter.Includes = ContentSection.None;
      objectChildrenFilter.Excludes = ContentSection.All;
      objectChildrenFilter.ShowHidden = false;
      objectChildrenFilter.Form = MtgObjectForm.Compact;
      objectChildrenFilter.Limit = new int?(int.MaxValue);
      MtgObjectChildrenFilter filter = objectChildrenFilter;
      MtgObject[] objectChildrenAsync;
      if (this.MtgObject.AccessType == MtgObjectAccessType.Offline)
        objectChildrenAsync = await ServiceFacade.MtgObjectDownloadService.GetMtgObjectChildrenAsync(filter);
      else
        objectChildrenAsync = await ServiceFacade.MtgObjectService.GetMtgObjectChildrenAsync(filter);
      MtgObject[] source = objectChildrenAsync;
      if (source == null || source.Length == 0)
        return (IEnumerable<TouristAttractionListItemViewModel>) null;
      string[] order = this.MtgObject.MainContent.Playback != null ? this.MtgObject.MainContent.Playback.Order : new string[0];
      System.Collections.Generic.List<MtgObject> orderedData = ((IEnumerable<MtgObject>) source).Where<MtgObject>((Func<MtgObject, bool>) (x => !x.Hidden)).OrderBy<MtgObject, int>((Func<MtgObject, int>) (x => Array.IndexOf<string>(order, x.Uid))).ToList<MtgObject>();
      return orderedData.Select<MtgObject, TouristAttractionListItemViewModel>((Func<MtgObject, int, TouristAttractionListItemViewModel>) ((x, i) => new TouristAttractionListItemViewModel((IListViewModel) this, this.MtgObject, orderedData[i], i + 1)));
    }

    protected override string GetItemTitle(TouristAttractionListItemViewModel item)
    {
      return item.FullTitle;
    }

    protected override bool GetItemIsHidden(TouristAttractionListItemViewModel item)
    {
      return item.IsHidden;
    }

    protected override void SetItemIsHidden(TouristAttractionListItemViewModel item, bool value)
    {
      item.IsHidden = value;
    }
  }
}
