// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.ProgressOverlay
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Busy", GroupName = "CommonStates")]
  public class ProgressOverlay : ContentControl
  {
    private const string VisualGroupCommonStates = "CommonStates";
    private const string VisualStateNormal = "Normal";
    private const string VisualStateBusy = "Busy";
    public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(nameof (IsBusy), typeof (bool), typeof (ProgressOverlay), new PropertyMetadata((object) false, new PropertyChangedCallback(ProgressOverlay.OnIsBusyPropertyChanged)));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (ProgressOverlay), new PropertyMetadata((object) AppResources.ActionLoading));
    public static readonly DependencyProperty ProgressBrushProperty = DependencyProperty.Register(nameof (ProgressBrush), typeof (Brush), typeof (ProgressOverlay), new PropertyMetadata((object) null));

    public bool IsBusy
    {
      get => (bool) this.GetValue(ProgressOverlay.IsBusyProperty);
      set => this.SetValue(ProgressOverlay.IsBusyProperty, (object) value);
    }

    public string Title
    {
      get => (string) this.GetValue(ProgressOverlay.TitleProperty);
      set => this.SetValue(ProgressOverlay.TitleProperty, (object) value);
    }

    public Brush ProgressBrush
    {
      get => (Brush) this.GetValue(ProgressOverlay.ProgressBrushProperty);
      set => this.SetValue(ProgressOverlay.ProgressBrushProperty, (object) value);
    }

    public ProgressOverlay() => this.DefaultStyleKey = (object) typeof (ProgressOverlay);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.SetVisualState(false);
    }

    private void SetVisualState(bool useTransition)
    {
      VisualStateManager.GoToState((Control) this, this.IsBusy ? "Busy" : "Normal", useTransition);
    }

    private static void OnIsBusyPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ProgressOverlay progressOverlay))
        return;
      progressOverlay.SetVisualState(true);
    }
  }
}
