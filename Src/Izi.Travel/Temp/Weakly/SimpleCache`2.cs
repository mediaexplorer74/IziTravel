// Decompiled with JetBrains decompiler
// Type: Weakly.SimpleCache`2
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System.Collections.Generic;

#nullable disable
namespace Weakly
{
  internal sealed class SimpleCache<TKey, TValue>
  {
    private readonly IDictionary<TKey, TValue> _storage;

    public SimpleCache()
    {
      this._storage = (IDictionary<TKey, TValue>) new Dictionary<TKey, TValue>();
    }

    public TValue GetValueOrDefault(TKey key)
    {
      TValue valueOrDefault;
      lock (this._storage)
        this._storage.TryGetValue(key, out valueOrDefault);
      return valueOrDefault;
    }

    public TValueAs GetValueOrDefault<TValueAs>(TKey key) where TValueAs : class
    {
      return (object) this.GetValueOrDefault(key) as TValueAs;
    }

    public void AddOrUpdate(TKey key, TValue value)
    {
      lock (this._storage)
        this._storage[key] = value;
    }

    public bool Remove(TKey key)
    {
      lock (this._storage)
        return this._storage.Remove(key);
    }

    public void Clear()
    {
      lock (this._storage)
        this._storage.Clear();
    }
  }
}
