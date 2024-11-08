// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.UriMapper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Helpers;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.ViewModels;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Linq.Expressions;
using System.Net;
//using System.Windows.Navigation;

#nullable disable
namespace Izi.Travel.Shell
{
    //RnD

  public class UriMapper : UriMapperBase
  {
    public /*override*/ Uri MapUri(Uri uri)
    {
      if (uri.IsWellFormedOriginalString())
      {
        string str = HttpUtility.UrlDecode(uri.OriginalString);
        if (str.StartsWith("/Protocol?encodedLaunchUri="))
        {
          MtgLinkInfo mtgLinkInfo = MtgLinkHelper.Parse(
              str.Replace("/Protocol?encodedLaunchUri=", 
              string.Empty));

          if (mtgLinkInfo == null)
            return ShellServiceFacade.NavigationService.UriFor<MainViewModel>().BuildUri();

          if (string.Equals(mtgLinkInfo.Language, "any", StringComparison.CurrentCultureIgnoreCase))
            mtgLinkInfo.Language = (string) null;

          return ShellServiceFacade.NavigationService.UriFor<RedirectViewModel>()
                    .WithParam<string>((Expression<Func<RedirectViewModel, string>>)
                    (x => x.Uid), mtgLinkInfo.Uid).WithParam<string>(
              (Expression<Func<RedirectViewModel, string>>) (x => x.Language), 
              mtgLinkInfo.Language).BuildUri();
        }
      }
      return uri;
    }
  }
}
