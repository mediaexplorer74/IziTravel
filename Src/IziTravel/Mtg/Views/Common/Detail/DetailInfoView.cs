// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Views.Common.Detail.DetailInfoView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Toolkit.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Common.Detail
{
  public class DetailInfoView : UserControl
  {
    internal UserControl PartDetailInfoView;
    internal FlipView FlipViewImage;
    internal ItemsControl PartSiteList;
    private bool _contentLoaded;

    public DetailInfoView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Views/Common/Detail/DetailInfoView.xaml", UriKind.Relative));
      this.PartDetailInfoView = (UserControl) this.FindName("PartDetailInfoView");
      this.FlipViewImage = (FlipView) this.FindName("FlipViewImage");
      this.PartSiteList = (ItemsControl) this.FindName("PartSiteList");
    }
  }
}
