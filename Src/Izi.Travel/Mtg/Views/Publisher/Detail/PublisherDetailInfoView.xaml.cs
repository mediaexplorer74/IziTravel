// ********************************************************************
// Type: Izi.Travel.Mtg.Views.Publisher.Detail.PublisherDetailInfoView
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace Izi.Travel.Mtg.Views.Publisher.Detail
{
  public partial class PublisherDetailInfoView : UserControl
  {
      // Default placeholder image for when loading fails
      private static readonly BitmapImage DefaultPlaceholderImage = new BitmapImage(new Uri("ms-appx:///Assets/Placeholder.png"));

      public PublisherDetailInfoView()
      {
          this.InitializeComponent();
      }

      private void Image_Loaded(object sender, RoutedEventArgs e)
      {
          // Image loaded successfully
          if (sender is Image image)
          {
              // You can add any additional logic here when an image loads successfully
          }
      }

      private void Image_Failed(object sender, ExceptionRoutedEventArgs e)
      {
          // Handle image loading failure
          if (sender is Image image)
          {
              // Set a default placeholder image when loading fails
              image.Source = DefaultPlaceholderImage;
          }
      }

      private void UserControl_Loaded(object sender, RoutedEventArgs e)
      {
          // Additional initialization when the user control is loaded
      }
  }
}
