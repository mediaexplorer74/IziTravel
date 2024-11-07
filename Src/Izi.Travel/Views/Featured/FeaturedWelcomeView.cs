// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Views.Featured.FeaturedWelcomeView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Views.Featured
{
  public class FeaturedWelcomeView : UserControl
  {
    private bool _isLoading;
    public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(nameof (IsLoading), typeof (bool), typeof (FeaturedWelcomeView), new PropertyMetadata((object) false, new PropertyChangedCallback(FeaturedWelcomeView.OnIsLoadingPropertyChanged)));
    internal UserControl PartFeaturedWelcomeView;
    internal VisualStateGroup Featured;
    internal VisualState Normal;
    internal VisualState Loading;
    internal Image PartImage;
    internal CompositeTransform PartImageTransform;
    internal Image PartLogo;
    internal CompositeTransform PartLogoTransform;
    internal TextBlock PartInfo;
    internal CompositeTransform PartInfoTransform;
    internal StackPanel PartProgress;
    internal Button PartButtonStart;
    internal CompositeTransform PartButtonStartTransform;
    private bool _contentLoaded;

    public bool IsLoading
    {
      get => (bool) this.GetValue(FeaturedWelcomeView.IsLoadingProperty);
      set => this.SetValue(FeaturedWelcomeView.IsLoadingProperty, (object) value);
    }

    public FeaturedWelcomeView()
    {
      this.InitializeComponent();
      VisualStateManager.GoToState((Control) this, nameof (Normal), false);
    }

    private void OnStoryboardLoadingCompleted(object sender, EventArgs e)
    {
      this._isLoading = false;
      if (this.IsLoading)
        return;
      VisualStateManager.GoToState((Control) this, "Normal", true);
    }

    private static void OnIsLoadingPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is FeaturedWelcomeView featuredWelcomeView))
        return;
      if ((bool) e.NewValue)
      {
        featuredWelcomeView._isLoading = true;
        VisualStateManager.GoToState((Control) (d as FeaturedWelcomeView), "Loading", true);
      }
      else
      {
        if (featuredWelcomeView._isLoading)
          return;
        VisualStateManager.GoToState((Control) (d as FeaturedWelcomeView), "Normal", true);
      }
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Views/Featured/FeaturedWelcomeView.xaml", UriKind.Relative));
      this.PartFeaturedWelcomeView = (UserControl) this.FindName("PartFeaturedWelcomeView");
      this.Featured = (VisualStateGroup) this.FindName("Featured");
      this.Normal = (VisualState) this.FindName("Normal");
      this.Loading = (VisualState) this.FindName("Loading");
      this.PartImage = (Image) this.FindName("PartImage");
      this.PartImageTransform = (CompositeTransform) this.FindName("PartImageTransform");
      this.PartLogo = (Image) this.FindName("PartLogo");
      this.PartLogoTransform = (CompositeTransform) this.FindName("PartLogoTransform");
      this.PartInfo = (TextBlock) this.FindName("PartInfo");
      this.PartInfoTransform = (CompositeTransform) this.FindName("PartInfoTransform");
      this.PartProgress = (StackPanel) this.FindName("PartProgress");
      this.PartButtonStart = (Button) this.FindName("PartButtonStart");
      this.PartButtonStartTransform = (CompositeTransform) this.FindName("PartButtonStartTransform");
    }
  }
}
