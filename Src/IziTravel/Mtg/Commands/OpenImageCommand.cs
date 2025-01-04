// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Commands.OpenImageCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Media.Provider;
using Izi.Travel.Shell.Media.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Commands
{
  public class OpenImageCommand : BaseCommand
  {
    private readonly MtgObject _mtgObject;

    public OpenImageCommand(MtgObject mtgObject) => this._mtgObject = mtgObject;

    public override bool CanExecute(object parameter) => true;

    public override void Execute(object parameter)
    {
      if (this._mtgObject == null || this._mtgObject.ContentProvider == null || this._mtgObject.MainContent == null || this._mtgObject.MainContent.Images == null)
        return;
      MediaPlayerDataProvider.Instance.MediaData = ((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) this._mtgObject.MainContent.Images).Where<Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Business.Entities.Data.Media, bool>) (x => x.Type == MediaType.Story)).Select<Izi.Travel.Business.Entities.Data.Media, MediaInfo>((Func<Izi.Travel.Business.Entities.Data.Media, MediaInfo>) (x => new MediaInfo()
      {
        MediaFormat = MediaFormat.Image,
        MediaUid = x.Uid,
        Title = x.Title,
        ContentProviderUid = this._mtgObject.ContentProvider.Uid,
        PreviewUrl = ServiceFacade.MediaService.GetImageUrl(x.Uid, this._mtgObject.ContentProvider.Uid, ImageFormat.Low480X360),
        ImageUrl = ServiceFacade.MediaService.GetImageUrl(x.Uid, this._mtgObject.ContentProvider.Uid, ImageFormat.High800X600)
      })).ToArray<MediaInfo>();
      ShellServiceFacade.NavigationService.UriFor<MediaPlayerPartViewModel>().WithParam<MediaFormat>((Expression<Func<MediaPlayerPartViewModel, MediaFormat>>) (x => x.MediaFormat), MediaFormat.Image).Navigate();
    }
  }
}
