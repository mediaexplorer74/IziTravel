// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Views.Common.ReviewListPartView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Common
{
  public class ReviewListPartView : PhoneApplicationPage
  {
    internal ContentControl ActiveItem;
    private bool _contentLoaded;

    public ReviewListPartView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Views/Common/ReviewListPartView.xaml", UriKind.Relative));
      this.ActiveItem = (ContentControl) this.FindName("ActiveItem");
    }
  }
}
