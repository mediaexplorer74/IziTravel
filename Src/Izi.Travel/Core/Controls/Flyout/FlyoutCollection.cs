// ********************************************************************
// Type: Izi.Travel.Shell.Core.Controls.Flyout.FlyoutCollection
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Collections.Specialized;
using System.Linq;
using Windows.UI.Xaml;
#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Flyout
{
  public class FlyoutCollection : DependencyObjectCollection
  {
    private readonly FrameworkElement _owner;

    public FlyoutCollection(FrameworkElement owner)
    {
      this._owner = owner;
      this.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
    }

        public NotifyCollectionChangedEventHandler CollectionChanged { get; private set; }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems == null)
        return;
      foreach (var item in e.NewItems)
      {
        var flyoutBase = item as FlyoutBase;
        if (flyoutBase != null)
          flyoutBase.SetOwner(this._owner);
      }
    }
  }
}

