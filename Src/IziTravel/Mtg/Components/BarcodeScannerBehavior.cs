// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Components.BarcodeScannerBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Mtg.Controls;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Components
{
  public class BarcodeScannerBehavior : Behavior<BarcodeScanner>
  {
    public static readonly DependencyProperty BarcodeScannedCommandProperty = DependencyProperty.Register(nameof (BarcodeScannedCommand), typeof (ICommand), typeof (BarcodeScannerBehavior), new PropertyMetadata((object) null));

    public ICommand BarcodeScannedCommand
    {
      get => (ICommand) this.GetValue(BarcodeScannerBehavior.BarcodeScannedCommandProperty);
      set => this.SetValue(BarcodeScannerBehavior.BarcodeScannedCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.BarcodeScanned += new EventHandler<BarcodeScannerEventArgs>(this.OnBarcodeScanned);
      base.OnAttached();
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.BarcodeScanned -= new EventHandler<BarcodeScannerEventArgs>(this.OnBarcodeScanned);
      base.OnDetaching();
    }

    private void OnBarcodeScanned(object sender, BarcodeScannerEventArgs args)
    {
      if (this.BarcodeScannedCommand == null || !this.BarcodeScannedCommand.CanExecute((object) args.Data))
        return;
      this.BarcodeScannedCommand.Execute((object) args.Data);
    }
  }
}
