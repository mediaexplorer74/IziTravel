// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Numpad.NumpadFlyoutViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Helper;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Mtg.Model;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Numpad
{
  public class NumpadFlyoutViewModel : BaseSearchFlyoutViewModel
  {
    private const int MaxNumberLength = 6;
    private bool _isEmpty;
    private string _number = string.Empty;
    private RelayCommand _typeCommand;
    private RelayCommand _eraseCommand;

    public bool IsEmpty
    {
      get => this._isEmpty;
      set => this.SetProperty<bool>(ref this._isEmpty, value, propertyName: nameof (IsEmpty));
    }

    public string Number
    {
      get => this._number;
      set
      {
        this.SetProperty<string>(ref this._number, value, new System.Action(((BaseSearchFlyoutViewModel) this).RefreshCommands), nameof (Number));
      }
    }

    public NumpadFlyoutViewModel(IScreen parentScreen)
      : base(parentScreen)
    {
    }

    public RelayCommand TypeCommand
    {
      get
      {
        return this._typeCommand ?? (this._typeCommand = new RelayCommand(new Action<object>(this.ExecuteTypeCommand), new Func<object, bool>(this.CanExecuteTypeCommand)));
      }
    }

    private bool CanExecuteTypeCommand(object parameter) => !this.IsBusy && this.Number.Length < 6;

    private void ExecuteTypeCommand(object parameter) => this.Number += (string) parameter;

    public RelayCommand EraseCommand
    {
      get
      {
        return this._eraseCommand ?? (this._eraseCommand = new RelayCommand(new Action<object>(this.ExecuteEraseCommand), new Func<object, bool>(this.CanExecuteEraseCommand)));
      }
    }

    private bool CanExecuteEraseCommand(object parameter)
    {
      return !this.IsBusy && !string.IsNullOrWhiteSpace(this.Number);
    }

    private void ExecuteEraseCommand(object parameter)
    {
      this.Number = this.Number.Substring(0, this.Number.Length - 1);
    }

    protected override bool CanExecuteSearchCommand(object parameter)
    {
      return base.CanExecuteSearchCommand(parameter) && !string.IsNullOrWhiteSpace(this.Number);
    }

    protected override void RefreshCommands()
    {
      base.RefreshCommands();
      this.TypeCommand.RaiseCanExecuteChanged();
      this.EraseCommand.RaiseCanExecuteChanged();
      this.SearchCommand.RaiseCanExecuteChanged();
    }

    protected override async Task<SearchFlyoutResult> SearchTask(object parameter)
    {
      this.IsEmpty = false;
      MtgObjectChildrenExtendedFilter filter = new MtgObjectChildrenExtendedFilter();
      filter.Limit = new int?(1);
      filter.Uid = this.ParentUid;
      filter.PageExhibitNumber = this.Number;
      filter.Languages = new string[1]
      {
        this.ParentLanguage
      };
      filter.Types = new MtgObjectType[1]
      {
        MtgObjectType.Exhibit
      };
      filter.Form = MtgObjectForm.Full;
      filter.Includes = ContentSection.None;
      filter.Excludes = ContentSection.All;
      MtgChildrenListResult childrenExtendedAsync = await MtgObjectServiceHelper.GetMtgObjectChildrenExtendedAsync(filter);
      if (childrenExtendedAsync == null || childrenExtendedAsync.Data == null || childrenExtendedAsync.Data.Length == 0)
      {
        this.IsEmpty = true;
        return SearchFlyoutResult.Empty;
      }
      MtgObject mtgObject1 = childrenExtendedAsync.Data[0];
      MtgObject mtgObject2 = new MtgObject();
      mtgObject2.Uid = this.ParentUid;
      mtgObject2.Type = this.ParentType;
      MtgObject mtgObjectParent = mtgObject2;
      this.ActivateInternal(mtgObjectParent, mtgObject1, ActivationTypeParameter.Numpad);
      this.IsBusy = false;
      this.NavigateInternal(mtgObject1, this.ParentUid);
      return new SearchFlyoutResult()
      {
        Success = true,
        MtgObject = mtgObject1,
        MtgObjectParent = mtgObjectParent
      };
    }

    protected override void OnSearchError(Exception ex) => this.IsEmpty = true;
  }
}
