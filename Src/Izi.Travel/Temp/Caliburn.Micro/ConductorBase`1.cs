// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ConductorBase`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A base class for various implementations of <see cref="T:Caliburn.Micro.IConductor" />.
  /// </summary>
  /// <typeparam name="T">The type that is being conducted.</typeparam>
  public abstract class ConductorBase<T> : 
    Screen,
    IConductor,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged,
    IParent<T>,
    IParent
    where T : class
  {
    private ICloseStrategy<T> closeStrategy;

    /// <summary>Gets or sets the close strategy.</summary>
    /// <value>The close strategy.</value>
    public ICloseStrategy<T> CloseStrategy
    {
      get
      {
        return this.closeStrategy ?? (this.closeStrategy = (ICloseStrategy<T>) new DefaultCloseStrategy<T>());
      }
      set => this.closeStrategy = value;
    }

    void IConductor.ActivateItem(object item) => this.ActivateItem((T) item);

    void IConductor.DeactivateItem(object item, bool close) => this.DeactivateItem((T) item, close);

    IEnumerable IParent.GetChildren() => (IEnumerable) this.GetChildren();

    /// <summary>Occurs when an activation request is processed.</summary>
    public event EventHandler<ActivationProcessedEventArgs> ActivationProcessed = (param0, param1) => { };

    /// <summary>Gets the children.</summary>
    /// <returns>The collection of children.</returns>
    public abstract IEnumerable<T> GetChildren();

    /// <summary>Activates the specified item.</summary>
    /// <param name="item">The item to activate.</param>
    public abstract void ActivateItem(T item);

    /// <summary>Deactivates the specified item.</summary>
    /// <param name="item">The item to close.</param>
    /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
    public abstract void DeactivateItem(T item, bool close);

    /// <summary>
    /// Called by a subclass when an activation needs processing.
    /// </summary>
    /// <param name="item">The item on which activation was attempted.</param>
    /// <param name="success">if set to <c>true</c> activation was successful.</param>
    protected virtual void OnActivationProcessed(T item, bool success)
    {
      if ((object) item == null)
        return;
      this.ActivationProcessed((object) this, new ActivationProcessedEventArgs()
      {
        Item = (object) item,
        Success = success
      });
    }

    /// <summary>Ensures that an item is ready to be activated.</summary>
    /// <param name="newItem"></param>
    /// <returns>The item to be activated.</returns>
    protected virtual T EnsureItem(T newItem)
    {
      if (newItem is IChild child && child.Parent != this)
        child.Parent = (object) this;
      return newItem;
    }
  }
}
