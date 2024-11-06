// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Download.ProfileDownloadTileViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Tiles;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Model.Profile;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Download
{
  public class ProfileDownloadTileViewModel : ProfileTileViewModel
  {
    private const int MaxImageCount = 5;
    private readonly Random _random;
    private readonly List<MtgObject> _items;
    private string _itemTitle;
    private string _itemImageUrl;
    private int _itemIndex;
    private int _totalCount;
    private double _totalSize;
    private RelayCommand _stateChangeCommand;

    public override string Title => AppResources.LabelDownloads;

    public override ProfileType Type => ProfileType.Download;

    public override bool IsFrozen => this._items.Count == 0;

    public string ItemTitle
    {
      get => this._itemTitle;
      set => this.SetProperty<string>(ref this._itemTitle, value, propertyName: nameof (ItemTitle));
    }

    public string ItemImageUrl
    {
      get => this._itemImageUrl;
      set
      {
        this.SetProperty<string>(ref this._itemImageUrl, value, propertyName: nameof (ItemImageUrl));
      }
    }

    public int TotalCount
    {
      get => this._totalCount;
      set => this.SetProperty<int>(ref this._totalCount, value, propertyName: nameof (TotalCount));
    }

    public double TotalSize
    {
      get => this._totalSize;
      set
      {
        this.SetProperty<double>(ref this._totalSize, value, (System.Action) (() => this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.TotalSizeString))), nameof (TotalSize));
      }
    }

    public string TotalSizeString => string.Format("{0:F1} MB", (object) this.TotalSize);

    public ProfileDownloadTileViewModel()
    {
      this._random = new Random(Environment.TickCount);
      this._items = new List<MtgObject>(5);
    }

    public RelayCommand StateChangeCommand
    {
      get
      {
        return this._stateChangeCommand ?? (this._stateChangeCommand = new RelayCommand(new Action<object>(this.ExecuteStateChangeCommand), new Func<object, bool>(this.CanExecuteStateChangeCommand)));
      }
    }

    private bool CanExecuteStateChangeCommand(object parameter)
    {
      return (FlipTileState) parameter != FlipTileState.Front && !this.IsDataLoading && this._items.Count > 0;
    }

    private void ExecuteStateChangeCommand(object parameter)
    {
      if (this._itemIndex >= this._items.Count)
        this._itemIndex = 0;
      MtgObject mtgObject = this._items[this._itemIndex];
      if (mtgObject.MainContent != null)
        this.ItemTitle = mtgObject.MainContent.Title;
      if (mtgObject.MainImageMedia != null && mtgObject.ContentProvider != null)
        this.ItemImageUrl = ServiceFacade.MediaService.GetImageUrl(mtgObject.MainImageMedia.Uid, mtgObject.ContentProvider.Uid, ImageFormat.Low480X360);
      ++this._itemIndex;
    }

    protected override void RefreshCommands()
    {
      base.RefreshCommands();
      this.StateChangeCommand.RaiseCanExecuteChanged();
    }

    protected override async Task LoadDataProcess(CancellationToken token)
    {
      IMtgObjectDownloadService objectDownloadService1 = ServiceFacade.MtgObjectDownloadService;
      MtgObjectListFilter objectListFilter1 = new MtgObjectListFilter();
      objectListFilter1.Languages = ServiceFacade.CultureService.GetNeutralLanguageCodes();
      objectListFilter1.Types = new MtgObjectType[2]
      {
        MtgObjectType.Tour,
        MtgObjectType.Museum
      };
      MtgObjectListFilter filter1 = objectListFilter1;
      this.TotalCount = await objectDownloadService1.GetMtgObjectCountAsync(filter1);
      this.TotalSize = await Task<double>.Factory.StartNew((Func<double>) (() => IsolatedStorageFileHelper.GetDirectorySize(ServiceFacade.MediaService.GetLocalDirectory()).ToMegabytes()), token);
      MtgObject[] downloads = (MtgObject[]) null;
      if (this.TotalCount > 0)
      {
        int maxValue = this.TotalCount > 5 ? this.TotalCount - 5 : 0;
        MtgObject[] mtgObjectArray = downloads;
        IMtgObjectDownloadService objectDownloadService2 = ServiceFacade.MtgObjectDownloadService;
        MtgObjectListFilter objectListFilter2 = new MtgObjectListFilter();
        objectListFilter2.Offset = new int?(this._random.Next(0, maxValue));
        objectListFilter2.Limit = new int?(5);
        objectListFilter2.Languages = ServiceFacade.CultureService.GetNeutralLanguageCodes();
        objectListFilter2.Form = MtgObjectForm.Compact;
        objectListFilter2.Includes = ContentSection.None;
        objectListFilter2.Excludes = ContentSection.All;
        objectListFilter2.Types = new MtgObjectType[2]
        {
          MtgObjectType.Tour,
          MtgObjectType.Museum
        };
        MtgObjectListFilter filter2 = objectListFilter2;
        downloads = await objectDownloadService2.GetMtgObjectListAsync(filter2);
      }
      this._itemIndex = 0;
      this._items.Clear();
      if (downloads != null && downloads.Length != 0)
      {
        foreach (MtgObject mtgObject in (IEnumerable<MtgObject>) ((IEnumerable<MtgObject>) downloads).OrderBy<MtgObject, int>((Func<MtgObject, int>) (x => this._random.Next(downloads.Length))))
        {
          if (mtgObject.MainImageMedia != null)
            this._items.Add(mtgObject);
        }
      }
      ((System.Action) (() => this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsFrozen)))).OnUIThread();
    }
  }
}
