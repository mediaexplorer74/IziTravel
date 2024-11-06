// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Tiles.BaseTileControl
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Tiles
{
  public abstract class BaseTileControl : Control
  {
    internal int StallingCounter;
    private static ITileService _tileService;
    public static readonly DependencyProperty IsFrozenProperty = DependencyProperty.Register(nameof (IsFrozen), typeof (bool), typeof (BaseTileControl), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseTileControl.OnIsFrozenPropertyChanged)));

    public bool IsFrozen
    {
      get => (bool) this.GetValue(BaseTileControl.IsFrozenProperty);
      set => this.SetValue(BaseTileControl.IsFrozenProperty, (object) value);
    }

    protected BaseTileControl()
    {
      this.InitializeService();
      this.Loaded += new RoutedEventHandler(this.OnLoaded);
      this.Unloaded += new RoutedEventHandler(this.OnUnloaded);
    }

    private void InitializeService()
    {
      if (BaseTileControl._tileService != null)
        return;
      BaseTileControl._tileService = this.CreateTileService();
    }

    protected abstract ITileService CreateTileService();

    private void OnLoaded(object sender, RoutedEventArgs args)
    {
      BaseTileControl._tileService.Initialize(this);
    }

    private void OnUnloaded(object sender, RoutedEventArgs args)
    {
      BaseTileControl._tileService.Deinitialize(this);
    }

    private static void OnIsFrozenPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is BaseTileControl control))
        return;
      if ((bool) e.NewValue)
        BaseTileControl._tileService.FreezeTile(control);
      else
        BaseTileControl._tileService.UnfreezeTile(control);
    }
  }
}
