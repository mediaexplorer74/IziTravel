// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Application.SettingsAppLanguageListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Culture;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Application
{
  public class SettingsAppLanguageListItemViewModel : PropertyChangedBase
  {
    private bool _isSelected;
    private bool _isDefault;
    private readonly LanguageData _languageData;

    public bool IsSelected
    {
      get => this._isSelected;
      set => this.SetProperty<bool>(ref this._isSelected, value, propertyName: nameof (IsSelected));
    }

    public bool IsDefault
    {
      get => this._isDefault;
      set
      {
        this.SetProperty<bool, string>(ref this._isDefault, value, (Expression<Func<string>>) (() => this.IsDefaultString), propertyName: nameof (IsDefault));
      }
    }

    public string Code => this._languageData == null ? string.Empty : this._languageData.Code;

    public string NativeName
    {
      get => this._languageData == null ? string.Empty : this._languageData.NativeName;
    }

    public string EnglishName
    {
      get => this._languageData == null ? string.Empty : this._languageData.EnglishName;
    }

    public string IsDefaultString
    {
      get => !this.IsDefault ? (string) null : "(" + AppResources.LabelDefault.ToLower() + ")";
    }

    public SettingsAppLanguageListItemViewModel(LanguageData langaugeData)
    {
      this._languageData = langaugeData;
    }
  }
}
