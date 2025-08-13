// ********************************************************************
// Type: Izi.Travel.Settings.ViewModels.Application.SettingsListItemCodeNameViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Settings.ViewModels.Items;

#nullable disable
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Izi.Travel.Core.Command;

namespace Izi.Travel.Settings.ViewModels.Application
{
  public class SettingsListItemCodeNameViewModel : SettingsListItemBaseViewModel
  {
    private SettingsViewModel SettingsViewModel { get; set; }

    public SettingsListItemCodeNameViewModel(
      SettingsViewModel settingsViewModel,
      string name,
      string info)
      : base(name, info)
    {
      this.SettingsViewModel = settingsViewModel;
    }

    protected override async Task ExecuteSelectCommandAsync(object parameter)
    {
        try
        {
            var command = SettingsViewModel?.PasscodeFlyoutViewModel?.OpenCommand;
            if (command == null) return;

            if (command is IAsyncCommand asyncCommand)
            {
                await asyncCommand.ExecuteAsync(parameter);
            }
            else if (command is ICommand syncCommand && syncCommand.CanExecute(parameter))
            {
                // For synchronous commands, we'll run them on a background thread
                // to avoid blocking the UI thread
                await Task.Run(() => syncCommand.Execute(parameter));
            }
        }
        catch (Exception ex)
        {
            // Log error or handle it appropriately
            System.Diagnostics.Debug.WriteLine($"Error executing command: {ex}");
            throw;
        }
    }
  }
}
