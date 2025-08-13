// ********************************************************************
// Type: Izi.Travel.Views.Featured.FeaturedWelcomeView
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Media;

#nullable disable
namespace Izi.Travel.Views.Featured
{
  public partial class FeaturedWelcomeView : UserControl
  {
    private bool _isLoading;
    public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(nameof (IsLoading), 
        typeof (bool), typeof (FeaturedWelcomeView), new PropertyMetadata((object) false, 
            new PropertyChangedCallback(FeaturedWelcomeView.OnIsLoadingPropertyChanged)));
   

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

    private void OnStoryboardLoadingCompleted(object sender, object e)
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

    
  }
}

