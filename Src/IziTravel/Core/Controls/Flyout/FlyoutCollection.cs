// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Flyout.FlyoutCollection
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Collections.Specialized;
using System.Linq;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Flyout
{
  public class FlyoutCollection : DependencyObjectCollection<FlyoutBase>
  {
    private readonly FrameworkElement _owner;

    public FlyoutCollection(FrameworkElement owner)
    {
      this._owner = owner;
      this.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems == null)
        return;
      foreach (FlyoutBase flyoutBase in e.NewItems.OfType<FlyoutBase>())
        flyoutBase.SetOwner(this._owner);
    }
  }
}
