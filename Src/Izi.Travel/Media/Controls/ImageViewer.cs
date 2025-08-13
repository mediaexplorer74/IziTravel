// ********************************************************************
// Type: Izi.Travel.Media.Controls.ImageViewer
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace Izi.Travel.Media.Controls
{
  [TemplatePart(Name = "PartCanvas", Type = typeof (Canvas))]
  [TemplatePart(Name = "PartImage", Type = typeof (Image))]
  public class ImageViewer : Control
  {
    private const string PartViewport = "PartViewport";
    private const string PartCanvas = "PartCanvas";
    private const string PartImage = "PartImage";
    private Canvas _canvas;
    private Image _image;
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof (Source), typeof (ImageSource), typeof (ImageViewer), new PropertyMetadata((object) null));

    public ImageSource Source
    {
      get => (ImageSource) this.GetValue(ImageViewer.SourceProperty);
      set => this.SetValue(ImageViewer.SourceProperty, (object) value);
    }

    public ImageViewer() => this.DefaultStyleKey = (object) typeof (ImageViewer);

    protected override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._canvas = this.GetTemplateChild("PartCanvas") as Canvas;
      this._image = this.GetTemplateChild("PartImage") as Image;
      if (this._image != null)
      {
        this._image.Source = this.Source as ImageSource;
      }
    }

  }
}

