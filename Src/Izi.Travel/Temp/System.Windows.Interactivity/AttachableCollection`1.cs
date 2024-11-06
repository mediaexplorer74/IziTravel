// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.AttachableCollection`1
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Represents a collection of IAttachedObject with a shared AssociatedObject and provides change notifications to its contents when that AssociatedObject changes.
  /// </summary>
  public abstract class AttachableCollection<T> : DependencyObjectCollection<T>, IAttachedObject where T : DependencyObject, IAttachedObject
  {
    private Collection<T> snapshot;
    private DependencyObject associatedObject;

    /// <summary>The object on which the collection is hosted.</summary>
    protected DependencyObject AssociatedObject => this.associatedObject;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.AttachableCollection`1" /> class.
    /// </summary>
    /// <remarks>Internal, because this should not be inherited outside this assembly.</remarks>
    internal AttachableCollection()
    {
      this.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
      this.snapshot = new Collection<T>();
    }

    /// <summary>
    /// Called immediately after the collection is attached to an AssociatedObject.
    /// </summary>
    protected abstract void OnAttached();

    /// <summary>
    /// Called when the collection is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    protected abstract void OnDetaching();

    /// <summary>Called when a new item is added to the collection.</summary>
    /// <param name="item">The new item.</param>
    internal abstract void ItemAdded(T item);

    /// <summary>Called when an item is removed from the collection.</summary>
    /// <param name="item">The removed item.</param>
    internal abstract void ItemRemoved(T item);

    [Conditional("DEBUG")]
    private void VerifySnapshotIntegrity()
    {
      if (this.Count != this.snapshot.Count)
        return;
      for (int index = 0; index < this.Count; ++index)
      {
        if ((object) this[index] != (object) this.snapshot[index])
          break;
      }
    }

    /// <exception cref="T:System.InvalidOperationException">Cannot add the instance to a collection more than once.</exception>
    private void VerifyAdd(T item)
    {
      if (this.snapshot.Contains(item))
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, ExceptionStringTable.DuplicateItemInCollectionExceptionMessage, (object) typeof (T).Name, (object) this.GetType().Name));
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          IEnumerator enumerator1 = e.NewItems.GetEnumerator();
          try
          {
            while (enumerator1.MoveNext())
            {
              T current = (T) enumerator1.Current;
              try
              {
                this.VerifyAdd(current);
                this.ItemAdded(current);
              }
              finally
              {
                this.snapshot.Insert(this.IndexOf(current), current);
              }
            }
            break;
          }
          finally
          {
            if (enumerator1 is IDisposable disposable)
              disposable.Dispose();
          }
        case NotifyCollectionChangedAction.Remove:
          IEnumerator enumerator2 = e.OldItems.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
            {
              T current = (T) enumerator2.Current;
              this.ItemRemoved(current);
              this.snapshot.Remove(current);
            }
            break;
          }
          finally
          {
            if (enumerator2 is IDisposable disposable)
              disposable.Dispose();
          }
        case NotifyCollectionChangedAction.Replace:
          foreach (T oldItem in (IEnumerable) e.OldItems)
          {
            this.ItemRemoved(oldItem);
            this.snapshot.Remove(oldItem);
          }
          IEnumerator enumerator3 = e.NewItems.GetEnumerator();
          try
          {
            while (enumerator3.MoveNext())
            {
              T current = (T) enumerator3.Current;
              try
              {
                this.VerifyAdd(current);
                this.ItemAdded(current);
              }
              finally
              {
                this.snapshot.Insert(this.IndexOf(current), current);
              }
            }
            break;
          }
          finally
          {
            if (enumerator3 is IDisposable disposable)
              disposable.Dispose();
          }
        case NotifyCollectionChangedAction.Reset:
          foreach (T obj in this.snapshot)
            this.ItemRemoved(obj);
          this.snapshot = new Collection<T>();
          using (IEnumerator<T> enumerator4 = this.GetEnumerator())
          {
            while (enumerator4.MoveNext())
            {
              T current = enumerator4.Current;
              this.VerifyAdd(current);
              this.ItemAdded(current);
            }
            break;
          }
      }
    }

    /// <summary>Gets the associated object.</summary>
    /// <value>The associated object.</value>
    DependencyObject IAttachedObject.AssociatedObject => this.AssociatedObject;

    /// <summary>Attaches to the specified object.</summary>
    /// <param name="dependencyObject">The object to attach to.</param>
    /// <exception cref="T:System.InvalidOperationException">The IAttachedObject is already attached to a different object.</exception>
    public void Attach(DependencyObject dependencyObject)
    {
      if (dependencyObject == this.AssociatedObject)
        return;
      if (this.AssociatedObject != null)
        throw new InvalidOperationException();
      if (Application.Current == null || Application.Current.RootVisual == null || !(bool) Application.Current.RootVisual.GetValue(DesignerProperties.IsInDesignModeProperty))
        this.associatedObject = dependencyObject;
      this.OnAttached();
    }

    /// <summary>Detaches this instance from its associated object.</summary>
    public void Detach()
    {
      this.OnDetaching();
      this.associatedObject = (DependencyObject) null;
    }
  }
}
