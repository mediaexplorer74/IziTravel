// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.Views.Application.SettingsAppLanguageView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Settings.Views.Application
{
  public class SettingsAppLanguageView : PhoneApplicationPage
  {
    internal ListBox LanguageListBox;
    private bool _contentLoaded;

    public SettingsAppLanguageView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      System.Windows.Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Settings/Views/Application/SettingsAppLanguageView.xaml", UriKind.Relative));
      this.LanguageListBox = (ListBox) this.FindName("LanguageListBox");
    }
  }
}
