// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.BindableCollection`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A base collection class that supports automatic UI thread marshalling.
  /// </summary>
  /// <typeparam name="T">The type of elements contained in the collection.</typeparam>
  public class BindableCollection<T> : 
    ObservableCollection<T>,
    IObservableCollection<T>,
    IList<T>,
    ICollection<T>,
    IEnumerable<T>,
    IEnumerable,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged,
    INotifyCollectionChanged
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.BindableCollection`1" /> class.
    /// </summary>
    public BindableCollection() => this.IsNotifying = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.BindableCollection`1" /> class.
    /// </summary>
    /// <param name="collection">The collection from which the elements are copied.</param>
    public BindableCollection(IEnumerable<T> collection)
      : base(collection)
    {
      this.IsNotifying = true;
    }

    /// <summary>Enables/Disables property change notification.</summary>
    public bool IsNotifying { get; set; }

    /// <summary>Notifies subscribers of the property change.</summary>
    /// <param name="propertyName">Name of the property.</param>
    public virtual void NotifyOfPropertyChange(string propertyName)
    {
      if (!this.IsNotifying)
        return;
      ((Action) (() => this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName)))).OnUIThread();
    }

    /// <summary>
    /// Raises a change notification indicating that all bindings should be refreshed.
    /// </summary>
    public void Refresh()
    {
      ((Action) (() =>
      {
        this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
        this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      })).OnUIThread();
    }

    /// <summary>Inserts the item to the specified position.</summary>
    /// <param name="index">The index to insert at.</param>
    /// <param name="item">The item to be inserted.</param>
    protected override sealed void InsertItem(int index, T item)
    {
      ((Action) (() => this.InsertItemBase(index, item))).OnUIThread();
    }

    /// <summary>
    /// Exposes the base implementation of the <see cref="M:Caliburn.Micro.BindableCollection`1.InsertItem(System.Int32,`0)" /> function.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="item">The item.</param>
    /// <remarks>
    ///   Used to avoid compiler warning regarding unverifiable code.
    /// </remarks>
    protected virtual void InsertItemBase(int index, T item) => base.InsertItem(index, item);

    /// <summary>Sets the item at the specified position.</summary>
    /// <param name="index">The index to set the item at.</param>
    /// <param name="item">The item to set.</param>
    protected override sealed void SetItem(int index, T item)
    {
      ((Action) (() => this.SetItemBase(index, item))).OnUIThread();
    }

    /// <summary>
    /// Exposes the base implementation of the <see cref="M:Caliburn.Micro.BindableCollection`1.SetItem(System.Int32,`0)" /> function.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="item">The item.</param>
    /// <remarks>
    ///   Used to avoid compiler warning regarding unverifiable code.
    /// </remarks>
    protected virtual void SetItemBase(int index, T item) => base.SetItem(index, item);

    /// <summary>Removes the item at the specified position.</summary>
    /// <param name="index">The position used to identify the item to remove.</param>
    protected override sealed void RemoveItem(int index)
    {
      ((Action) (() => this.RemoveItemBase(index))).OnUIThread();
    }

    /// <summary>
    /// Exposes the base implementation of the <see cref="M:Caliburn.Micro.BindableCollection`1.RemoveItem(System.Int32)" /> function.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <remarks>
    ///   Used to avoid compiler warning regarding unverifiable code.
    /// </remarks>
    protected virtual void RemoveItemBase(int index) => base.RemoveItem(index);

    /// <summary>Clears the items contained by the collection.</summary>
    protected override sealed void ClearItems() => new Action(this.ClearItemsBase).OnUIThread();

    /// <summary>
    /// Exposes the base implementation of the <see cref="M:Caliburn.Micro.BindableCollection`1.ClearItems" /> function.
    /// </summary>
    /// <remarks>
    ///   Used to avoid compiler warning regarding unverifiable code.
    /// </remarks>
    protected virtual void ClearItemsBase() => base.ClearItems();

    /// <summary>
    /// Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.
    /// </summary>
    /// <param name="e">Arguments of the event being raised.</param>
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (!this.IsNotifying)
        return;
      base.OnCollectionChanged(e);
    }

    /// <summary>
    /// Raises the PropertyChanged event with the provided arguments.
    /// </summary>
    /// <param name="e">The event data to report in the event.</param>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (!this.IsNotifying)
        return;
      base.OnPropertyChanged(e);
    }

    /// <summary>Adds the range.</summary>
    /// <param name="items">The items.</param>
    public virtual void AddRange(IEnumerable<T> items)
    {
      ((Action) (() =>
      {
        bool isNotifying = this.IsNotifying;
        this.IsNotifying = false;
        int count = this.Count;
        foreach (T obj in items)
        {
          this.InsertItemBase(count, obj);
          ++count;
        }
        this.IsNotifying = isNotifying;
        this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
        this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      })).OnUIThread();
    }

    /// <summary>Removes the range.</summary>
    /// <param name="items">The items.</param>
    public virtual void RemoveRange(IEnumerable<T> items)
    {
      ((Action) (() =>
      {
        bool isNotifying = this.IsNotifying;
        this.IsNotifying = false;
        foreach (T obj in items)
        {
          int index = this.IndexOf(obj);
          if (index >= 0)
            this.RemoveItemBase(index);
        }
        this.IsNotifying = isNotifying;
        this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
        this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      })).OnUIThread();
    }
  }
}
