// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Explore.Flyouts.ExploreFlyoutTypeViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.Helpers;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Explore.Flyouts
{
  public class ExploreFlyoutTypeViewModel : ExploreFlyoutViewModel
  {
    private static readonly MtgObjectType[] SupportedTypes = new MtgObjectType[2]
    {
      MtgObjectType.Museum,
      MtgObjectType.Tour
    };
    private readonly List<KeyValueModel> _items;
    private KeyValueModel _selectedItem;
    private bool _isClearing;
    private RelayCommand _selectCommand;

    public IEnumerable<KeyValueModel> Items => (IEnumerable<KeyValueModel>) this._items;

    public KeyValueModel SelectedItem
    {
      get => this._selectedItem;
      set
      {
        this.SetProperty<KeyValueModel>(ref this._selectedItem, value, (Action) (() =>
        {
          MtgObjectType key = (MtgObjectType) this.SelectedItem.Key;
          ExploreViewModel exploreViewModel = this.ExploreViewModel;
          MtgObjectType[] mtgObjectTypeArray;
          if (key == MtgObjectType.Unknown)
            mtgObjectTypeArray = ExploreFlyoutTypeViewModel.SupportedTypes;
          else
            mtgObjectTypeArray = new MtgObjectType[1]{ key };
          exploreViewModel.SelectedTypes = mtgObjectTypeArray;
        }), nameof (SelectedItem));
      }
    }

    public ExploreFlyoutTypeViewModel(ExploreViewModel exploreViewModel)
      : base(exploreViewModel)
    {
      this._items = new List<KeyValueModel>()
      {
        new KeyValueModel((object) MtgObjectType.Unknown, (object) AppResources.LabelAll)
      };
      foreach (MtgObjectType supportedType in ExploreFlyoutTypeViewModel.SupportedTypes)
        this._items.Add(new KeyValueModel((object) supportedType, (object) MtgObjectHelper.GetTypeName(supportedType)));
      this.SelectedItem = this._items.First<KeyValueModel>();
    }

    public RelayCommand SelectCommand
    {
      get
      {
        return this._selectCommand ?? (this._selectCommand = new RelayCommand(new Action<object>(this.ExecuteSelectCommand), new Func<object, bool>(this.CanExecuteSelectCommand)));
      }
    }

    private bool CanExecuteSelectCommand(object parameter) => !this._isClearing;

    private async void ExecuteSelectCommand(object parameter)
    {
      this.CloseCommand.Execute((object) null);
      await this.ExploreViewModel.RefreshItemsDataAsync();
    }

    public void Clear()
    {
      this._isClearing = true;
      this.SelectedItem = this._items[0];
      this._isClearing = false;
    }
  }
}
