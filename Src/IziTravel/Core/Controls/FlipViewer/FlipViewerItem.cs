// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewerItem
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.FlipViewer
{
  internal class FlipViewerItem
  {
    private FrameworkElement _rootFrameworkElement;
    private DataTemplate _dataTemplate;
    private Size _size;

    public int? RepresentingItemIndex { get; set; }

    public DataTemplate DataTemplate
    {
      get => this._dataTemplate;
      set
      {
        this._dataTemplate = value;
        this._rootFrameworkElement = (FrameworkElement) this._dataTemplate.LoadContent();
        this._rootFrameworkElement.Visibility = Visibility.Collapsed;
        this._rootFrameworkElement.Height = this._size.Height;
        this._rootFrameworkElement.Width = this._size.Width;
      }
    }

    public FrameworkElement RootFrameworkElement => this._rootFrameworkElement;

    public object DataContext
    {
      get => this._rootFrameworkElement.DataContext;
      set => this._rootFrameworkElement.DataContext = value;
    }

    public Size Size
    {
      get => this._size;
      set
      {
        this._size = value;
        this._rootFrameworkElement.Height = this._size.Height;
        this._rootFrameworkElement.Width = this._size.Width;
      }
    }

    public FlipViewerItem(Size size) => this._size = size;
  }
}
