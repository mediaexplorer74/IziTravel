// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Player.InfoPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.Commands;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Player
{
  public class InfoPartViewModel : Screen
  {
    private static MtgObject _sharedMtgObjectRoot;
    private static MtgObject _sharedMtgObjectParent;
    private static MtgObject _sharedMtgObject;
    private readonly ILog _logger = LogManager.GetLog(typeof (InfoPartViewModel));
    private OpenVideoCommand _openVideoCommand;
    private ShareCommand _shareCommand;
    private ToggleBookmarkCommand _toggleBookmarkCommand;

    public MtgObject MtgObjectRoot { get; private set; }

    public MtgObject MtgObjectParent { get; private set; }

    public MtgObject MtgObject { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Language { get; private set; }

    public ReferenceListItemViewModel[] References { get; private set; }

    public bool HasReferences { get; private set; }

    public bool HasImages { get; private set; }

    public OpenVideoCommand OpenVideoCommand
    {
      get
      {
        return this._openVideoCommand ?? (this._openVideoCommand = new OpenVideoCommand(this.MtgObject, this.MtgObjectRoot));
      }
    }

    public ShareCommand ShareCommand
    {
      get
      {
        return this._shareCommand ?? (this._shareCommand = new ShareCommand(this.MtgObject, this.MtgObjectRoot));
      }
    }

    public ToggleBookmarkCommand ToggleBookmarkCommand
    {
      get
      {
        return this._toggleBookmarkCommand ?? (this._toggleBookmarkCommand = new ToggleBookmarkCommand(this.MtgObject, this.MtgObjectParent != null ? this.MtgObjectParent.Uid : (string) null));
      }
    }

    public InfoPartViewModel() => this.Intialize();

    private async void Intialize()
    {
      try
      {
        this.MtgObjectRoot = InfoPartViewModel._sharedMtgObjectRoot;
        this.MtgObjectParent = InfoPartViewModel._sharedMtgObjectParent;
        this.MtgObject = InfoPartViewModel._sharedMtgObject;
        if (this.MtgObject == null || this.MtgObject.MainContent == null)
          throw new Exception("Invalid input");
        if (this.MtgObject.MainContent.Title != null)
          this.Title = this.MtgObject.MainContent.Title.Trim();
        this.Language = this.MtgObject.MainContent.Language;
        if (this.MtgObject.MainContent.Description != null)
          this.Description = this.MtgObject.MainContent.Description.Trim();
        if (this.MtgObject.MainContent.References != null)
          this.References = ((IEnumerable<MtgObject>) this.MtgObject.MainContent.References).Select<MtgObject, ReferenceListItemViewModel>((Func<MtgObject, ReferenceListItemViewModel>) (x => new ReferenceListItemViewModel((IListViewModel) null, x))).Take<ReferenceListItemViewModel>(20).ToArray<ReferenceListItemViewModel>();
        this.HasReferences = this.References != null && this.References.Length != 0;
        this.HasImages = this.MtgObject.MainContent.Images != null && this.MtgObject.MainContent.Images.Length != 0;
        await ServiceFacade.MtgObjectService.CreateOrUpdateHistoryAsync(this.MtgObject, this.MtgObjectParent != null ? this.MtgObjectParent.Uid : (string) null);
      }
      catch (Exception ex)
      {
        this._logger.Error(ex);
        ShellServiceFacade.NavigationService.GoBack();
      }
    }

    public static void Navigate(
      MtgObject mtgObjectRoot,
      MtgObject mtgObjectParent,
      MtgObject mtgObject)
    {
      InfoPartViewModel._sharedMtgObjectRoot = mtgObjectRoot;
      InfoPartViewModel._sharedMtgObjectParent = mtgObjectParent;
      InfoPartViewModel._sharedMtgObject = mtgObject;
      ShellServiceFacade.NavigationService.UriFor<InfoPartViewModel>().Navigate();
    }
  }
}
