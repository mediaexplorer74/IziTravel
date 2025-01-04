// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Controls.ImagesFlipView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Media.Provider;
using Izi.Travel.Shell.Media.ViewModels;
using Izi.Travel.Shell.Toolkit.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Controls
{
  public class ImagesFlipView : UserControl
  {
    public static readonly DependencyProperty MtgObjectProperty = DependencyProperty.Register(nameof (MtgObject), typeof (MtgObject), typeof (ImagesFlipView), new PropertyMetadata((object) null, new PropertyChangedCallback(ImagesFlipView.MtgObjectPropertyChangedCallback)));
    private List<ImagesFlipViewItem> _images;
    internal Grid LayoutRoot;
    internal FlipView FlipView;
    internal ItemsControl ItemsControl;
    private bool _contentLoaded;

    public MtgObject MtgObject
    {
      get => (MtgObject) this.GetValue(ImagesFlipView.MtgObjectProperty);
      set => this.SetValue(ImagesFlipView.MtgObjectProperty, (object) value);
    }

    public ImagesFlipView() => this.InitializeComponent();

    private static void MtgObjectPropertyChangedCallback(
      DependencyObject a,
      DependencyPropertyChangedEventArgs b)
    {
      ImagesFlipView imagesFlipView = a as ImagesFlipView;
      MtgObject mtgObject = b.NewValue as MtgObject;
      if (imagesFlipView == null || mtgObject == null || mtgObject.ContentProvider == null || mtgObject.MainContent == null || mtgObject.MainContent.Images == null)
        return;
      List<ImagesFlipViewItem> list1 = ((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) mtgObject.MainContent.Images).Where<Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Business.Entities.Data.Media, bool>) (x => x.Type == MediaType.Story)).Select<Izi.Travel.Business.Entities.Data.Media, ImagesFlipViewItem>((Func<Izi.Travel.Business.Entities.Data.Media, ImagesFlipViewItem>) (x => new ImagesFlipViewItem()
      {
        Uid = x.Uid,
        Title = x.Title,
        PreviewUrl = ServiceFacade.MediaService.GetImageUrl(x.Uid, mtgObject.ContentProvider.Uid, ImageFormat.Low480X360),
        ImageUrl = ServiceFacade.MediaService.GetImageUrl(x.Uid, mtgObject.ContentProvider.Uid, ImageFormat.High800X600)
      })).ToList<ImagesFlipViewItem>();
      List<ImagesFlipViewItem> list2 = list1.Take<ImagesFlipViewItem>(5).ToList<ImagesFlipViewItem>();
      imagesFlipView._images = list1;
      imagesFlipView.FlipView.ItemsSource = (IEnumerable) list2;
      imagesFlipView.ItemsControl.ItemsSource = (IEnumerable) list2;
      imagesFlipView.ItemsControl.Visibility = (list1.Count > 1).ToVisibility();
    }

    private void FlipView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      ImagesFlipViewItem removedItem = e.RemovedItems.Count > 0 ? e.RemovedItems[0] as ImagesFlipViewItem : (ImagesFlipViewItem) null;
      if (removedItem != null)
        removedItem.IsSelected = false;
      ImagesFlipViewItem addedItem = e.AddedItems.Count > 0 ? e.AddedItems[0] as ImagesFlipViewItem : (ImagesFlipViewItem) null;
      if (addedItem == null)
        return;
      addedItem.IsSelected = true;
    }

    private void Image_OnTap(object sender, GestureEventArgs e)
    {
      if (!(this.FlipView.SelectedItem is ImagesFlipViewItem selectedItem))
        return;
      MediaPlayerDataProvider.Instance.MediaData = this._images.Select<ImagesFlipViewItem, MediaInfo>((Func<ImagesFlipViewItem, MediaInfo>) (x => new MediaInfo()
      {
        MediaFormat = MediaFormat.Image,
        MediaUid = x.Uid,
        Title = x.Title,
        ImageUrl = x.ImageUrl,
        PreviewUrl = x.PreviewUrl
      })).ToArray<MediaInfo>();
      MediaPlayerDataProvider.Instance.MediaDataUid = selectedItem.Uid;
      ShellServiceFacade.NavigationService.UriFor<MediaPlayerPartViewModel>().WithParam<MediaFormat>((Expression<Func<MediaPlayerPartViewModel, MediaFormat>>) (x => x.MediaFormat), MediaFormat.Image).Navigate();
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Controls/ImagesFlipView.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) this.FindName("LayoutRoot");
      this.FlipView = (FlipView) this.FindName("FlipView");
      this.ItemsControl = (ItemsControl) this.FindName("ItemsControl");
    }
  }
}
