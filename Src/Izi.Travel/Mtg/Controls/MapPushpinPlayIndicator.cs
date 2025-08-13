// ********************************************************************
// Type: Izi.Travel.Mtg.Controls.MapPushpinPlayIndicator
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Mtg.Controls
{
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Active", GroupName = "CommonStates")]
  public class MapPushpinPlayIndicator : Control
  {
    private const string VisualStateGroupCommonStates = "CommonStates";
    private const string VisualStateNormal = "Normal";
    private const string VisualStateActive = "Active";
    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(nameof (IsActive), typeof (bool), typeof (MapPushpinPlayIndicator), new PropertyMetadata((object) false, new PropertyChangedCallback(MapPushpinPlayIndicator.OnIsActivePropertyChanged)));

    public bool IsActive
    {
      get => (bool) this.GetValue(MapPushpinPlayIndicator.IsActiveProperty);
      set => this.SetValue(MapPushpinPlayIndicator.IsActiveProperty, (object) value);
    }

    public MapPushpinPlayIndicator()
    {
      this.DefaultStyleKey = (object) typeof (MapPushpinPlayIndicator);
    }

    protected override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.SetContentVisualState(false);
    }

    private void SetContentVisualState(bool transition)
    {
      VisualStateManager.GoToState((Control) this, this.IsActive ? "Active" : "Normal", transition);
    }

    private static void OnIsActivePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is MapPushpinPlayIndicator pushpinPlayIndicator))
        return;
      pushpinPlayIndicator.SetContentVisualState(true);
    }
  }
}

