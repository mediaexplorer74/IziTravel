// Decompiled with JetBrains decompiler
// Type: Weakly.WeakCollection`1
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Weakly
{
  /// <summary>
  /// A collections which only holds weak references to the items.
  /// </summary>
  /// <typeparam name="T">The type of the elements in the collection.</typeparam>
  public class WeakCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable where T : class
  {
    private readonly List<WeakReference> _inner;
    private readonly WeakReference _gcSentinel = new WeakReference(new object());

    private bool IsCleanupNeeded()
    {
      if (this._gcSentinel.Target != null)
        return false;
      this._gcSentinel.Target = new object();
      return true;
    }

    private void CleanAbandonedItems()
    {
      for (int index = this._inner.Count - 1; index >= 0; --index)
      {
        if (!this._inner[index].IsAlive)
          this._inner.RemoveAt(index);
      }
    }

    private void CleanIfNeeded()
    {
      if (!this.IsCleanupNeeded())
        return;
      this.CleanAbandonedItems();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakCollection`1" /> class that is empty and has the default initial capacity.
    /// </summary>
    public WeakCollection() => this._inner = new List<WeakReference>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakCollection`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new collection.</param>
    public WeakCollection(IEnumerable<T> collection)
    {
      this._inner = new List<WeakReference>(collection.Select<T, WeakReference>((Func<T, WeakReference>) (item => new WeakReference((object) item))));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakCollection`1" /> class that is empty and has the specified initial capacity.
    /// </summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public WeakCollection(int capacity) => this._inner = new List<WeakReference>(capacity);

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
    public IEnumerator<T> GetEnumerator()
    {
      this.CleanIfNeeded();
      return this._inner.Select<WeakReference, T>((Func<WeakReference, T>) (item => (T) item.Target)).Where<T>((Func<T, bool>) (value => (object) value != null)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    public void Add(T item)
    {
      this.CleanIfNeeded();
      this._inner.Add(new WeakReference((object) item));
    }

    /// <summary>
    /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    public void Clear() => this._inner.Clear();

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
    public bool Contains(T item)
    {
      this.CleanIfNeeded();
      return this._inner.Any<WeakReference>((Func<WeakReference, bool>) (w => (object) (T) w.Target == (object) item));
    }

    /// <summary>
    /// Copies the elements of the collection to an Array, starting at a particular Array index.
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination of the elements copied from the collection.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(T[] array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (arrayIndex < 0 || arrayIndex >= array.Length)
        throw new ArgumentOutOfRangeException(nameof (arrayIndex));
      if (arrayIndex + this.Count > array.Length)
        throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
      this.ToArray<T>().CopyTo((Array) array, arrayIndex);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
    public bool Remove(T item)
    {
      this.CleanIfNeeded();
      for (int index = 0; index < this._inner.Count; ++index)
      {
        if ((object) (T) this._inner[index].Target == (object) item)
        {
          this._inner.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
    public int Count
    {
      get
      {
        this.CleanIfNeeded();
        return this._inner.Count;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </summary>
    /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
    public bool IsReadOnly => false;
  }
}
