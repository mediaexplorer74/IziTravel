// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.CollectionChangeListener`1
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
  internal abstract class CollectionChangeListener<T>
  {
    protected void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e), "NotifyCollectionChangedEventArgs");
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          for (int index = e.NewItems.Count - 1; index >= 0; --index)
            this.InsertItemInternal(e.NewStartingIndex, (T) e.NewItems[index]);
          break;
        case NotifyCollectionChangedAction.Remove:
          IEnumerator enumerator = e.OldItems.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
              this.RemoveItemInternal((T) enumerator.Current);
            break;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        case NotifyCollectionChangedAction.Replace:
          foreach (T oldItem in (IEnumerable) e.OldItems)
            this.RemoveItemInternal(oldItem);
          for (int index = e.NewItems.Count - 1; index >= 0; --index)
            this.InsertItemInternal(e.NewStartingIndex, (T) e.NewItems[index]);
          break;
        case NotifyCollectionChangedAction.Move:
          this.MoveInternal((T) e.OldItems[0], e.NewStartingIndex);
          break;
        case NotifyCollectionChangedAction.Reset:
          this.ResetInternal();
          this.AddRangeInternal((IEnumerable<T>) sender);
          break;
      }
    }

    protected void AddRangeInternal(IEnumerable<T> items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      foreach (T obj in items)
        this.AddInternal(obj);
    }

    protected abstract void InsertItemInternal(int index, T obj);

    protected abstract void RemoveItemInternal(T obj);

    protected abstract void ResetInternal();

    protected abstract void AddInternal(T obj);

    protected abstract void MoveInternal(T obj, int newIndex);
  }
}
