// ********************************************************************
// Type: Izi.Travel.Settings.ViewModels.Items.SettingsListItemBaseViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Core.Command;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Settings.ViewModels.Items
{
  public abstract class SettingsListItemBaseViewModel : PropertyChangedBase
  {
    private string _name;
    private string _info;
    private ICommand _selectCommand;

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

    public ICommand SelectCommand
    {
      get
      {
        return _selectCommand ??= new RelayCommand(
            async param => await ExecuteSelectCommandAsync(param),
            param => CanExecuteSelectCommand(param));
      }
    }

    protected virtual bool CanExecuteSelectCommand(object parameter) => true;

    protected virtual async Task ExecuteSelectCommandAsync(object parameter)
    {
        // Default implementation does nothing
        await Task.CompletedTask;
    }
    
    [Obsolete("Use ExecuteSelectCommandAsync instead")]
    protected virtual void ExecuteSelectCommand(object parameter)
    {
        // For backward compatibility only
        ExecuteSelectCommandAsync(parameter).GetAwaiter().GetResult();
    }
  }
}
