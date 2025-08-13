// ********************************************************************
// Type: Izi.Travel.Core.Controls.Flyout.FlyoutCollection
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Izi.Travel.Core.Controls.Flyout
{
  /// <summary>
  /// A collection of Flyout controls
  /// </summary>
  public class FlyoutCollection : ObservableCollection<Windows.UI.Xaml.Controls.Flyout>
  {
    private readonly FrameworkElement _owner;

    public FlyoutCollection(FrameworkElement owner)
    {
      _owner = owner;
      CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems == null)
        return;
        
      foreach (var item in e.NewItems)
      {
        if (item is Windows.UI.Xaml.Controls.Flyout flyout)
        {
          // In UWP, Flyout is automatically associated with its owner
          // via the FlyoutBase.SetAttachedFlyout method or through XAML
          FlyoutBase.SetAttachedFlyout(_owner, flyout);
        }
      }
    }
  }
}
