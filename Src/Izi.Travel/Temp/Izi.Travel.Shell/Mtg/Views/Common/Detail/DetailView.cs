// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Views.Common.Detail.DetailView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Utility;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Common.Detail
{
  public class DetailView : UserControl
  {
    private bool _contentLoaded;

    public DetailView()
    {
      Counter.Construct("D:\\TeamCity\\buildAgent\\work\\961976af89b2d6d9\\src\\Izi.Travel.Shell\\Mtg\\Views\\Common\\Detail\\DetailView.xaml.cs");
      this.InitializeComponent();
    }

    ~DetailView()
    {
      Counter.Destruct("D:\\TeamCity\\buildAgent\\work\\961976af89b2d6d9\\src\\Izi.Travel.Shell\\Mtg\\Views\\Common\\Detail\\DetailView.xaml.cs");
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Views/Common/Detail/DetailView.xaml", UriKind.Relative));
    }
  }
}
