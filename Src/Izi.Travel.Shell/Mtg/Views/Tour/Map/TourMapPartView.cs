// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Views.Tour.Map.TourMapPartView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Utility;
using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Tour.Map
{
  public class TourMapPartView : PhoneApplicationPage
  {
    internal Style MapButton;
    private bool _contentLoaded;

    public TourMapPartView()
    {
      Counter.Construct("D:\\TeamCity\\buildAgent\\work\\961976af89b2d6d9\\src\\Izi.Travel.Shell\\Mtg\\Views\\Tour\\Map\\TourMapPartView.xaml.cs");
      this.InitializeComponent();
    }

    ~TourMapPartView()
    {
      Counter.Destruct("D:\\TeamCity\\buildAgent\\work\\961976af89b2d6d9\\src\\Izi.Travel.Shell\\Mtg\\Views\\Tour\\Map\\TourMapPartView.xaml.cs");
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Views/Tour/Map/TourMapPartView.xaml", UriKind.Relative));
      this.MapButton = (Style) this.FindName("MapButton");
    }
  }
}
