// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.AttachedCollection`1
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A collection that can exist as part of a behavior.</summary>
  /// <typeparam name="T">The type of item in the attached collection.</typeparam>
  public class AttachedCollection<T> : DependencyObjectCollection<T>, IAttachedObject where T : DependencyObject, IAttachedObject
  {
    private DependencyObject associatedObject;

    /// <summary>
    /// Creates an instance of <see cref="T:Caliburn.Micro.AttachedCollection`1" />
    /// </summary>
    public AttachedCollection()
    {
      this.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
    }

    /// <summary>Attached the collection.</summary>
    /// <param name="dependencyObject">The dependency object to attach the collection to.</param>
    public void Attach(DependencyObject dependencyObject)
    {
      this.associatedObject = dependencyObject;
      this.Apply<T>((Action<T>) (x => x.Attach(this.associatedObject)));
    }

    /// <summary>Detaches the collection.</summary>
    public void Detach()
    {
      this.Apply<T>((Action<T>) (x => x.Detach()));
      this.associatedObject = (DependencyObject) null;
    }

    DependencyObject IAttachedObject.AssociatedObject => this.associatedObject;

    /// <summary>Called when an item is added from the collection.</summary>
    /// <param name="item">The item that was added.</param>
    protected void OnItemAdded(T item)
    {
      if (this.associatedObject == null)
        return;
      item.Attach(this.associatedObject);
    }

    /// <summary>Called when an item is removed from the collection.</summary>
    /// <param name="item">The item that was removed.</param>
    protected void OnItemRemoved(T item)
    {
      if (item.AssociatedObject == null)
        return;
      item.Detach();
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          e.NewItems.OfType<T>().Where<T>((Func<T, bool>) (x => !this.Contains(x))).Apply<T>(new Action<T>(this.OnItemAdded));
          break;
        case NotifyCollectionChangedAction.Remove:
          e.OldItems.OfType<T>().Apply<T>(new Action<T>(this.OnItemRemoved));
          break;
        case NotifyCollectionChangedAction.Replace:
          e.OldItems.OfType<T>().Apply<T>(new Action<T>(this.OnItemRemoved));
          e.NewItems.OfType<T>().Where<T>((Func<T, bool>) (x => !this.Contains(x))).Apply<T>(new Action<T>(this.OnItemAdded));
          break;
        case NotifyCollectionChangedAction.Reset:
          this.Apply<T>(new Action<T>(this.OnItemRemoved));
          this.Apply<T>(new Action<T>(this.OnItemAdded));
          break;
      }
    }
  }
}
