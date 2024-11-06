// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.SecureObjectPool`2
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  internal class SecureObjectPool<T, TCaller> where TCaller : ISecurePooledObjectUser
  {
    private AllocFreeConcurrentStack<SecurePooledObject<T>> pool = new AllocFreeConcurrentStack<SecurePooledObject<T>>();

    public void TryAdd(TCaller caller, SecurePooledObject<T> item)
    {
      lock (item)
      {
        if (!(caller.PoolUserId == item.Owner))
          return;
        item.Owner = Guid.Empty;
        this.pool.TryAdd(item);
      }
    }

    public bool TryTake(TCaller caller, out SecurePooledObject<T> item)
    {
      if (caller.PoolUserId != Guid.Empty && this.pool.TryTake(out item))
      {
        item.Owner = caller.PoolUserId;
        return true;
      }
      item = (SecurePooledObject<T>) null;
      return false;
    }

    public SecurePooledObject<T> PrepNew(TCaller caller, T newValue)
    {
      Requires.NotNullAllowStructs<T>(newValue, nameof (newValue));
      return new SecurePooledObject<T>(newValue)
      {
        Owner = caller.PoolUserId
      };
    }
  }
}
