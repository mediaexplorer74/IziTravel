// ********************************************************************
// Type: Izi.Travel.Mtg.Views.Common.QrCode.QrCodeScannerFlyoutView
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Mtg.Views.Common.QrCode
{
  public partial class QrCodeScannerFlyoutView : UserControl
  {
      public QrCodeScannerFlyoutView() => this.InitializeComponent();
      
      private void ManualEntryButton_Click(object sender, RoutedEventArgs e)
      {
          // Stub for ManualEntryButton_Click event handler
          Debug.WriteLine("ManualEntryButton_Click called");
      }
  }
}

