// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Application.SettingsAppFeedbackMessageViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Services;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Application
{
  public class SettingsAppFeedbackMessageViewModel : Screen
  {
    private string _message;
    private bool _messageIsFocused;
    private RelayCommand _navigateCommand;

    public string Message
    {
      get => this._message;
      set => this.SetProperty<string>(ref this._message, value, propertyName: nameof (Message));
    }

    public bool MessageIsFocused
    {
      get => this._messageIsFocused;
      set
      {
        this.SetProperty<bool>(ref this._messageIsFocused, value, propertyName: nameof (MessageIsFocused));
      }
    }

    public RelayCommand NavigateCommand
    {
      get
      {
        return this._navigateCommand ?? (this._navigateCommand = new RelayCommand(new Action<object>(this.Navigate)));
      }
    }

    private void Navigate(object o)
    {
      if (string.IsNullOrWhiteSpace(this.Message))
        this.FocusMessage();
      else
        ShellServiceFacade.NavigationService.UriFor<SettingsAppFeedbackViewModel>().WithParam<string>((Expression<Func<SettingsAppFeedbackViewModel, string>>) (x => x.Message), this.Message).Navigate();
    }

    private void FocusMessage()
    {
      this.MessageIsFocused = true;
      this.MessageIsFocused = false;
    }
  }
}
