// ********************************************************************
// Type: Izi.Travel.Mtg.Views.Publisher.Detail.PublisherDetailContentView
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Izi.Travel.Common.ViewModels.List;
using Izi.Travel.Mtg.ViewModels.Publisher.Detail;

#nullable disable
namespace Izi.Travel.Mtg.Views.Publisher.Detail
{
  public partial class PublisherDetailContentView : UserControl
  {
    // Default placeholder image for when loading fails
    private static readonly BitmapImage DefaultPlaceholderImage = new BitmapImage(new Uri("ms-appx:///Assets/Placeholder.png"));
    
    public PublisherDetailContentView()
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

    private void ListView_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
    {
        var listView = sender as ListView;
        var viewModel = listView?.DataContext as IListViewModelBase;
        
        if (viewModel != null && viewModel.NavigateCommand != null && viewModel.NavigateCommand.CanExecute(e.ClickedItem))
        {
            viewModel.NavigateCommand.Execute(e.ClickedItem);
            return;
        }
        
        var detailViewModel = DataContext as IListViewModelBase;
        if (detailViewModel?.ActiveItem != null && detailViewModel.ActiveItem is IListViewModelBase activeItemViewModel)
        {
            if (activeItemViewModel.NavigateCommand != null && activeItemViewModel.NavigateCommand.CanExecute(e.ClickedItem))
            {
                activeItemViewModel.NavigateCommand.Execute(e.ClickedItem);
            }
        }
    }
}
}

