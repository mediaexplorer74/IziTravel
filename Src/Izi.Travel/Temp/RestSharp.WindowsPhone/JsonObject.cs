// Decompiled with JetBrains decompiler
// Type: RestSharp.JsonObject
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace RestSharp
{
  /// <summary>Represents the json object.</summary>
  [GeneratedCode("simple-json", "1.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class JsonObject : 
    IDictionary<string, object>,
    ICollection<KeyValuePair<string, object>>,
    IEnumerable<KeyValuePair<string, object>>,
    IEnumerable
  {
    /// <summary>The internal member dictionary.</summary>
    private readonly Dictionary<string, object> _members;

    /// <summary>
    /// Initializes a new instance of <see cref="T:RestSharp.JsonObject" />.
    /// </summary>
    public JsonObject() => this._members = new Dictionary<string, object>();

    /// <summary>
    /// Initializes a new instance of <see cref="T:RestSharp.JsonObject" />.
    /// </summary>
    /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
    public JsonObject(IEqualityComparer<string> comparer)
    {
      this._members = new Dictionary<string, object>(comparer);
    }

    /// <summary>
    /// Gets the <see cref="T:System.Object" /> at the specified index.
    /// </summary>
    /// <value></value>
    public object this[int index]
    {
      get => JsonObject.GetAtIndex((IDictionary<string, object>) this._members, index);
    }

    internal static object GetAtIndex(IDictionary<string, object> obj, int index)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (index >= obj.Count)
        throw new ArgumentOutOfRangeException(nameof (index));
      int num = 0;
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) obj)
      {
        if (num++ == index)
          return keyValuePair.Value;
      }
      return (object) null;
    }

    /// <summary>Adds the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public void Add(string key, object value) => this._members.Add(key, value);

    /// <summary>Determines whether the specified key contains key.</summary>
    /// <param name="key">The key.</param>
    /// <returns>
    ///     <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
    /// </returns>
    public bool ContainsKey(string key) => this._members.ContainsKey(key);

    /// <summary>Gets the keys.</summary>
    /// <value>The keys.</value>
    public ICollection<string> Keys => (ICollection<string>) this._members.Keys;

    /// <summary>Removes the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public bool Remove(string key) => this._members.Remove(key);

    /// <summary>Tries the get value.</summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool TryGetValue(string key, out object value)
    {
      return this._members.TryGetValue(key, out value);
    }

    /// <summary>Gets the values.</summary>
    /// <value>The values.</value>
    public ICollection<object> Values => (ICollection<object>) this._members.Values;

    /// <summary>
    /// Gets or sets the <see cref="T:System.Object" /> with the specified key.
    /// </summary>
    /// <value></value>
    public object this[string key]
    {
      get => this._members[key];
      set => this._members[key] = value;
    }

    /// <summary>Adds the specified item.</summary>
    /// <param name="item">The item.</param>
    public void Add(KeyValuePair<string, object> item) => this._members.Add(item.Key, item.Value);

    /// <summary>Clears this instance.</summary>
    public void Clear() => this._members.Clear();

    /// <summary>Determines whether [contains] [the specified item].</summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// 	<c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
    /// </returns>
    public bool Contains(KeyValuePair<string, object> item)
    {
      return this._members.ContainsKey(item.Key) && this._members[item.Key] == item.Value;
    }

    /// <summary>Copies to.</summary>
    /// <param name="array">The array.</param>
    /// <param name="arrayIndex">Index of the array.</param>
    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      int count = this.Count;
      foreach (KeyValuePair<string, object> keyValuePair in this)
      {
        array[arrayIndex++] = keyValuePair;
        if (--count <= 0)
          break;
      }
    }

    /// <summary>Gets the count.</summary>
    /// <value>The count.</value>
    public int Count => this._members.Count;

    /// <summary>
    /// Gets a value indicating whether this instance is read only.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is read only; otherwise, <c>false</c>.
    /// </value>
    public bool IsReadOnly => false;

    /// <summary>Removes the specified item.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public bool Remove(KeyValuePair<string, object> item) => this._members.Remove(item.Key);

    /// <summary>Gets the enumerator.</summary>
    /// <returns></returns>
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<string, object>>) this._members.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._members.GetEnumerator();

    /// <summary>
    /// Returns a json <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// A json <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString() => SimpleJson.SerializeObject((object) this);
  }
}
