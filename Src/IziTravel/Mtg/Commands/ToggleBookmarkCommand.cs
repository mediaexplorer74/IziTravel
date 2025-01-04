// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Commands.ToggleBookmarkCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Commands
{
  public class ToggleBookmarkCommand : BaseCommand
  {
    private readonly MtgObject _mtgObject;
    private readonly string _parentUid;
    private bool _hasBookmark;

    public bool HasBookmark
    {
      get => this._hasBookmark;
      set
      {
        this.SetProperty<bool, string>(ref this._hasBookmark, value, (Expression<Func<string>>) (() => this.Label), propertyName: nameof (HasBookmark));
      }
    }

    public string Label => !this.HasBookmark ? this.AddBookmarkLabel : this.RemoveBookmarkLabel;

    public string AddBookmarkLabel { get; set; }

    public string RemoveBookmarkLabel { get; set; }

    public ToggleBookmarkCommand(MtgObject mtgObject, string parentUid)
    {
      this._mtgObject = mtgObject;
      this._parentUid = parentUid;
      this.UpdateHasBookmark();
    }

    public override bool CanExecute(object parameter)
    {
      return this._mtgObject != null && this._mtgObject.MainContent != null && this._mtgObject.Type != MtgObjectType.StoryNavigation;
    }

    public override async void Execute(object parameter)
    {
      if (this._mtgObject == null || this._mtgObject.MainContent == null)
        return;
      if (this.HasBookmark)
      {
        await ServiceFacade.MtgObjectService.RemoveBookmarkAsync(new MtgObjectFilter(this._mtgObject.Uid, this._mtgObject.MainContent.Language));
        ShellServiceFacade.DialogService.ShowToast(AppResources.ToastBookmarkRemoved, (Uri) null, (Action) null, false);
      }
      else
      {
        await ServiceFacade.MtgObjectService.CreateBookmarkAsync(this._mtgObject, this._parentUid ?? this._mtgObject.ParentUid);
        ShellServiceFacade.DialogService.ShowToast(AppResources.ToastBookmarkAdded, (Uri) null, (Action) null, false);
      }
      this.UpdateHasBookmark();
    }

    public async void UpdateHasBookmark()
    {
      if (this._mtgObject == null || this._mtgObject.MainContent == null)
        return;
      this.HasBookmark = await ServiceFacade.MtgObjectService.IsBookmarkExistsForMtgObjectAsync(new MtgObjectFilter(this._mtgObject.Uid, this._mtgObject.MainContent.Language));
    }
  }
}
