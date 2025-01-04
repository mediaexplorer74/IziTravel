// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail.PublisherDetailPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Mtg.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail
{
  public class PublisherDetailPartViewModel : MtgObjectPartViewModel
  {
    protected override async Task<MtgObject> LoadMtgObjectAsync()
    {
      string[] array = ((IList<string>) ServiceFacade.CultureService.GetNeutralLanguageCodes()).OrderAs((IList<string>) ServiceFacade.SettingsService.GetAppSettings().Languages).ToArray<string>();
      string[] languages;
      if (string.IsNullOrWhiteSpace(this.Language))
        languages = array;
      else
        languages = new string[1]{ this.Language };
      MtgObjectFilter filter1 = new MtgObjectFilter(this.Uid, languages);
      filter1.Form = MtgObjectForm.Full;
      filter1.Includes = ContentSection.None;
      MtgObject publisher = await ServiceFacade.MtgObjectService.GetPublisherAsync(filter1);
      if (publisher != null)
      {
        MtgObject mtgObject = publisher;
        IMtgObjectService mtgObjectService = ServiceFacade.MtgObjectService;
        MtgPublisherChildrenCountFilter filter2 = new MtgPublisherChildrenCountFilter();
        filter2.Uid = publisher.Uid;
        filter2.Languages = ServiceFacade.SettingsService.GetAppSettings().Languages;
        CancellationToken ct = new CancellationToken();
        int childrenCountAsync = await mtgObjectService.GetPublisherChildrenCountAsync(filter2, ct);
        mtgObject.ChildrenCount = childrenCountAsync;
        mtgObject = (MtgObject) null;
      }
      return publisher;
    }

    protected override IScreen CreateScreenItem() => (IScreen) IoC.Get<PublisherDetailViewModel>();
  }
}
