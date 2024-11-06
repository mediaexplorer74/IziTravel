// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.App
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Themes;
using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell
{
  public class App : Application
  {
    private bool _contentLoaded;

    public App()
    {
      this.InitializeComponent();
      ThemeHelper.OverrideSystemColors();
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/App.xaml", UriKind.Relative));
    }
  }
}
