// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Flyouts.FlyoutLanguageItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Extensions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Flyouts
{
  public class FlyoutLanguageItemViewModel : PropertyChangedBase
  {
    private string _name;
    private string _code;
    private string _title;
    private bool _isSelected;

    public string Name
    {
      get => this._name;
      set => this.SetProperty<string>(ref this._name, value, propertyName: nameof (Name));
    }

    public string Code
    {
      get => this._code;
      set => this.SetProperty<string>(ref this._code, value, propertyName: nameof (Code));
    }

    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, propertyName: nameof (Title));
    }

    public bool IsSelected
    {
      get => this._isSelected;
      set => this.SetProperty<bool>(ref this._isSelected, value, propertyName: nameof (IsSelected));
    }

    public override string ToString() => this.Title;
  }
}
