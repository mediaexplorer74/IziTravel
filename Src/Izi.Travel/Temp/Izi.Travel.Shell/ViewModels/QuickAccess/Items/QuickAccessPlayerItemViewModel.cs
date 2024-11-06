// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.QuickAccess.Items.QuickAccessPlayerItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Media.ViewModels.Audio;
using Izi.Travel.Shell.Mtg.Commands;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Player;
using System;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.QuickAccess.Items
{
  public sealed class QuickAccessPlayerItemViewModel : QuickAccessBaseItemViewModel
  {
    private AudioContentViewModel _audioContentViewModel;
    private readonly MtgObject _mtgObjectRoot;
    private readonly MtgObject _mtgObjectParent;
    private readonly MtgObject _mtgObject;
    private readonly int _index;
    private readonly bool _hasNext;
    private readonly bool _hasPrevious;
    private string _imageUrl;
    private RelayCommand _navigateCommand;
    private RelayCommand _navigateToParentCommand;
    private OpenVideoCommand _openVideoCommand;
    private RelayCommand _showInfoCommand;

    public AudioContentViewModel AudioViewModel
    {
      get
      {
        return this._audioContentViewModel ?? (this._audioContentViewModel = new AudioContentViewModel());
      }
    }

    public int Index => this._index;

    public bool HasNext => this._hasNext;

    public bool HasPrevious => this._hasPrevious;

    public MtgObject MtgObject => this._mtgObject;

    public MtgObject MtgObjectParent => this._mtgObjectParent;

    public string ParentUid
    {
      get => this._mtgObjectParent == null ? (string) null : this._mtgObjectParent.Uid;
    }

    public string ParentTitle
    {
      get => this._mtgObjectParent == null ? (string) null : this._mtgObjectParent.Title;
    }

    public string Uid => this._mtgObject == null ? (string) null : this._mtgObject.Uid;

    public string Key => this._mtgObject == null ? (string) null : this._mtgObject.Key;

    public string Language => this._mtgObject == null ? (string) null : this._mtgObject.Language;

    public string Number
    {
      get
      {
        return this._mtgObject == null || this._mtgObject.Location == null ? (string) null : this._mtgObject.Location.Number;
      }
    }

    public string Title => this._mtgObject == null ? (string) null : this._mtgObject.Title;

    public string FullTitle
    {
      get
      {
        return string.IsNullOrWhiteSpace(this.Number) ? this.Title : string.Format("#{0} {1}", (object) this.Number, (object) this.Title);
      }
    }

    public string ImageUrl
    {
      get => this._imageUrl;
      set => this.SetProperty<string>(ref this._imageUrl, value, propertyName: nameof (ImageUrl));
    }

    public QuickAccessPlayerItemViewModel(
      QuickAccessViewModel quickAccessViewModel,
      MtgObject mtgObjectParent,
      MtgObject mtgObject,
      int index,
      bool hasNext,
      bool hasPrevious)
      : base(quickAccessViewModel)
    {
      if (this._mtgObjectParent != null && (this._mtgObjectParent.Type == MtgObjectType.Museum || this._mtgObjectParent.Type == MtgObjectType.Tour))
        this._mtgObjectRoot = this._mtgObjectParent;
      this._mtgObjectParent = mtgObjectParent;
      this._mtgObject = mtgObject;
      this._index = index;
      this._hasNext = hasNext;
      this._hasPrevious = hasPrevious;
    }

    public RelayCommand NavigateCommand
    {
      get
      {
        return this._navigateCommand ?? (this._navigateCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateCommand), new Func<object, bool>(this.CanExecuteNavigateCommand)));
      }
    }

    private bool CanExecuteNavigateCommand(object parameter) => this.MtgObject != null;

    private void ExecuteNavigateCommand(object parameter)
    {
      NavigationHelper.NavigateToAudio(this.MtgObject.Type, this.MtgObject.Uid, this.MtgObject.Language, this.ParentUid);
    }

    public RelayCommand NavigateToParentCommand
    {
      get
      {
        return this._navigateToParentCommand ?? (this._navigateToParentCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateToParentCommand), new Func<object, bool>(this.CanExecuteNavigateToParentCommand)));
      }
    }

    private bool CanExecuteNavigateToParentCommand(object parameter)
    {
      return this._mtgObjectParent != null;
    }

    private void ExecuteNavigateToParentCommand(object parameter)
    {
      NavigationHelper.NavigateToDetails(this._mtgObjectParent.Type, this._mtgObjectParent.Uid, this._mtgObjectParent.Language, (string) null);
    }

    public OpenVideoCommand OpenVideoCommand
    {
      get
      {
        return this._openVideoCommand ?? (this._openVideoCommand = new OpenVideoCommand(this._mtgObject, this._mtgObjectRoot));
      }
    }

    public RelayCommand ShowInfoCommand
    {
      get
      {
        return this._showInfoCommand ?? (this._showInfoCommand = new RelayCommand(new Action<object>(this.ShowInfo)));
      }
    }

    private void ShowInfo(object parameter)
    {
      InfoPartViewModel.Navigate(this._mtgObjectRoot, this._mtgObjectParent, this._mtgObject);
    }

    protected override void OnActivate()
    {
      if (this._mtgObject != null && this._mtgObject.MainImageMedia != null && this._mtgObject.ContentProvider != null)
        this.ImageUrl = ServiceFacade.MediaService.GetImageUrl(this._mtgObject.MainImageMedia.Uid, this._mtgObject.ContentProvider.Uid, ImageFormat.Low480X360);
      this.AudioViewModel.Activate(this._mtgObject, this._mtgObjectParent, this._mtgObjectRoot, ActivationTypeParameter.Manual);
    }

    protected override void OnDeactivate()
    {
      this.ImageUrl = (string) null;
      this.AudioViewModel.Deactivate();
    }
  }
}
