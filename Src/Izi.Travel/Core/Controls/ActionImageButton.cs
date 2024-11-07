// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.ActionImageButton
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
  public class ActionImageButton : Button
  {
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (ActionImageButton), new PropertyMetadata((object) null));

    public ImageSource ImageSource
    {
      get => (ImageSource) this.GetValue(ActionImageButton.ImageSourceProperty);
      set => this.SetValue(ActionImageButton.ImageSourceProperty, (object) value);
    }

    public ActionImageButton() => this.DefaultStyleKey = (object) typeof (ActionImageButton);
  }
}
