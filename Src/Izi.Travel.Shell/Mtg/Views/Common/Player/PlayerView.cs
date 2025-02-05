// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Views.Common.Player.PlayerView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Common.Player
{
  public class PlayerView : UserControl
  {
    internal ExponentialEase EaseIn;
    internal ProgressOverlay LayoutRoot;
    internal ExpandablePanel QuizPanel;
    internal CompositeTransform QuizPanelTransform;
    private bool _contentLoaded;

    public PlayerView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Views/Common/Player/PlayerView.xaml", UriKind.Relative));
      this.EaseIn = (ExponentialEase) this.FindName("EaseIn");
      this.LayoutRoot = (ProgressOverlay) this.FindName("LayoutRoot");
      this.QuizPanel = (ExpandablePanel) this.FindName("QuizPanel");
      this.QuizPanelTransform = (CompositeTransform) this.FindName("QuizPanelTransform");
    }
  }
}
