// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.Views.Application.SettingsAppFeedbackView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;

#nullable disable
namespace Izi.Travel.Shell.Settings.Views.Application
{
  public class SettingsAppFeedbackView : PhoneApplicationPage
  {
    private bool _contentLoaded;

    public SettingsAppFeedbackView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      System.Windows.Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Settings/Views/Application/SettingsAppFeedbackView.xaml", UriKind.Relative));
    }
  }
}
