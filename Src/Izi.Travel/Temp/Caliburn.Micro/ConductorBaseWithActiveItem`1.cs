// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ConductorBaseWithActiveItem`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.ComponentModel;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A base class for various implementations of <see cref="T:Caliburn.Micro.IConductor" /> that maintain an active item.
  /// </summary>
  /// <typeparam name="T">The type that is being conducted.</typeparam>
  public abstract class ConductorBaseWithActiveItem<T> : 
    ConductorBase<T>,
    IConductActiveItem,
    IConductor,
    IParent,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged,
    IHaveActiveItem
    where T : class
  {
    private T activeItem;

    /// <summary>The currently active item.</summary>
    public T ActiveItem
    {
      get => this.activeItem;
      set => this.ActivateItem(value);
    }

    /// <summary>The currently active item.</summary>
    /// <value></value>
    object IHaveActiveItem.ActiveItem
    {
      get => (object) this.ActiveItem;
      set => this.ActiveItem = (T) value;
    }

    /// <summary>Changes the active item.</summary>
    /// <param name="newItem">The new item to activate.</param>
    /// <param name="closePrevious">Indicates whether or not to close the previous active item.</param>
    protected virtual void ChangeActiveItem(T newItem, bool closePrevious)
    {
      ScreenExtensions.TryDeactivate((object) this.activeItem, closePrevious);
      newItem = this.EnsureItem(newItem);
      if (this.IsActive)
        ScreenExtensions.TryActivate((object) newItem);
      this.activeItem = newItem;
      this.NotifyOfPropertyChange("ActiveItem");
      this.OnActivationProcessed(this.activeItem, true);
    }
  }
}
