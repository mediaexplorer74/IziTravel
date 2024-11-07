// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.Controls.ImagePlaceholder
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Common.Controls
{
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Busy", GroupName = "CommonStates")]
  [TemplatePart(Name = "PartImage", Type = typeof (Image))]
  public class ImagePlaceholder : Control
  {
    private const string VisualGroupCommonStates = "CommonStates";
    private const string VisualStateNormal = "Normal";
    private const string VisualStateBusy = "Busy";
    private const string PartImage = "PartImage";
    private Image _image;
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (ImagePlaceholder), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ImageStretchProperty = DependencyProperty.Register(nameof (ImageStretch), typeof (Stretch), typeof (ImagePlaceholder), new PropertyMetadata((object) Stretch.None));

    public ImageSource ImageSource
    {
      get => (ImageSource) this.GetValue(ImagePlaceholder.ImageSourceProperty);
      set => this.SetValue(ImagePlaceholder.ImageSourceProperty, (object) value);
    }

    public Stretch ImageStretch
    {
      get => (Stretch) this.GetValue(ImagePlaceholder.ImageStretchProperty);
      set => this.SetValue(ImagePlaceholder.ImageStretchProperty, (object) value);
    }

    public ImagePlaceholder() => this.DefaultStyleKey = (object) typeof (ImagePlaceholder);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.SetVisualState(false, true);
      this._image = this.GetTemplateChild("PartImage") as Image;
      if (this._image == null)
        return;
      this._image.ImageOpened += new EventHandler<RoutedEventArgs>(this.OnImageOpened);
    }

    private void SetVisualState(bool useTransition, bool isBusy)
    {
      VisualStateManager.GoToState((Control) this, isBusy ? "Busy" : "Normal", useTransition);
    }

    private void OnImageOpened(object sender, RoutedEventArgs e)
    {
      this.SetVisualState(true, false);
    }
  }
}
