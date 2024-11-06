// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Items.SettingsListItemBaseViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Core.Command;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Items
{
  public abstract class SettingsListItemBaseViewModel : PropertyChangedBase
  {
    private string _name;
    private string _info;
    private RelayCommand _selectCommand;

    protected ISettingsService SettingsService => ServiceFacade.SettingsService;

    public string Name
    {
      get => this._name;
      set
      {
        if (!(this._name != value))
          return;
        this._name = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Name));
      }
    }

    public string Info
    {
      get => this._info;
      set
      {
        if (!(this._info != value))
          return;
        this._info = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Info));
      }
    }

    protected SettingsListItemBaseViewModel(string name, string info)
    {
      this._name = name;
      this._info = info;
    }

    public RelayCommand SelectCommand
    {
      get
      {
        return this._selectCommand ?? (this._selectCommand = new RelayCommand(new Action<object>(this.ExecuteSelectCommand), new Func<object, bool>(this.CanExecuteSelectCommand)));
      }
    }

    protected virtual bool CanExecuteSelectCommand(object parameter) => true;

    protected virtual void ExecuteSelectCommand(object parameter)
    {
    }
  }
}
