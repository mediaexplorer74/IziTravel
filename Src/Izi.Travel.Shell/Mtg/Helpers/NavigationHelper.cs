// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Helpers.NavigationHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Managers;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Player;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map;
using Izi.Travel.Shell.ViewModels;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Helpers
{
  public static class NavigationHelper
  {
    public static void TryGoBack(CancelEventArgs e, string uid, string language)
    {
      if (ShellServiceFacade.NavigationService.CanGoBack)
        return;
      e.Cancel = true;
      if (uid != null && language != null)
        ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), language).Navigate();
      else
        ShellServiceFacade.NavigationService.UriFor<MainViewModel>().Navigate();
      ShellServiceFacade.NavigationService.RemoveBackEntry();
    }

    public static void TryGoBack()
    {
      if (ShellServiceFacade.NavigationService.CanGoBack)
      {
        ShellServiceFacade.NavigationService.GoBack();
      }
      else
      {
        ShellServiceFacade.NavigationService.UriFor<MainViewModel>().Navigate();
        ShellServiceFacade.NavigationService.RemoveBackEntry();
      }
    }

    public static void NavigateToUrl(string url)
    {
      if (string.IsNullOrWhiteSpace(url))
        return;
      ShellServiceFacade.NavigationService.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
    }

    public static void NavigateToDetails(
      MtgObjectType type,
      string uid,
      string language,
      string parentUid)
    {
      switch (type)
      {
        case MtgObjectType.Museum:
        case MtgObjectType.Tour:
          ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), language).Navigate();
          break;
        case MtgObjectType.Exhibit:
        case MtgObjectType.StoryNavigation:
          ShellServiceFacade.NavigationService.UriFor<PlayerPartViewModel>().WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.ParentUid), parentUid).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Language), language).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Uid), uid).Navigate();
          break;
        case MtgObjectType.TouristAttraction:
          ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), language).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.ParentUid), parentUid).Navigate();
          break;
        case MtgObjectType.Collection:
          ShellServiceFacade.NavigationService.UriFor<PlayerPartViewModel>().WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.ParentUid), uid).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Language), language).Navigate();
          break;
      }
    }

    public static void NavigateToAudio(
      MtgObjectType type,
      string uid,
      string language,
      string parentUid,
      bool autoPlay = false)
    {
      ShellServiceFacade.NavigationService.Navigate(NavigationHelper.UriToAudio(type, uid, language, parentUid, autoPlay));
    }

    public static Uri UriToAudio(
      MtgObjectType type,
      string uid,
      string language,
      string parentUid,
      bool autoPlay = false)
    {
      switch (type)
      {
        case MtgObjectType.Museum:
        case MtgObjectType.Collection:
          return ShellServiceFacade.NavigationService.UriFor<PlayerPartViewModel>().WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.ParentUid), uid).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Language), language).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Uid), uid).WithParam<bool>((Expression<Func<PlayerPartViewModel, bool>>) (x => x.AutoPlay), (autoPlay ? 1 : 0) != 0).BuildUri();
        case MtgObjectType.Exhibit:
        case MtgObjectType.StoryNavigation:
          return ShellServiceFacade.NavigationService.UriFor<PlayerPartViewModel>().WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.ParentUid), parentUid).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Language), language).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Uid), uid).WithParam<bool>((Expression<Func<PlayerPartViewModel, bool>>) (x => x.AutoPlay), (autoPlay ? 1 : 0) != 0).BuildUri();
        case MtgObjectType.Tour:
        case MtgObjectType.TouristAttraction:
          if (type == MtgObjectType.TouristAttraction && TourPlaybackManager.IsTourAttached(parentUid, language))
            return ShellServiceFacade.NavigationService.UriFor<TourMapPartViewModel>().WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Uid), parentUid).WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Language), language).BuildUri();
          return ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), language).WithParam<bool>((Expression<Func<DetailPartViewModel, bool>>) (x => x.AutoPlay), (autoPlay ? 1 : 0) != 0).BuildUri();
        default:
          throw new NotSupportedException();
      }
    }
  }
}
