// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items.PlayerEndItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Navigation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items
{
  public class PlayerEndItemViewModel : PlayerItemViewModel
  {
    private RelayCommand _returnCommand;

    public RelayCommand ReturnCommand
    {
      get
      {
        return this._returnCommand ?? (this._returnCommand = new RelayCommand(new Action<object>(this.Return)));
      }
    }

    private void Return(object parameter)
    {
      Uri uri = ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), this.MtgObjectRoot.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), this.MtgObjectRoot.Language).BuildUri();
      JournalEntry journalEntry = ShellServiceFacade.NavigationService.BackStack.FirstOrDefault<JournalEntry>();
      if (journalEntry != null && UriHelper.EqualsByCommonParameters(uri, journalEntry.Source))
        ShellServiceFacade.NavigationService.GoBack();
      else
        ShellServiceFacade.NavigationService.Navigate(uri);
    }

    public PlayerEndItemViewModel(
      PlayerViewModel playerViewModel,
      MtgObject mtgObjectRoot,
      MtgObject mtgObject)
      : base(playerViewModel, -1, mtgObjectRoot, (MtgObject) null, mtgObject)
    {
    }

    protected override string GetImageUrl()
    {
      return this.MtgObject == null || this.MtgObject.MainImageMedia == null || this.MtgObject.ContentProvider == null ? (string) null : ServiceFacade.MediaService.GetImageUrl(this.MtgObject.MainImageMedia.Uid, this.MtgObject.ContentProvider.Uid, ImageFormat.High800X600);
    }
  }
}
