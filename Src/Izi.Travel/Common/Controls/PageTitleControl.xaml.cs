// ********************************************************************
// Type: Izi.Travel.Shell.Common.Controls.PageTitleControl
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Shell.Common.Controls
{
  public partial class PageTitleControl : UserControl
  {
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (PageTitleControl), new PropertyMetadata((object) null));
    public static readonly DependencyProperty SubTitleProperty = DependencyProperty.Register(nameof (SubTitle), typeof (string), typeof (PageTitleControl), new PropertyMetadata((object) null));
    internal UserControl PartPageTitleControl;
    private bool _contentLoaded;

    public string Title
    {
      get => (string) this.GetValue(PageTitleControl.TitleProperty);
      set => this.SetValue(PageTitleControl.TitleProperty, (object) value);
    }

    public string SubTitle
    {
      get => (string) this.GetValue(PageTitleControl.SubTitleProperty);
      set => this.SetValue(PageTitleControl.SubTitleProperty, (object) value);
    }

    public PageTitleControl() => this.InitializeComponent();

    
  }
}
