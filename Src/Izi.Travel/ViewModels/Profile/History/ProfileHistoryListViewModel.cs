// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.History.ProfileHistoryListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.History
{
  public class ProfileHistoryListViewModel : 
    ProfileListViewModel<ProfileHistoryListGroupItemViewModel>
  {
    public override string DisplayName
    {
      get => AppResources.LabelHistory;
      set => throw new NotImplementedException();
    }

    protected override string GetClearPrompt() => AppResources.PromptClearHistory;

    protected override Task ClearProcess()
    {
      return ServiceFacade.MtgObjectService.ClearHistoryListAsync();
    }

    protected override bool CanExecuteLoadDataCommand(object parameter) => false;

    protected override IEnumerable<ProfileListItemViewModel> GetProfileListItems()
    {
      return (IEnumerable<ProfileListItemViewModel>) this.Items.SelectMany<ProfileHistoryListGroupItemViewModel, ProfileHistoryListItemViewModel>((Func<ProfileHistoryListGroupItemViewModel, IEnumerable<ProfileHistoryListItemViewModel>>) (x => (IEnumerable<ProfileHistoryListItemViewModel>) x));
    }

    protected override Task<IEnumerable<ProfileHistoryListGroupItemViewModel>> GetDataAsync()
    {
      return Task.Run<IEnumerable<ProfileHistoryListGroupItemViewModel>>((Func<Task<IEnumerable<ProfileHistoryListGroupItemViewModel>>>) (async () =>
      {
        MtgObject[] historyListAsync = await ServiceFacade.MtgObjectService.GetHistoryListAsync(new HistoryListFilter()
        {
          Types = new MtgObjectType[5]
          {
            MtgObjectType.Tour,
            MtgObjectType.TouristAttraction,
            MtgObjectType.Museum,
            MtgObjectType.Collection,
            MtgObjectType.Exhibit
          },
          From = DateTime.Now.AddDays(-3.0),
          To = DateTime.Now
        });
        if (historyListAsync == null)
          return (IEnumerable<ProfileHistoryListGroupItemViewModel>) null;
        System.Collections.Generic.List<ProfileHistoryListGroupItemViewModel> source1 = new System.Collections.Generic.List<ProfileHistoryListGroupItemViewModel>();
        foreach (IGrouping<DateTime, MtgObject> source2 in ((IEnumerable<MtgObject>) historyListAsync).OrderByDescending<MtgObject, DateTime>((Func<MtgObject, DateTime>) (x => x.DateTime)).GroupBy<MtgObject, DateTime>((Func<MtgObject, DateTime>) (x =>
        {
          DateTime dateTime = x.DateTime;
          int year = dateTime.Year;
          dateTime = x.DateTime;
          int month = dateTime.Month;
          dateTime = x.DateTime;
          int day = dateTime.Day;
          return new DateTime(year, month, day);
        })))
        {
          ProfileHistoryListGroupItemViewModel groupItemViewModel = new ProfileHistoryListGroupItemViewModel(ProfileHistoryListViewModel.GetGroupTitle(source2.Key));
          groupItemViewModel.AddRange(source2.Select<MtgObject, ProfileHistoryListItemViewModel>((Func<MtgObject, ProfileHistoryListItemViewModel>) (x => new ProfileHistoryListItemViewModel((IListViewModel) this, x))));
          source1.Add(groupItemViewModel);
        }
        return source1.AsEnumerable<ProfileHistoryListGroupItemViewModel>();
      }));
    }

    private static string GetGroupTitle(DateTime dateTime)
    {
      DateTime date1 = DateTime.Now.Date;
      DateTime date2 = dateTime.Date;
      if (date2 == date1)
        return AppResources.LabelToday;
      return date1.Subtract(date2) == TimeSpan.FromDays(1.0) ? AppResources.LabelYesterday : date2.ToString("dd.MM.yyyy");
    }
  }
}
