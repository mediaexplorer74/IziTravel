// ********************************************************************
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.List.SponsorListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Windows.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.List
{
  public class SponsorListViewModel : ChildrenListViewModel<SponsorListItemViewModel>
  {
    protected override Task<IEnumerable<SponsorListItemViewModel>> GetDataAsync()
    {
      return Task<IEnumerable<SponsorListItemViewModel>>.Factory.StartNew((Func<IEnumerable<SponsorListItemViewModel>>) (() => ((IEnumerable<Sponsor>) this.MtgObject.Sponsors).Select<Sponsor, SponsorListItemViewModel>((Func<Sponsor, SponsorListItemViewModel>) (x => new SponsorListItemViewModel(this.MtgObject, x)))));
    }

    protected override void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is SponsorListItemViewModel listItemViewModel))
        return;
      try
      {
        var _ = Launcher.LaunchUriAsync(new Uri(listItemViewModel.Url, UriKind.Absolute));
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
      }
    }
  }
}
