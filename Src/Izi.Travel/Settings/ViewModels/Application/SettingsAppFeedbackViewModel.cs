// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Application.SettingsAppFeedbackViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Helper;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Settings.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Application
{
  public class SettingsAppFeedbackViewModel : Screen
  {
    private bool _isBusy;
    private string _email;
    private string _subject;
    private string _message;
    private bool _emailIsFocused;
    private bool _subjectIsFocused;
    private RelayCommand _focusSubjectCommand;
    private RelayCommand _sendCommand;

    public bool IsBusy
    {
      get => this._isBusy;
      set => this.SetProperty<bool>(ref this._isBusy, value, propertyName: nameof (IsBusy));
    }

    public string Email
    {
      get => this._email;
      set => this.SetProperty<string>(ref this._email, value, propertyName: nameof (Email));
    }

    public string Subject
    {
      get => this._subject;
      set => this.SetProperty<string>(ref this._subject, value, propertyName: nameof (Subject));
    }

    public string Message
    {
      get => this._message;
      set => this.SetProperty<string>(ref this._message, value, propertyName: nameof (Message));
    }

    public bool EmailIsFocused
    {
      get => this._emailIsFocused;
      set
      {
        this.SetProperty<bool>(ref this._emailIsFocused, value, propertyName: nameof (EmailIsFocused));
      }
    }

    public bool SubjectIsFocused
    {
      get => this._subjectIsFocused;
      set
      {
        this.SetProperty<bool>(ref this._subjectIsFocused, value, propertyName: nameof (SubjectIsFocused));
      }
    }

    public RelayCommand FocusSubjectCommand
    {
      get
      {
        return this._focusSubjectCommand ?? (this._focusSubjectCommand = new RelayCommand((Action<object>) (x => this.FocusSubject())));
      }
    }

    public RelayCommand SendCommand
    {
      get
      {
        return this._sendCommand ?? (this._sendCommand = new RelayCommand(new Action<object>(this.Send), new Func<object, bool>(this.CanSend)));
      }
    }

    private bool CanSend(object o) => !this.IsBusy;

    private async void Send(object o)
    {
      if (string.IsNullOrWhiteSpace(this.Email))
        this.FocusEmail();
      else if (string.IsNullOrWhiteSpace(this.Subject))
      {
        this.FocusSubject();
      }
      else
      {
        this.IsBusy = true;
        if (System.Windows.Application.Current.RootVisual is Control rootVisual)
          rootVisual.Focus();
        bool flag = !string.IsNullOrWhiteSpace(this.Email) && !string.IsNullOrWhiteSpace(this.Subject) && !string.IsNullOrWhiteSpace(this.Message);
        if (flag)
          flag = await UserVoiceHelper.Post(this.Email, this.Subject, this.Message);
        if (flag)
        {
          AnalyticsHelper.SendShareAndFollowUs(ShareAndFollowUsParameter.Feedback);
          ShellServiceFacade.DialogService.Show(AppResources.SendFeedbackSuccessTitle, AppResources.SendFeedbackSuccessMessage, MessageBoxButtonContent.Ok, (Action<FlyoutDialog>) null, (Action<FlyoutDialog, MessageBoxResult>) ((x, y) =>
          {
            IoC.Get<INavigationService>().RemoveBackEntry();
            IoC.Get<INavigationService>().GoBack();
          }));
        }
        else
          ShellServiceFacade.DialogService.Show(AppResources.SendFeedbackFailureTitle, AppResources.SendFeedbackFailureMessage, MessageBoxButtonContent.Ok, (Action<FlyoutDialog>) null, (Action<FlyoutDialog, MessageBoxResult>) null);
        this.IsBusy = false;
      }
    }

    private void FocusEmail()
    {
      this.EmailIsFocused = true;
      this.EmailIsFocused = false;
    }

    private void FocusSubject()
    {
      this.SubjectIsFocused = true;
      this.SubjectIsFocused = false;
    }
  }
}
