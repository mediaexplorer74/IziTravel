// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlAttributeCollection
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace HtmlAgilityPack
{
  /// <summary>
  /// Represents a combined list and collection of HTML nodes.
  /// </summary>
  public class HtmlAttributeCollection : 
    IList<HtmlAttribute>,
    ICollection<HtmlAttribute>,
    IEnumerable<HtmlAttribute>,
    IEnumerable
  {
    internal Dictionary<string, HtmlAttribute> Hashitems = new Dictionary<string, HtmlAttribute>();
    private HtmlNode _ownernode;
    private List<HtmlAttribute> items = new List<HtmlAttribute>();

    internal HtmlAttributeCollection(HtmlNode ownernode) => this._ownernode = ownernode;

    /// <summary>Gets a given attribute from the list using its name.</summary>
    public HtmlAttribute this[string name]
    {
      get
      {
        if (name == null)
          throw new ArgumentNullException(nameof (name));
        HtmlAttribute htmlAttribute;
        return !this.Hashitems.TryGetValue(name.ToLower(), out htmlAttribute) ? (HtmlAttribute) null : htmlAttribute;
      }
      set => this.Append(value);
    }

    /// <summary>
    /// Gets the number of elements actually contained in the list.
    /// </summary>
    public int Count => this.items.Count;

    /// <summary>Gets readonly status of colelction</summary>
    public bool IsReadOnly => false;

    /// <summary>Gets the attribute at the specified index.</summary>
    public HtmlAttribute this[int index]
    {
      get => this.items[index];
      set => this.items[index] = value;
    }

    /// <summary>Adds supplied item to collection</summary>
    /// <param name="item"></param>
    public void Add(HtmlAttribute item) => this.Append(item);

    /// <summary>Explicit clear</summary>
    void ICollection<HtmlAttribute>.Clear() => this.items.Clear();

    /// <summary>Retreives existence of supplied item</summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(HtmlAttribute item) => this.items.Contains(item);

    /// <summary>Copies collection to array</summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public void CopyTo(HtmlAttribute[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }

    /// <summary>Get Explicit enumerator</summary>
    /// <returns></returns>
    IEnumerator<HtmlAttribute> IEnumerable<HtmlAttribute>.GetEnumerator()
    {
      return (IEnumerator<HtmlAttribute>) this.items.GetEnumerator();
    }

    /// <summary>Explicit non-generic enumerator</summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.items.GetEnumerator();

    /// <summary>
    /// Retrieves the index for the supplied item, -1 if not found
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int IndexOf(HtmlAttribute item) => this.items.IndexOf(item);

    /// <summary>Inserts given item into collection at supplied index</summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    public void Insert(int index, HtmlAttribute item)
    {
      this.Hashitems[item.Name] = item != null ? item : throw new ArgumentNullException(nameof (item));
      item._ownernode = this._ownernode;
      this.items.Insert(index, item);
      this._ownernode.SetChanged();
    }

    /// <summary>Explicit collection remove</summary>
    /// <param name="item"></param>
    /// <returns></returns>
    bool ICollection<HtmlAttribute>.Remove(HtmlAttribute item) => this.items.Remove(item);

    /// <summary>Removes the attribute at the specified index.</summary>
    /// <param name="index">The index of the attribute to remove.</param>
    public void RemoveAt(int index)
    {
      this.Hashitems.Remove(this.items[index].Name);
      this.items.RemoveAt(index);
      this._ownernode.SetChanged();
    }

    /// <summary>
    /// Adds a new attribute to the collection with the given values
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void Add(string name, string value) => this.Append(name, value);

    /// <summary>
    /// Inserts the specified attribute as the last attribute in the collection.
    /// </summary>
    /// <param name="newAttribute">The attribute to insert. May not be null.</param>
    /// <returns>The appended attribute.</returns>
    public HtmlAttribute Append(HtmlAttribute newAttribute)
    {
      this.Hashitems[newAttribute.Name] = newAttribute != null ? newAttribute : throw new ArgumentNullException(nameof (newAttribute));
      newAttribute._ownernode = this._ownernode;
      this.items.Add(newAttribute);
      this._ownernode.SetChanged();
      return newAttribute;
    }

    /// <summary>
    /// Creates and inserts a new attribute as the last attribute in the collection.
    /// </summary>
    /// <param name="name">The name of the attribute to insert.</param>
    /// <returns>The appended attribute.</returns>
    public HtmlAttribute Append(string name)
    {
      return this.Append(this._ownernode._ownerdocument.CreateAttribute(name));
    }

    /// <summary>
    /// Creates and inserts a new attribute as the last attribute in the collection.
    /// </summary>
    /// <param name="name">The name of the attribute to insert.</param>
    /// <param name="value">The value of the attribute to insert.</param>
    /// <returns>The appended attribute.</returns>
    public HtmlAttribute Append(string name, string value)
    {
      return this.Append(this._ownernode._ownerdocument.CreateAttribute(name, value));
    }

    /// <summary>Checks for existance of attribute with given name</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Contains(string name)
    {
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index].Name.Equals(name.ToLower()))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Inserts the specified attribute as the first node in the collection.
    /// </summary>
    /// <param name="newAttribute">The attribute to insert. May not be null.</param>
    /// <returns>The prepended attribute.</returns>
    public HtmlAttribute Prepend(HtmlAttribute newAttribute)
    {
      this.Insert(0, newAttribute);
      return newAttribute;
    }

    /// <summary>Removes a given attribute from the list.</summary>
    /// <param name="attribute">The attribute to remove. May not be null.</param>
    public void Remove(HtmlAttribute attribute)
    {
      int index = attribute != null ? this.GetAttributeIndex(attribute) : throw new ArgumentNullException(nameof (attribute));
      if (index == -1)
        throw new IndexOutOfRangeException();
      this.RemoveAt(index);
    }

    /// <summary>
    /// Removes an attribute from the list, using its name. If there are more than one attributes with this name, they will all be removed.
    /// </summary>
    /// <param name="name">The attribute's name. May not be null.</param>
    public void Remove(string name)
    {
      string str = name != null ? name.ToLower() : throw new ArgumentNullException(nameof (name));
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index].Name == str)
          this.RemoveAt(index);
      }
    }

    /// <summary>Remove all attributes in the list.</summary>
    public void RemoveAll()
    {
      this.Hashitems.Clear();
      this.items.Clear();
      this._ownernode.SetChanged();
    }

    /// <summary>
    /// Returns all attributes with specified name. Handles case insentivity
    /// </summary>
    /// <param name="attributeName">Name of the attribute</param>
    /// <returns></returns>
    public IEnumerable<HtmlAttribute> AttributesWithName(string attributeName)
    {
      attributeName = attributeName.ToLower();
      for (int i = 0; i < this.items.Count; ++i)
      {
        if (this.items[i].Name.Equals(attributeName))
          yield return this.items[i];
      }
    }

    /// <summary>Removes all attributes from the collection</summary>
    public void Remove()
    {
      foreach (HtmlAttribute htmlAttribute in this.items)
        htmlAttribute.Remove();
    }

    /// <summary>Clears the attribute collection</summary>
    internal void Clear()
    {
      this.Hashitems.Clear();
      this.items.Clear();
    }

    internal int GetAttributeIndex(HtmlAttribute attribute)
    {
      if (attribute == null)
        throw new ArgumentNullException(nameof (attribute));
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index] == attribute)
          return index;
      }
      return -1;
    }

    internal int GetAttributeIndex(string name)
    {
      string str = name != null ? name.ToLower() : throw new ArgumentNullException(nameof (name));
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index].Name == str)
          return index;
      }
      return -1;
    }
  }
}
